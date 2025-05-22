using Challenge.Persistence.Entities;
using Challenge.Persistence.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Persistence.Repositories.Concrete
{
    public class ErrorRepository : BaseRepository<Error>, IErrorRepository
    {
        ChallengeDBContext dbContext;
        public ErrorRepository(ChallengeDBContext tContext) : base(tContext)
        {
            dbContext = tContext;
        }
    }
}
