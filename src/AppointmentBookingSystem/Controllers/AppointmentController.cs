using AppointmentBookingSystem.DTOs;
using AppointmentBookingSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AppointmentBookingSystem.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class AppointmentController : ControllerBase
{
    private readonly IAppointmentService _appointmentService;

    public AppointmentController(IAppointmentService appointmentService)
    {
        _appointmentService = appointmentService;
    }

    // POST: api/appointment
    [HttpPost]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> Book([FromBody] BookAppointmentRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
        
        try
        {
            var id = await _appointmentService.BookAppointmentAsync(userId, request);
            return Ok(new { id, message = "Appointment booked successfully" });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    // GET: api/appointment/my
    [HttpGet("my")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> GetMyAppointments()
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
        var appointments = await _appointmentService.GetUserAppointmentsAsync(userId);
        return Ok(appointments);
    }

    // GET: api/appointment/doctor/{doctorId}
    [HttpGet("doctor/{doctorId}")]
    [Authorize(Roles = "Doctor,Admin")]
    public async Task<IActionResult> GetDoctorSchedule(int doctorId)
    {
        var appointments = await _appointmentService.GetDoctorScheduleAsync(doctorId);
        return Ok(appointments);
    }

    // DELETE: api/appointment/{id}
    [HttpDelete("{id}")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> Cancel(int id)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
        var success = await _appointmentService.CancelAppointmentAsync(id, userId);
        
        if (!success)
            return NotFound(new { message = "Appointment not found" });

        return Ok(new { message = "Appointment cancelled successfully" });
    }
}
