using Domain.Entities;

namespace Domain.Ports;

public interface IUserRepository
{
    public Task<User> CreateAsync(User user);
    public Task<User?> GetOneByIdAsync(Guid id);
    public Task<User?> GetOneByEmailAsync(string email);
    public Task<List<User>> GetAllAsync(int perPage, int page, string orderBy, string order);
    public Task<User> UpdateAsync(User user);
    public Task DeleteAsync(User user);
}