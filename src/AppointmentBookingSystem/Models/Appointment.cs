namespace AppointmentBookingSystem.Models;

public class Appointment
{
    public int Id { get; set; }
    public int SlotId { get; set; }
    public int UserId { get; set; }
    public int DoctorId { get; set; }
    public string PatientName { get; set; } = string.Empty;
    public string PatientEmail { get; set; } = string.Empty;
    public string PatientPhone { get; set; } = string.Empty;
    public string Status { get; set; } = "Booked"; // Booked, Cancelled, Completed
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; }
}
