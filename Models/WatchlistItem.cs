public class WatchlistItem
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Symbol { get; set; } = string.Empty;

    public decimal TargetPrice { get; set; }

    public string? Note { get; set; }
}