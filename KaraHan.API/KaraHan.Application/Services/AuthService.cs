using KaraHan.Application.Abstractions;
using KaraHan.Application.DTOs;
using KaraHan.Domain.Entities;

namespace KaraHan.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenService _tokenService;

    public AuthService(IUserRepository userRepository, IPasswordHasher passwordHasher, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _tokenService = tokenService;
    }

    public async Task<AuthResponse> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default)
    {
        var existingUser = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);
        if (existingUser is not null)
        {
            throw new InvalidOperationException("Bu e-posta adresi zaten kayitli.");
        }

        var (hash, salt) = _passwordHasher.Hash(request.Password);
        var user = new AppUser
        {
            FullName = request.FullName.Trim(),
            Email = request.Email.Trim().ToLowerInvariant(),
            PasswordHash = hash,
            PasswordSalt = salt
        };

        await _userRepository.AddAsync(user, cancellationToken);
        await _userRepository.SaveChangesAsync(cancellationToken);

        var (token, expiresAt) = _tokenService.CreateToken(user);
        return new AuthResponse(token, expiresAt);
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);
        if (user is null || !_passwordHasher.Verify(request.Password, user.PasswordHash, user.PasswordSalt))
        {
            throw new UnauthorizedAccessException("E-posta veya sifre hatali.");
        }

        var (token, expiresAt) = _tokenService.CreateToken(user);
        return new AuthResponse(token, expiresAt);
    }

    public async Task<UserProfileDto?> GetProfileAsync(int userId, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetByIdAsync(userId, cancellationToken);
        return user is null
            ? null
            : new UserProfileDto(user.Id, user.FullName, user.Email, user.CreatedAtUtc);
    }
}
