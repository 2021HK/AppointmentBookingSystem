using AppointmentBookingSystem.DTOs;

namespace AppointmentBookingSystem.Services;

public interface IAppointmentService
{
    Task<int> BookAppointmentAsync(int userId, BookAppointmentRequest request);
    Task<IEnumerable<AppointmentResponse>> GetUserAppointmentsAsync(int userId);
    Task<IEnumerable<AppointmentResponse>> GetDoctorScheduleAsync(int doctorId);
    Task<bool> CancelAppointmentAsync(int appointmentId, int userId);
}
