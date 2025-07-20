using Homework_2m_9l.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace Homework_2m_9l.Data;

public class WebServiceDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
}