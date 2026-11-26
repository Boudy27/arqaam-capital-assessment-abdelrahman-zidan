public class WatchlistService : IWatchlistService
{
    private readonly List<WatchlistItem> _watchlist = new();

    public IEnumerable<WatchlistItem> GetAll()
    {
        return _watchlist;
    }

    public WatchlistItem Create(CreateWatchlistDto dto)
    {
        var symbol = dto.Symbol.Trim().ToUpperInvariant();

         if (_watchlist.Any(x => x.Symbol == symbol))
        {
            throw new InvalidOperationException(
                "This symbol is already in your watchlist.");
        }

        var item = new WatchlistItem
        {
            Symbol = symbol,
            TargetPrice = dto.TargetPrice,
            Note = dto.Note
        };

        _watchlist.Add(item);

        return item;
    }

   
public BestPairDto GetBestPair(IEnumerable<WatchlistItem> items, decimal targetTotal)
    {
        var itemsList = items
            .OrderBy(x => x.TargetPrice)
            .ToList();

        int left = 0;
        int right = itemsList.Count - 1;

        decimal? bestSum = null;

        while (left < right)
        {
            var sum = itemsList[left].TargetPrice +
                    itemsList[right].TargetPrice;

            if (sum <= targetTotal)
            {
                if (bestSum == null || sum > bestSum.Value)
                    bestSum = sum;

                left++;
            }
            else
            {
                right--;
            }
        }

        if (bestSum == null)
        {
            return new BestPairDto
            {
                BestPairs = [],
                CombinedTargetPrice = null,
                Message = "No matching pair"
            };
        }

        var priceGroups = itemsList
            .GroupBy(x => x.TargetPrice)
            .ToDictionary(
                g => g.Key,
                g => g.ToList());

        // Sort items alphabetically
        var alphabeticalItems = itemsList
            .OrderBy(
                x => x.Symbol,
                StringComparer.OrdinalIgnoreCase)
            .ToList();

        WatchlistItem? first = null;
        WatchlistItem? second = null;

        for (int i = 0; i < alphabeticalItems.Count; i++)
        {
            var item = alphabeticalItems[i];

            var requiredPrice = bestSum.Value - item.TargetPrice;

            if (!priceGroups.TryGetValue(
                    requiredPrice,
                    out var matches))
                continue;

            var match = matches
                .Where(x => !ReferenceEquals(x, item))
                .OrderBy(
                    x => x.Symbol,
                    StringComparer.OrdinalIgnoreCase)
                .FirstOrDefault();

            if (match != null)
            {
                first = item;
                second = match;
                break;
            }
        }

        return new BestPairDto
        {
            BestPairs = [first!, second!],
            CombinedTargetPrice = bestSum.Value
        };
    }

}