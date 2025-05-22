using Challenge.Persistence.Entities;
using Challenge.Persistence.Repositories.Abstract;

namespace Challenge.Persistence.Repositories.Concrete
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        ChallengeDBContext dbContext;
        public ProductRepository(ChallengeDBContext tContext) : base(tContext)
        {
            dbContext = tContext;
        }
    }
}
