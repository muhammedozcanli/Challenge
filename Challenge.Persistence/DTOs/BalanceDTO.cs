namespace Challenge.Persistence.DTOs
{
    public record BalanceDTO
    {
        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
        public double? AvailableBalance { get; set; }
        public double? BlockedBalance { get; set; }
        public string? Currency { get; set; }
        public DateTime? LastUpdated { get; set; }
    }
}
