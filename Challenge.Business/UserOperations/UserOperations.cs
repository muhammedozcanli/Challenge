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
        public UserDTO GetUser(Guid id)
        {
            return _userManager.GetUser(id);
        }
    }
}
