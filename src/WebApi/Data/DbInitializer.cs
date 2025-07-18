using Microsoft.EntityFrameworkCore;

namespace Homework_2m_9l.Data;

public static class DbInitializer
{
    public static void InitDb(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        SeedData(scope.ServiceProvider.GetService<WebServiceDbContext>());
    }

    private static void SeedData(WebServiceDbContext context)
    {
        context.Database.Migrate();
    }
}