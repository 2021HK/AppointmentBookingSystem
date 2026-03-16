using System.ComponentModel.DataAnnotations;

namespace AppointmentBookingSystem.DTOs;

public class CreateDoctorRequest
{
    
    public string Name { get; set; } = string.Empty;
    
   
    public string Specialization { get; set; } = string.Empty;
    
    
    public string Email { get; set; } = string.Empty;
    
    
    public string Phone { get; set; } = string.Empty;
    
    public string? Username { get; set; }
    public string? Password { get; set; }
}

public class UpdateDoctorRequest
{
    
    public string Name { get; set; } = string.Empty;
    
    
    public string Specialization { get; set; } = string.Empty;
    
    
    public string Email { get; set; } = string.Empty;
    
   
    public string Phone { get; set; } = string.Empty;
}
