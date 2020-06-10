namespace Products.Infra.Context.Settings
{
    public class MongoDbSettings : IMongoDbSettings
    {
        public string ProductsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string Database { get; set; }
    }
}