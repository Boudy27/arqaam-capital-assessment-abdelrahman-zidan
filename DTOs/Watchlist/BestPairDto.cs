public record BestPairDto
{
    public IEnumerable<WatchlistItem> BestPairs { get; set; } = new List<WatchlistItem>();

    public decimal? CombinedTargetPrice { get; set; }

    public string? Message { get; set; }
}