using System;
using MongoDB.Driver;
using Products.Domain.Entities;
using Products.Infra.Context.Settings;
using Microsoft.Extensions.Options;

namespace Products.Infra.Context
{
    public class EcommDbContext
    {
        private readonly IMongoDatabase _database;
        private readonly IMongoDbSettings _settings;
        public EcommDbContext(IOptions<MongoDbSettings> settings)
        {
            _settings = settings.Value;

            var client = new MongoClient(_settings.ConnectionString);
            if (client != null)
                _database = client.GetDatabase(_settings.Database);
        }

        public IMongoCollection<ProductEntity> Products
        {
            get
            {
                return _database.GetCollection<ProductEntity>(_settings.ProductsCollectionName);
            }
        }
    }
}