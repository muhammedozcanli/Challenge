using AutoMapper;
using Challenge.Persistence.DTOs;
using Challenge.Persistence.Entities;
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
    public class BalanceManager : IBalanceManager
    {
        IBalanceRepository _balanceRepository;
        IMapper _mapper;
        public BalanceManager(IBalanceRepository balanceRepository, IMapper mapper)
        {
            _balanceRepository = balanceRepository;
            _mapper = mapper;
        }
        /// <summary>
        /// Retrieves a list of all user balances from the database and maps them to BalanceDTOs.
        /// </summary>
        /// <returns>A collection of <see cref="BalanceDTO"/> representing user balances.</returns>
        public IEnumerable<BalanceDTO> GetBalances()
        {
            var balances = _balanceRepository.GetList();
            var balanceDTO = _mapper.Map<List<BalanceDTO>>(balances);
            return balanceDTO;
        }

        /// <summary>
        /// Updates the specified balance in the database using the provided <see cref="BalanceDTO"/>.
        /// </summary>
        /// <param name="balanceDTO">The balance data transfer object containing updated balance values.</param>
        /// <returns>True if the update is successful; otherwise, false.</returns>
        public bool UpdateBalance(BalanceDTO balanceDTO)
        {
            try
            {
                var balance = _balanceRepository.Get(p => p.Id == balanceDTO.Id);

                if (balance == null)
                    return false;

                _mapper.Map(balanceDTO, balance);

                _balanceRepository.Update(balance);
                _balanceRepository.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Retrieves the balance information for the specified user by their unique identifier.
        /// </summary>
        /// <param name="userId">The unique identifier of the user whose balance is to be retrieved.</param>
        /// <returns>A BalanceDTO object containing the user's balance details.</returns>
        public BalanceDTO GetBalanceByUserId(Guid userId)
        {
            var balance = _balanceRepository.Get(p => p.UserId == userId);
            var balanceDTO = _mapper.Map<BalanceDTO>(balance);
            return balanceDTO;
        }


    }
}
