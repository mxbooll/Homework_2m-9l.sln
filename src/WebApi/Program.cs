using Homework_2m_9l.Data;
using Homework_2m_9l.Data.Repository.Users;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Services.AddDbContext<WebServiceDbContext>(opt =>
{
    opt.UseNpgsql(builder.Configuration.GetConnectionString("PostgresOtusConnection"));
});

builder.Services.AddScoped<IUserRepository, UserRepository>();

var app = builder.Build();
app.MapControllers();

DbInitializer.InitDb(app);

app.Run();