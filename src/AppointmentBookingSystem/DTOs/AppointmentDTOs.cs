using System.ComponentModel.DataAnnotations;

namespace AppointmentBookingSystem.DTOs;

public class BookAppointmentRequest
{
    
    public int SlotId { get; set; }
    
    
    public string PatientName { get; set; } = string.Empty;
    
    
    public string PatientEmail { get; set; } = string.Empty;
    
   
    public string PatientPhone { get; set; } = string.Empty;
    
    
    public string? Notes { get; set; }
}

public class AppointmentResponse
{
    public int Id { get; set; }
    public int SlotId { get; set; }
    public string DoctorName { get; set; } = string.Empty;
    public string Specialization { get; set; } = string.Empty;
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string PatientName { get; set; } = string.Empty;
    public string PatientEmail { get; set; } = string.Empty;
    public string PatientPhone { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; }
}
