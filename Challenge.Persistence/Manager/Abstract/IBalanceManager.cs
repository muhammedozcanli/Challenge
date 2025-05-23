using Challenge.Persistence.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Persistence.Manager.Abstract
{
    public interface IBalanceManager
    {
        IEnumerable<BalanceDTO> GetBalances();
        bool UpdateBalance(BalanceDTO balanceDTO);
        BalanceDTO GetBalanceByUserId(Guid userId);
    }
}
