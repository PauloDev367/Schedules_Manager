using Domain.Entities;
using Domain.Ports;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace DataEF.Repositories;

public class UserRepository : Repository, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<User> CreateAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User?> GetOneByIdAsync(Guid id)
    {
        return await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id.Equals(id));
    }

    public async Task<User?> GetOneByEmailAsync(string email)
    {
        return await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email.Equals(email));
    }

    public async Task<List<User>> GetAllAsync(int perPage, int page, string orderBy, string order)
    {
        List<User> data;

        IQueryable<User> query = _context.Users;
        var totalCount = await query.CountAsync();
        int skipAmount = page * perPage;
        query = query
            .OrderBy(orderBy + " " + order)
            .Skip(skipAmount)
            .Take(perPage);

        var totalPages = (int)Math.Ceiling((double)totalCount / perPage);
        var currentPage = page + 1;
        var nextPage = currentPage < totalPages ? currentPage + 1 : 1;
        var prevPage = currentPage > 1 ? currentPage - 1 : 1;

        data = await query.AsNoTracking().ToListAsync();
        return data;
    }

    public async Task<User> UpdateAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task DeleteAsync(User user)
    {
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }
}