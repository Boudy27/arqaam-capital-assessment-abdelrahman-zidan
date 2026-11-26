using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/watchlist-items")]
public class WatchlistController : ControllerBase
{
    private readonly IWatchlistService _watchlistService;

    public WatchlistController(IWatchlistService watchlistService)
    {
        _watchlistService = watchlistService;
    }

    // GET: api/watchlist
    [HttpGet]
    public IActionResult GetAll()
    {
        var items = _watchlistService.GetAll();
        return Ok(items);
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateWatchlistDto dto)
    {
            try
        {
            var item = _watchlistService.Create(dto);

            return Created("",item);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    [HttpGet("best-pair")]
    public IActionResult GetBestPair([FromQuery] decimal targetTotal)
    {
        if (targetTotal <= 0)
        {
            return BadRequest(new { message = "Target total must be greater than zero." });
        }

        var items = _watchlistService.GetAll().ToList();

        var bestPair = _watchlistService.GetBestPair(items, targetTotal);

        return Ok(bestPair);
    }
}