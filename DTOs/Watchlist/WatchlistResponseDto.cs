public class WatchlistResponseDto
{
    public int Id { get; set; }

    public string Symbol { get; set; } = string.Empty;

    public decimal TargetPrice { get; set; }

    public string? Note { get; set; }
}