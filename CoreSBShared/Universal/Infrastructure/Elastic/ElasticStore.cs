using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json;
using System.Threading.Tasks;
using CoreSBShared.Universal.Infrastructure.Interfaces;

using MongoDB.Driver.Core.Configuration;

using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using Nest;

using MongoDB.Bson;
using MongoDB.Bson.Serialization;

using Elasticsearch.Net;
using Result = Elastic.Clients.Elasticsearch.Result;

namespace CoreSBShared.Universal.Infrastructure.Elastic
{
       public class ElasticStore : IElasticStore
       {
        private readonly ElasticsearchClient _client;
        private readonly ElasticClient _client2;
        private string _indexName { get; set; }

        public ElasticStore()
        {

            var ur = new Uri("https://localhost:9200");
            var settings = new ElasticsearchClientSettings(ur);

            _client = new ElasticsearchClient(settings);
            SetIndex("Logging");
        }

        public void SetIndex(string indexName)
        {
            _indexName = indexName;
        }

        public async Task<T> GetByIdAsync<T, TKey>(TKey id) where T : class, IEntity<TKey>
        {
            var response = await _client.GetAsync<object>(_indexName, id.ToString());
            return BsonSerializer.Deserialize<T>(response.ToBsonDocument());
        }

        public async Task<T?> GetByIdAsync<T>(string id) where T : class, IEntityStringId
        {
            var result = await _client.GetAsync<T>(id);
            return result.Source;
        }

        public async Task<T> AddAsync<T>(T item) where T : class
        {
            var response = await _client.IndexAsync(item, _indexName);
            return BsonSerializer.Deserialize<T>(response.ToBsonDocument());
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
            return response.Result == Result.Deleted;
        }

        public async Task<bool> DeleteManyAsync<T>(IEnumerable<T> items) where T : class, IEntityStringId
        {
            var response = await _client
                .DeleteByQueryAsync<T>(_indexName, q=> q.Query(
                    m=> m.MatchAll()) );
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
    }
}