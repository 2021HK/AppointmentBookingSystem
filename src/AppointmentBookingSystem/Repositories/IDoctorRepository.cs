using AppointmentBookingSystem.Models;

namespace AppointmentBookingSystem.Repositories;

public interface IDoctorRepository
{
    Task<IEnumerable<Doctor>> GetAllAsync();
    Task<Doctor?> GetByIdAsync(int id);
    Task<int> CreateAsync(Doctor doctor);
    Task<bool> UpdateAsync(Doctor doctor);
    Task<bool> DeleteAsync(int id);
}
