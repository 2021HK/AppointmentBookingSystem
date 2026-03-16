using AppointmentBookingSystem.Models;

namespace AppointmentBookingSystem.Repositories;

public interface IAppointmentSlotRepository
{
    Task<IEnumerable<AppointmentSlot>> GetAvailableSlotsByDoctorAsync(int doctorId);
    Task<AppointmentSlot?> GetByIdAsync(int id);
    Task<int> CreateAsync(AppointmentSlot slot);
    Task<bool> UpdateAvailabilityAsync(int id, bool isAvailable);
    Task<bool> DeleteAsync(int id);
}
