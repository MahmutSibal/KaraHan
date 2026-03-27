namespace KaraHan.Application.DTOs;

public record RegisterRequest(string FullName, string Email, string Password);

public record LoginRequest(string Email, string Password);

public record AuthResponse(string Token, DateTime ExpiresAtUtc);

public record UserProfileDto(int Id, string FullName, string Email, DateTime CreatedAtUtc);
