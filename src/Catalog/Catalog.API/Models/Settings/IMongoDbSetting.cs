namespace Catalog.API.Models.Settings
{
    public interface IMongoDbSetting
    {
        string CollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}