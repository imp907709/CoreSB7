﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json;
using System.Threading.Tasks;
using CoreSBShared.Universal.Infrastructure.Interfaces;

using Elasticsearch.Net;
using Nest;

using MongoDB.Bson;
using MongoDB.Bson.Serialization;

namespace CoreSBShared.Universal.Infrastructure.Elastic
{
       public class ElasticStoreNest : IElasticStore, IElasticStoreNest
       {
        private readonly ElasticClient _client;
        private string _indexName { get; set; }

        public ElasticStoreNest()
        {
            var pool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));
            var connSettings = new Nest.ConnectionSettings(pool).DefaultIndex("logging");
            _client = new ElasticClient(connSettings);
            
            SetIndex("logging");
        }

        public void SetIndex(string indexName)
        {
            _indexName = indexName;
        }
        
        public string SetIndex<T>(string indexName)
            where T : class, IEntityStringId
        {
            _indexName = indexName;
            return CreateindexIfNotExists<T>(_indexName);
        }

        public string CreateindexIfNotExists<T>(string indexName) 
            where T : class, IEntityStringId
        {
            if (string.IsNullOrEmpty(indexName))
                return "No index info provided";

            bool indexExists = _client.Indices.Exists(indexName).Exists;
            
            if (!indexExists)
            {
                var createIndexResponse = _client.Indices.Create(indexName, c => c
                    .Map<T>(m => m.AutoMap())
                );

                if (!createIndexResponse.IsValid)
                    return $"Index not created: {createIndexResponse.DebugInformation}";
                
                return $"Index created: {createIndexResponse.DebugInformation}";
            }
            
            return $"indexExists: {indexExists}";
        }
        

        public async Task<T> GetByIdAsync<T, TKey>(TKey id) where T : class, IEntity<TKey>
        {
            
            var response = await _client.GetAsync<T>(id.ToString(), idx => idx.Index(_indexName));
            return BsonSerializer.Deserialize<T>(response.ToBsonDocument());
        }

        public async Task<T?> GetByIdAsync<T>(string id) where T : class, IEntityStringId
        {
            var result = await _client.GetAsync<T>(id);
            return result.Source;
        }

        public async Task<IndexResponse> AddAsyncElk<T>(T item) where T : class
        {
            var response = await _client.IndexAsync(item, idx => idx.Index<T>());
            return response;
        }
        public async Task<T> AddAsync<T>(T item) where T : class
        {
            var response = await _client.IndexAsync(item, idx => idx.Index<T>());
            if (response.IsValid && response.Result == Nest.Result.Created)
                return item;

            return null;
        }

        public async Task<IEnumerable<T>> AddManyAsync<T>(IEnumerable<T> items) where T : class
        {
            var response = await _client.IndexManyAsync(items, _indexName);
            return BsonSerializer.Deserialize<IEnumerable<T>>(response.Items.ToBsonDocument());
        }

        public async Task<IEnumerable<T>> GetByFilterAsync<T>(Expression<Func<T, bool>> expression) 
            where T : class, IEntityStringId
        {
            
            var searchResponse = await _client.SearchAsync<T>(s => s
                .Index(_indexName)
                .Query(q => q.MatchAll()));
            return searchResponse.Documents;
        }

        public async Task<T> UpdateAsync<T>(T item) where T : class
        {
            var indexResponse = await _client
                .IndexAsync(item, idx => idx.Index(_indexName));
            return BsonSerializer.Deserialize<T>(indexResponse.ToBsonDocument());
        }

        public async Task<bool> DeleteAsync<T>(T item) where T : class, IEntityStringId
        {
            var response = await _client.DeleteAsync<T>(item.Id, 
                d => d.Index(_indexName));
            return response.Result == Nest.Result.Deleted;
        }

        public async Task<bool> DeleteManyAsync<T>(IEnumerable<T> items) where T : class, IEntityStringId
        {
            var response = await _client.DeleteByQueryAsync<T>(d => d
                .Index(_indexName)
                .Query(q => q
                    .Ids(i => i
                        .Values(items.Select(s=>s.Id))
                    )
                )
            );
            return response?.Deleted > 0;
        }

        public void CreateDB()
        {
            if (!_client.Indices.Exists(_indexName).Exists)
            {
                _client.Indices.Create(_indexName);
            }
        }

        public void DropDB()
        {
            if (_client.Indices.Exists(_indexName).Exists)
            {
                _client.Indices.Delete(_indexName);
            }
        }

        private T ReturnDocument<T>(T doc) where T : BsonDocument
        {
            return BsonSerializer.Deserialize<T>(doc);
        }
        private IEnumerable<T> ReturnDocument<T>(List<T> doc) where T : BsonDocument
        {
            var result = doc.Select(s => BsonSerializer.Deserialize<T>(s));
            return result;
        }
        
        private IndexDescriptor<T> BuildIndexDescriptor<T>(string _indexName) where T : class, IEntityStringId
        {
            var index = Nest.IndexName.From<T>(_indexName);
            var indexDescriptor = new IndexDescriptor<T>(index);
            return indexDescriptor;
        }
    }
}