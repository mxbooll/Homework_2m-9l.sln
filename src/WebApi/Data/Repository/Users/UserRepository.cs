using Homework_2m_9l.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace Homework_2m_9l.Data.Repository.Users;

public class UserRepository(WebServiceDbContext context) : IUserRepository
{
    public async Task AddAsync(User user)
    {
        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
    }

    public async Task<User> GetAsync(Guid userId)
    {
        return await context.Users.FirstOrDefaultAsync(a => a.Id == userId);
    }

    public async Task UpdateAsync(User user)
    {
        var existingUser = await context.Users.FindAsync(user.Id);
        if (existingUser == null)
        {
            throw new ArgumentException($"User with id {user.Id} not found");
        }

        context.Entry(existingUser).CurrentValues.SetValues(user);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid userId)
    {
        var user = await GetAsync(userId);
        if (user != null)
        {
            context.Users.Remove(user);
            await context.SaveChangesAsync();
        }
    }

    private async Task<bool> SaveChangesAsync()
    {
        return await context.SaveChangesAsync() > 0;
    }
}