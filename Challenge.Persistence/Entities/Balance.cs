using Challenge.Persistance.Base.Entities;

namespace Challenge.Persistence.Entities
{
    public class Balance : BaseEntity
    {
        public Guid? UserId { get; set; }
        public long? AvailableBalance { get; set; }
        public long? BlockedBalance { get; set; }
        public string? Currency { get; set; }
        public DateTime? LastUpdated { get; set; }
    }
}
