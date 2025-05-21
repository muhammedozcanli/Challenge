using Challenge.Persistance.Base.Entities;

namespace Challenge.Persistence.Entities
{
    public class Balance : BaseEntity
    {
        public int? UserId { get; set; }
        public int? AvailableBalance { get; set; }
        public int? BlockedBalance { get; set; }
        public string? Currency { get; set; }
        public DateTime? LastUpdated { get; set; }
    }
}
