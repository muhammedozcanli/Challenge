using AutoMapper;
using Challenge.Persistence.DTOs;
using Challenge.Persistence.Manager.Abstract;
using Challenge.Persistence.Repositories.Abstract;

namespace Challenge.Persistence.Manager.Concrete
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserManager(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public UserDTO GetUser(Guid id)
        {
           var user = _userRepository.Get(p => p.Id == id);
           var userDTO = _mapper.Map<UserDTO>(user);
            return userDTO;
        }
    }
}
