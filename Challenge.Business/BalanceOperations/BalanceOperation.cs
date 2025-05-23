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
        /// <summary>
        /// Retrieves a collection of all user balances.
        /// </summary>
        /// <returns>An enumerable collection of <see cref="BalanceDTO"/> representing user balances.</returns>
        public IEnumerable<BalanceDTO> GetBalances()
        {
            return _balanceManager.GetBalances();
        }

        /// <summary>
        /// Updates the specified user balance information.
        /// </summary>
        /// <param name="balanceDTO">The <see cref="BalanceDTO"/> containing updated balance information.</param>
        /// <returns><c>true</c> if the balance was updated successfully; otherwise, <c>false</c>.</returns>
        public bool UpdateBalance(BalanceDTO balanceDTO)
        {
            return _balanceManager.UpdateBalance(balanceDTO);
        }
    }
}
