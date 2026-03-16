using System.ComponentModel.DataAnnotations;

namespace AppointmentBookingSystem.DTOs;

public class CreateSlotRequest
{
    
    public int DoctorId { get; set; }
    
    
    public DateTime StartTime { get; set; }
    
    
    public DateTime EndTime { get; set; }
}

public class SlotResponse
{
    public int Id { get; set; }
    public int DoctorId { get; set; }
    public string DoctorName { get; set; } = string.Empty;
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public bool IsAvailable { get; set; }
}
