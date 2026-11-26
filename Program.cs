var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<IWatchlistService, WatchlistService>();

var app = builder.Build();

app.MapControllers();

app.Run();