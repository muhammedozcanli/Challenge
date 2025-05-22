using Challenge.Persistence.Entities;
using Challenge.Persistence.Repositories.Abstract;

namespace Challenge.Persistence.Repositories.Concrete
{
    public class PreOrderRepository : BaseRepository<PreOrder>, IPreOrderRepository
    {
        ChallengeDBContext dbContext;
        public PreOrderRepository(ChallengeDBContext tContext) : base(tContext)
        {
            dbContext = tContext;
        }
    }
}
