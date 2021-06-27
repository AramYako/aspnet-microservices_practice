using Catalog.API.Entities;
using Catalog.API.Models.Settings;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Data
{
    public class CatalogContext: ICatalogContext
    {
        public IMongoCollection<Product> _products { get; }

        public CatalogContext(IMongoDbSetting settings)
        {
            var client = new MongoClient(settings.ConnectionString);

            var database = client.GetDatabase(settings.DatabaseName);

            _products = database.GetCollection<Product>(settings.CollectionName);

            CatalogContextSeed.SeedData(_products);
        }

    }
}
