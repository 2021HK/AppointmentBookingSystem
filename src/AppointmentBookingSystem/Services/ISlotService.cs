using AppointmentBookingSystem.DTOs;

namespace AppointmentBookingSystem.Services;

public interface ISlotService
{
    Task<int> CreateSlotAsync(CreateSlotRequest request);
    Task<IEnumerable<SlotResponse>> GetAvailableSlotsByDoctorAsync(int doctorId);
    Task<bool> DeleteSlotAsync(int slotId);
}
