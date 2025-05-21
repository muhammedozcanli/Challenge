namespace Challenge.Persistence.DTOs
{
    public record BalanceDTO
    {
        public Guid Id { get; set; }
        public int? UserId { get; set; }
        public int? AvailableBalance { get; set; }
        public int? BlockedBalance { get; set; }
        public string? Currency { get; set; }
        public DateTime? LastUpdated { get; set; }
    }
}
