using Challenge.Persistence.Entities;
using Challenge.Persistence.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Persistence.Repositories.Concrete
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        ChallengeDBContext dbContext;
        public UserRepository(ChallengeDBContext tContext) : base(tContext)
        {
            dbContext = tContext;
        }
    }
}
