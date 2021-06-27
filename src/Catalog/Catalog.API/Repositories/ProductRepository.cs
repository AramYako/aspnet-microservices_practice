using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Repositories
{
    public class ProductRepository: IProductRepository
    {
        private readonly ICatalogContext _context;

        private readonly IMongoCollection<Product> _products;
        public ProductRepository(ICatalogContext context)
        {
            _context = context;
            _products = context._products;
        }

        #region GET
        public async Task<IEnumerable<Product>> Get() =>
            await _products.Find(product => true).ToListAsync();

        public async Task<Product> GetById(string id) =>
            await _products.Find<Product>(product => product.Id == id).FirstOrDefaultAsync();

        public async Task<IEnumerable<Product>> GetByName(string name) =>
            await _products.Find<Product>(product => product.Name == name).ToListAsync();
        public async Task<IEnumerable<Product>> GetByCategoryName(string categoryName) =>
            await _products.Find<Product>(product => product.Category == categoryName).ToListAsync();


        #endregion

        #region CREATE
        public async Task<Product> CreateProduct(Product Product)
        {
            await _products.InsertOneAsync(Product);

            return Product;
        }


        #endregion

        #region UPDATE
        public async Task<bool> UpdateProduct(Product productIn)
        {
            ReplaceOneResult updatedResult = await _products.ReplaceOneAsync(Product => Product.Id == productIn.Id, productIn);

            return updatedResult.IsAcknowledged && updatedResult.ModifiedCount > 0;

        }

        #endregion

        #region DELETE
        public async Task<bool> DeleteProduct(Product ProductIn)
        {
            DeleteResult result = await _products.DeleteOneAsync(Product => Product.Id == ProductIn.Id);

            return result.IsAcknowledged && result.DeletedCount > 0;

    
        }

        public async Task<bool> DeleteProduct(string id)
        {
            DeleteResult result = await _products.DeleteOneAsync(Product => Product.Id == id);

            return result.IsAcknowledged && result.DeletedCount > 0;
        }


        #endregion

    }
}
