using Challenge.Persistence.DTOs;
using Challenge.Persistence.Manager.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Business.UserOperations
{
    public class UserOperations : IUserOperations
    {
        private readonly IUserManager _userManager;

        public UserOperations(IUserManager userManager)
        {
            _userManager = userManager;
        }
        /// <summary>
        /// Retrieves the user with the specified <paramref name="id"/>.
        /// </summary>
        /// <param name="id">The unique identifier (GUID) of the user.</param>
        /// <returns>A <see cref="UserDTO"/> object containing user information.</returns>
        public UserDTO GetUser(Guid id)
        {
            return _userManager.GetUser(id);
        }

    }
}
