using KaraHan.Application.DTOs;

namespace KaraHan.Application.Abstractions;

public interface IAuthService
{
    Task<AuthResponse> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default);
    Task<AuthResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default);
    Task<UserProfileDto?> GetProfileAsync(int userId, CancellationToken cancellationToken = default);
}
