using Challenge.Business.PreOrderOperations;
using Challenge.Persistence.Manager.Abstract;
using Xunit;

namespace Challenge.Business.Test.PreOrderTests
{
    public class PreOrderOperationsTests
    {
        private readonly IPreOrderOperations _preOrderOperations;
        private readonly IPreOrderManager _preOrderManager;

        public PreOrderOperationsTests(IPreOrderManager preOrderManager)
        {
            _preOrderManager = preOrderManager;
            _preOrderOperations = new PreOrderOperations.PreOrderOperations(_preOrderManager);
        }

        // Tests will be added when methods are implemented in PreOrderOperations class
    }
} 