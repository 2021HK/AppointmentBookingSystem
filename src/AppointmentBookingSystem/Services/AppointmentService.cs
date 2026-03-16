using AppointmentBookingSystem.Models;
using AppointmentBookingSystem.DTOs;
using AppointmentBookingSystem.Repositories;

namespace AppointmentBookingSystem.Services;

public class AppointmentService : IAppointmentService
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IAppointmentSlotRepository _slotRepository;

    public AppointmentService(
        IAppointmentRepository appointmentRepository,
        IAppointmentSlotRepository slotRepository)
    {
        _appointmentRepository = appointmentRepository;
        _slotRepository = slotRepository;
    }

    public async Task<int> BookAppointmentAsync(int userId, BookAppointmentRequest request)
    {
        var slot = await _slotRepository.GetByIdAsync(request.SlotId);
        if (slot == null || !slot.IsAvailable)
            throw new InvalidOperationException("Slot is not available");

        var appointment = new Appointment
        {
            SlotId = request.SlotId,
            UserId = userId,
            DoctorId = slot.DoctorId,
            PatientName = request.PatientName,
            PatientEmail = request.PatientEmail,
            PatientPhone = request.PatientPhone,
            Status = "Booked",
            Notes = request.Notes,
            CreatedAt = DateTime.UtcNow
        };

        var appointmentId = await _appointmentRepository.CreateAsync(appointment);
        await _slotRepository.UpdateAvailabilityAsync(request.SlotId, false);

        return appointmentId;
    }

    public async Task<IEnumerable<AppointmentResponse>> GetUserAppointmentsAsync(int userId)
    {
        return await _appointmentRepository.GetByUserIdAsync(userId);
    }

    public async Task<IEnumerable<AppointmentResponse>> GetDoctorScheduleAsync(int doctorId)
    {
        return await _appointmentRepository.GetByDoctorIdAsync(doctorId);
    }

    public async Task<bool> CancelAppointmentAsync(int appointmentId, int userId)
    {
        var appointment = await _appointmentRepository.GetByIdAsync(appointmentId);
        if (appointment == null) return false;

        var success = await _appointmentRepository.UpdateStatusAsync(appointmentId, "Cancelled");
        if (success)
        {
            await _slotRepository.UpdateAvailabilityAsync(appointment.SlotId, true);
        }
        return success;
    }
}
