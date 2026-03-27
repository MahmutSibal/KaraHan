using KaraHan.Domain.Entities;

namespace KaraHan.Application.Abstractions;

public interface IUserRepository
{
    Task<AppUser?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<AppUser?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task AddAsync(AppUser user, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
