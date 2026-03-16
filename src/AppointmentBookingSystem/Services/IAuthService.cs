using AppointmentBookingSystem.DTOs;

namespace AppointmentBookingSystem.Services;

public interface IAuthService
{
    Task<LoginResponse?> LoginAsync(LoginRequest request);
    string GenerateJwtToken(string username, string role, int userId);
}
