var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/health/", () => new { status = "OK" });

app.Run();