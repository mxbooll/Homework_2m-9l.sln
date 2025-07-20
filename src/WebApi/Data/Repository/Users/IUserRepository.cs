using Homework_2m_9l.Entities.Users;

namespace Homework_2m_9l.Data.Repository.Users;

public interface IUserRepository
{
    Task<Guid> AddAsync(User user);
    Task<User> GetAsync(Guid userId);
    Task UpdateAsync(User user);
    Task DeleteAsync(Guid userId);
}