using AutoMapper;
using Challenge.Persistence.Manager.Concrete;
using Challenge.Persistence.Repositories.Concrete;
using Xunit;

namespace Challenge.Persistence.Test.ManagerTests
{
    public class PreOrderManagerTests
    {
        private readonly PreOrderManager _preOrderManager;
        private readonly PreOrderRepository _preOrderRepository;
        private IMapper _mapper;

        public PreOrderManagerTests(PreOrderRepository preOrderRepository, IMapper mapper)
        {
            _preOrderRepository = preOrderRepository;
            _mapper = mapper;
            _preOrderManager = new PreOrderManager(_preOrderRepository, _mapper);
        }

        // Tests will be added when methods are implemented in PreOrderManager class
    }
} 