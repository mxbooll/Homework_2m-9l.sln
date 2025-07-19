using Homework_2m_9l.Configurations;
using Homework_2m_9l.Data;
using Homework_2m_9l.Data.Repository.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

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
app.MapControllers();

DbInitializer.InitDb(app);

app.Run();