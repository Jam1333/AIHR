using Domain.Entities;

namespace Domain.Abstractions.Repositories;

public interface IUserRepository
{
    Task CreateAsync(User user);
    Task<List<User>> GetAllAsync();
    Task<User?> GetByIdAsync(Guid id);
    Task<User?> GetByEmailAsync(string email);
    Task<bool> ExistsWithIdAsync(Guid id);
    Task<bool> ExistsWithEmailAsync(string email);
    Task UpdateAsync(User user);
    Task DeleteAsync(Guid id);
}
