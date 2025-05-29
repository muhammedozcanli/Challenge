using AutoMapper;
using Challenge.Persistence.DTOs;
using Challenge.Persistence.Manager.Abstract;
using Challenge.Persistence.Repositories.Abstract;
using Challenge.Persistence.Repositories.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Persistence.Manager.Concrete
{
    public class ProductManager : IProductManager
    {
        IProductRepository _productRepository;
        IMapper _mapper;

        public ProductManager(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        /// <summary>
        /// Retrieves a list of all available products from the data source.
        /// </summary>
        /// <returns>A collection of <see cref="ProductDTO"/> representing the available products.</returns>
        public async Task<IEnumerable<ProductDTO>> GetProducts()
        {
            var products = await _productRepository.GetListAsync(null);
            var productDTO = _mapper.Map<List<ProductDTO>>(products);
            return productDTO;
        }
        public ProductDTO GetProduct(Guid id)
        {
            var product = _productRepository.Get(p => p.Id == id);
            var productDTO = _mapper.Map<ProductDTO>(product);
            return productDTO;
        }

    }
}
