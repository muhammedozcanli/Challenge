using Challenge.Persistence.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Business.BalanceOperations
{
    public interface IBalanceOperations
    {
        IEnumerable<BalanceDTO> GetBalances();
        bool UpdateBalance(BalanceDTO balanceDTO);
        BalanceDTO GetBalanceByUserId(Guid userId);
    }
}
