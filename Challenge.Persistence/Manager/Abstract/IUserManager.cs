using Challenge.Persistence.DTOs;

namespace Challenge.Persistence.Manager.Abstract
{
    public interface IUserManager
    {
        UserDTO GetUser(Guid id);
    }
}
