using AppointmentBookingSystem.Models;
using AppointmentBookingSystem.DTOs;
using AppointmentBookingSystem.Repositories;

namespace AppointmentBookingSystem.Services;

public class SlotService : ISlotService
{
    private readonly IAppointmentSlotRepository _slotRepository;
    private readonly IDoctorRepository _doctorRepository;

    public SlotService(IAppointmentSlotRepository slotRepository, IDoctorRepository doctorRepository)
    {
        _slotRepository = slotRepository;
        _doctorRepository = doctorRepository;
    }

    public async Task<int> CreateSlotAsync(CreateSlotRequest request)
    {
        var slot = new AppointmentSlot
        {
            DoctorId = request.DoctorId,
            StartTime = request.StartTime,
            EndTime = request.EndTime,
            IsAvailable = true,
            CreatedAt = DateTime.UtcNow
        };

        return await _slotRepository.CreateAsync(slot);
    }

    public async Task<IEnumerable<SlotResponse>> GetAvailableSlotsByDoctorAsync(int doctorId)
    {
        var slots = await _slotRepository.GetAvailableSlotsByDoctorAsync(doctorId);
        var doctor = await _doctorRepository.GetByIdAsync(doctorId);

        return slots.Select(s => new SlotResponse
        {
            Id = s.Id,
            DoctorId = s.DoctorId,
            DoctorName = doctor?.Name ?? "",
            StartTime = s.StartTime,
            EndTime = s.EndTime,
            IsAvailable = s.IsAvailable
        });
    }

    public async Task<bool> DeleteSlotAsync(int slotId)
    {
        return await _slotRepository.DeleteAsync(slotId);
    }
}
