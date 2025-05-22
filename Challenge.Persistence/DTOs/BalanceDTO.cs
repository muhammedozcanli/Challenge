namespace Challenge.Persistence.DTOs
{
    public record BalanceDTO
    {
        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
        public long? AvailableBalance { get; set; }
        public long? BlockedBalance { get; set; }
        public string? Currency { get; set; }
        public DateTime? LastUpdated { get; set; }
    }
}
