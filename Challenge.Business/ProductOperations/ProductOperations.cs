using Challenge.Persistence.DTOs;
using Challenge.Persistence.Manager.Abstract;
using Challenge.Persistence.Manager.Concrete;
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
        public IEnumerable<ProductDTO> GetProducts()
        {
            return _productManager.GetProducts();
        }
    }
}
