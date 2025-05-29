using Challenge.Persistence.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Business.ProductOperations
{
    public interface IProductOperations
    {
        Task<IEnumerable<ProductDTO>> GetProducts();
        ProductDTO GetProduct(Guid id);
    }
}
