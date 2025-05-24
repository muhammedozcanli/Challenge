using Challenge.Persistance.Base.Entities;

namespace Challenge.Persistence.Entities
{
    public class PreOrderProduct : BaseEntity
    {
        public Guid PreOrderId { get; set; }
        public PreOrder PreOrder { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public int Quantity { get; set; }
    }
} 