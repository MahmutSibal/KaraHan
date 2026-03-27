using KaraHan.Application.Abstractions;
using KaraHan.Domain.Entities;
using KaraHan.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace KaraHan.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _dbContext;

    public UserRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<AppUser?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        var normalizedEmail = email.Trim().ToLowerInvariant();
        return await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == normalizedEmail, cancellationToken);
    }

    public Task<AppUser?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        => _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public async Task AddAsync(AppUser user, CancellationToken cancellationToken = default)
        => await _dbContext.Users.AddAsync(user, cancellationToken);

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        => _dbContext.SaveChangesAsync(cancellationToken);
}
