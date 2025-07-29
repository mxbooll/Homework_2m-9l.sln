using Homework_2m_9l.Configurations;
using Homework_2m_9l.Data;
using Homework_2m_9l.Data.Repository.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Prometheus;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Services.Configure<PostgresConfig>(config =>
{
    config.Host = builder.Configuration["POSTGRES_HOST"];
    config.Port = builder.Configuration["POSTGRES_PORT"];
    config.Database = builder.Configuration["POSTGRES_DB"];
    config.Username = builder.Configuration["POSTGRES_USER"];
    config.Password = builder.Configuration["POSTGRES_PASSWORD"];
});

builder.Services.AddDbContext<WebServiceDbContext>((services, opt) =>
{
    if (builder.Environment.IsDevelopment())
    {
        opt.UseNpgsql(builder.Configuration.GetConnectionString("PostgresOtusConnection"));
    }
    else
    {
        var pgConfig = services.GetRequiredService<IOptions<PostgresConfig>>().Value;
        opt.UseNpgsql(pgConfig.GetConnectionString());
    }
});

builder.Services.AddScoped<IUserRepository, UserRepository>();

var app = builder.Build();

var customHistogram = Metrics.CreateHistogram(
    "custom_request_duration_seconds",
    "Custom duration histogram",
    new HistogramConfiguration
    {
        Buckets = [0.01, 0.05, 0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.8],
        LabelNames = ["method", "route", "code"]
    });

app.Use(async (context, next) =>
{
    var stopwatch = System.Diagnostics.Stopwatch.StartNew();
    try
    {
        await next();
        stopwatch.Stop();
        
        customHistogram
            .WithLabels(context.Request.Method, context.Request.Path, context.Response.StatusCode.ToString())
            .Observe(stopwatch.Elapsed.TotalSeconds);
    }
    catch
    {
        stopwatch.Stop();
        customHistogram
            .WithLabels(context.Request.Method, context.Request.Path, "500")
            .Observe(stopwatch.Elapsed.TotalSeconds);
        throw;
    }
});

app.UseHttpMetrics(options =>
{
    options.RequestDuration.Enabled = true;
    options.RequestCount.Enabled = true;
    options.InProgress.Enabled = true;
    
    options.AddCustomLabel("route", context => context.Request.Path);
});

// app.UseMetricServer(port: 9000); 
app.MapMetrics();     // эндпоинт /metrics

app.MapControllers();

DbInitializer.InitDb(app);

app.Run();