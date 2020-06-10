namespace Products.Infra.Context.Settings
{
    public interface IMongoDbSettings
    {
        string ProductsCollectionName { get; set; }
        string ConnectionString { get; set; }
        string Database { get; set; }
    }
}