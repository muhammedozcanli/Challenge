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
        public IEnumerable<ProductDTO> GetProducts()
        {
            var products = _productRepository.GetList();
            var productDTO = _mapper.Map<List<ProductDTO>>(products);
            return productDTO;
        }
    }
}
