using Challenge.Persistance.Base.Entities;
using System.Collections.Generic;

namespace Challenge.Persistence.Entities
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string Password { get; set; }
        public Balance Balance { get; set; }
        public ICollection<PreOrder> PreOrders { get; set; }
    }
}
