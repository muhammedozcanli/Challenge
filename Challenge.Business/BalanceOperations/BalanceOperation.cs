using Challenge.Persistence.DTOs;
using Challenge.Persistence.Manager.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Business.BalanceOperations
{
    public class BalanceOperations : IBalanceOperations
    {
        IBalanceManager _balanceManager;
        public BalanceOperations(IBalanceManager balanceManager)
        {
            _balanceManager = balanceManager;
        }
        public IEnumerable<BalanceDTO> GetBalances()
        {
            return _balanceManager.GetBalances();
        }
        public bool UpdateBalance(BalanceDTO balanceDTO)
        {
            return _balanceManager.UpdateBalance(balanceDTO);
        }
    }
}
