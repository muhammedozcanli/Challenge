using Challenge.Persistance.Base.Entities;

namespace Challenge.Persistence.Entities
{
    public class Balance : BaseEntity
    {
        public Guid UserId { get; set; }
        public double? AvailableBalance { get; set; }
        public double? BlockedBalance { get; set; }
        public string? Currency { get; set; }
        public DateTime? LastUpdated { get; set; }
        public User User { get; set; }
    }
}
