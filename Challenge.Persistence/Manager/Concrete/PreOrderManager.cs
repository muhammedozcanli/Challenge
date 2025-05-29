using AutoMapper;
using Challenge.Persistence.DTOs;
using Challenge.Persistence.Entities;
using Challenge.Persistence.Manager.Abstract;
using Challenge.Persistence.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Persistence.Manager.Concrete
{
    public class PreOrderManager : IPreOrderManager
    {
        private readonly IPreOrderRepository _preOrderRepository;
        private readonly IMapper _mapper;
        public PreOrderManager(IPreOrderRepository preOrderRepository, IMapper mapper)
        {
            _preOrderRepository = preOrderRepository;
            _mapper = mapper;
        }
        public void Add(PreOrderDTO preOrderDTO)
        {
            var preOrderEntity = _mapper.Map<PreOrder>(preOrderDTO);
            _preOrderRepository.Add(preOrderEntity);
        }
    }
}
