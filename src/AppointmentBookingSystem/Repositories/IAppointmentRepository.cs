using AppointmentBookingSystem.Models;
using AppointmentBookingSystem.DTOs;

namespace AppointmentBookingSystem.Repositories;

public interface IAppointmentRepository
{
    Task<IEnumerable<AppointmentResponse>> GetByUserIdAsync(int userId);
    Task<IEnumerable<AppointmentResponse>> GetByDoctorIdAsync(int doctorId);
    Task<AppointmentResponse?> GetByIdAsync(int id);
    Task<int> CreateAsync(Appointment appointment);
    Task<bool> UpdateStatusAsync(int id, string status);
}
