using Catalog.API.Entities;
using Catalog.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly ILogger<CatalogController> _logger;
        private readonly IProductRepository _productRepo;

        public CatalogController(ILogger<CatalogController> logger, IProductRepository productRepo)
        {
            this._logger = logger;
            this._productRepo = productRepo;
        }

        #region GET
        [HttpGet("GetProducts", Name = "GetProducts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return Ok(await _productRepo.Get());
        }
        [HttpGet("GetProductById", Name = "GetProductById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Product>> GetProductById(string id)
        {
            Product product = await _productRepo.GetById(id);

            if (product == null)
                return NotFound($"No product with id {id} found");

            return Ok(product);
        }

        [HttpGet("GetProductByName",Name = "GetProductByName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByName(string name)
        {
            IEnumerable<Product> products = await _productRepo.GetByName(name);

            if (products.Count() == 0)
                return NotFound($"No products with name {name} found");

            return Ok(products);
        }

        [HttpGet("GetByCategoryName",Name = "GetByCategoryName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Product>>> GetByCategoryName(string categoryName)
        {
            IEnumerable<Product> products = await _productRepo.GetByCategoryName(categoryName);

            if (products.Count() == 0)
                return NotFound($"No products with categoryName {categoryName} found");

            return Ok(products);
        }

        #endregion

        #region POST
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            await _productRepo.CreateProduct(product);

            return CreatedAtRoute(nameof(GetProductById), new { id = product.Id }, product);
        }

        #endregion

        #region PUT
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product)
        {
            // We can check if product exist to return notfound
            return Ok(await _productRepo.UpdateProduct(product));
        }
        #endregion

        #region DELETE
        [HttpDelete("{id:length(24)}", Name = "DeleteProductById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteProductById(string id)
        {
            return Ok(await _productRepo.DeleteProduct(id));
        }

        #endregion
    }
}
