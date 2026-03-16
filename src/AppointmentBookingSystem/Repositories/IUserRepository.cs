using AppointmentBookingSystem.Models;

namespace AppointmentBookingSystem.Repositories;

public interface IUserRepository
{
    Task<User?> GetByUsernameAsync(string username);
    Task<User?> GetByIdAsync(int id);
    Task<int> CreateAsync(User user);
}
