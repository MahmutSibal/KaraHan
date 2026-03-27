using KaraHan.Domain.Entities;

namespace KaraHan.Application.Abstractions;

public interface ITokenService
{
    (string Token, DateTime ExpiresAtUtc) CreateToken(AppUser user);
}
