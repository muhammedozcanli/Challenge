using AutoMapper;
using Challenge.Persistence.DTOs;
using Challenge.Persistence.Manager.Abstract;
using Challenge.Persistence.Manager.Concrete;
using Challenge.Persistence.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Business.ProductOperations
{
    public class ProductOperations : IProductOperations
    {
        IProductManager _productManager;

        public ProductOperations(IProductManager productManager)
        {
            _productManager = productManager;
        }
        /// <summary>
        /// Retrieves a collection of all available products.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{ProductDTO}"/> containing the product information.</returns>
        public async Task<IEnumerable<ProductDTO>> GetProducts()
        {
            var products = await _productManager.GetProducts();
            return products;
        }
        /// <summary>
        /// Retrieves a product of a given Id.
        /// </summary>
        /// <returns>An <see cref="ProductDTO"/> containing the product information.</returns>
        public ProductDTO GetProduct(Guid id)
        {
            return _productManager.GetProduct(id);
        }

    }
}
