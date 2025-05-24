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
        /// <summary>
        /// Retrieves a user by their unique identifier and maps the result to a UserDTO.
        /// </summary>
        /// <param name="id">The unique identifier of the user.</param>
        /// <returns>A UserDTO containing the user's information.</returns>
        public UserDTO GetUser(Guid id)
        {
            var user = _userRepository.Get(p => p.Id == id);
            var userDTO = _mapper.Map<UserDTO>(user);
            return userDTO;
        }

    }
}
