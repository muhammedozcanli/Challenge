using Challenge.Persistence.DTOs;
using Challenge.Persistence.Manager.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Business.PreOrderOperations
{
    public class PreOrderOperations : IPreOrderOperations
    {
        private readonly IPreOrderManager _preOrderManager;
        public PreOrderOperations(IPreOrderManager preOrderManager)
        {
            _preOrderManager = preOrderManager;
        }
        public void Add(PreOrderDTO preOrderDTO)
        {
            _preOrderManager.Add(preOrderDTO);
        }
    }
}
