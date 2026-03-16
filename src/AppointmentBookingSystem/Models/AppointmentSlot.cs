namespace AppointmentBookingSystem.Models;

public class AppointmentSlot
{
    public int Id { get; set; }
    public int DoctorId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public bool IsAvailable { get; set; }
    public DateTime CreatedAt { get; set; }
}
