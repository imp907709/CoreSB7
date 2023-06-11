using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CoreSBShared.Universal.Infrastructure.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace CoreSBShared.Universal.Infrastructure.Mongo
{
    public class MongoStore : IMongoStore
    {
        internal IMongoClient _client;

        internal IMongoDatabase _database;
        internal string _dbName;

        internal MongoClientSettings _settings;

        /// <summary>
        ///     Empty constructors are required for DI
        /// </summary>
        public MongoStore()
        {
        }

        public MongoStore(IMongoClient client, string databaseName)
        {
            SetWorkingDatabase(databaseName);
        }

        public MongoStore(string connString)
        {
            SetClient(connString);
        }

        public MongoStore(string connString, string dbName)
        {
            SetClient(connString);
            SetWorkingDatabase(dbName);
        }

        public async Task<T> GetByIdAsync<T>(T item) where T : class, ICoreDalObjg
        {
            return await GetCollection<T>().Find(s => s.Id == item.Id).FirstOrDefaultAsync();
        }

        public async Task<T> AddAsync<T>(T item) where T : class
        {
            await GetCollection<T>().InsertOneAsync(item);
            return item;
        }

        public async Task<IEnumerable<T>> AddManyAsync<T>(IEnumerable<T> items) where T : class
        {
            await GetCollection<T>().InsertManyAsync(items);
            return items;
        }

        public async Task<IEnumerable<T>> GetByFilterAsync<T>(Expression<Func<T, bool>> expression)
            where T : class, ICoreDalObjg
        {
            var items = await GetCollection<T>().Find(expression).ToListAsync();
            return items;
        }

        public async Task<T> UpdateAsync<T>(T item) where T : class, ICoreDalObjg
        {
            var result = await GetCollection<T>().ReplaceOneAsync(s => s.Id == item.Id, item);
            return BsonSerializer.Deserialize<T>(result.ToBsonDocument());
        }

        public async Task<bool> DeleteAsync<T>(T item) where T : class, ICoreDalObjg
        {
            var deleteFilter = Builders<T>.Filter.Eq(s => s.Id, item.Id);
            var result = await GetCollection<T>()
                .DeleteOneAsync(deleteFilter);
            return result.DeletedCount > 0;
        }

        public async Task<bool> DeleteManyAsync<T>(IEnumerable<T> items) where T : class, ICoreDalObjg
        {
            var deleteFilter = Builders<T>.Filter.In(s => s.Id, items?.Select(s => s.Id));

            var result = await GetCollection<T>()
                .DeleteOneAsync(deleteFilter);
            return result.DeletedCount > 0;
        }

        public void CreateDB()
        {
            _client.GetDatabase(_dbName);
        }

        public void DropDB()
        {
            _client.DropDatabase(_dbName);
        }

        private void SetClient(string connString)
        {
            _settings = MongoClientSettings.FromConnectionString(connString);
            _settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            _client = new MongoClient(_settings);
        }

        public void SetWorkingDatabase(string dbName)
        {
            _database = _client.GetDatabase(dbName);
            _dbName = dbName;
        }

        public IMongoCollection<T> GetCollection<T>()
        {
            return _database.GetCollection<T>(typeof(T).Name);
        }

        private static FilterDefinition<T> BuildFilterDefinition<T>(Expression<Func<T, bool>> expression)
        {
            return new ExpressionFilterDefinition<T>(expression);
        }
    }
}
