public interface IWatchlistService
{
    IEnumerable<WatchlistItem> GetAll();

    WatchlistItem Create(CreateWatchlistDto dto);

    BestPairDto GetBestPair(IEnumerable<WatchlistItem> items, decimal targetTotal);
}