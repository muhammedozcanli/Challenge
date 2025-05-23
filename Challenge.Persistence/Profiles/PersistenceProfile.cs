using AutoMapper;
using Challenge.Persistence.DTOs;
using Challenge.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Persistence.Profiles
{
    public class PersistenceProfile : Profile
    {
        /// <summary>
        /// Configures AutoMapper mappings between entities and their corresponding DTOs.
        /// </summary>
        public PersistenceProfile()
        {
            CreateMap<Balance, BalanceDTO>().ReverseMap();
            CreateMap<Error, ErrorDTO>().ReverseMap(); ;
            CreateMap<PreOrder, PreOrderDTO>().ReverseMap();
            CreateMap<Product, ProductDTO>().ReverseMap();
        }
    }
}
