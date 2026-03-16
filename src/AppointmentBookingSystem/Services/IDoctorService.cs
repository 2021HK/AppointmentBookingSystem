using AppointmentBookingSystem.Models;
using AppointmentBookingSystem.DTOs;

namespace AppointmentBookingSystem.Services;

public interface IDoctorService
{
    Task<IEnumerable<Doctor>> GetAllDoctorsAsync();
    Task<Doctor?> GetDoctorByIdAsync(int id);
    Task<int> CreateDoctorAsync(CreateDoctorRequest request);
    Task<bool> UpdateDoctorAsync(int id, UpdateDoctorRequest request);
    Task<bool> DeleteDoctorAsync(int id);
}
