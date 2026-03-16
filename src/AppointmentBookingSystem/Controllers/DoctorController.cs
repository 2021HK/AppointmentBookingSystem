using AppointmentBookingSystem.DTOs;
using AppointmentBookingSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentBookingSystem.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DoctorController : ControllerBase
{
    private readonly IDoctorService _doctorService;

    public DoctorController(IDoctorService doctorService)
    {
        _doctorService = doctorService;
    }

    // GET: api/doctor
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var doctors = await _doctorService.GetAllDoctorsAsync();
        return Ok(doctors);
    }

    // GET: api/doctor/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var doctor = await _doctorService.GetDoctorByIdAsync(id);
        
        if (doctor == null)
            return NotFound(new { message = "Doctor not found" });

        return Ok(doctor);
    }

    // POST: api/doctor
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Add([FromBody] CreateDoctorRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var id = await _doctorService.CreateDoctorAsync(request);
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    // PUT: api/doctor/{id}
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateDoctorRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var success = await _doctorService.UpdateDoctorAsync(id, request);
        
        if (!success)
            return NotFound(new { message = "Doctor not found" });

        return Ok(new { message = "Doctor updated successfully" });
    }

    // DELETE: api/doctor/{id}
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _doctorService.DeleteDoctorAsync(id);
        
        if (!success)
            return NotFound(new { message = "Doctor not found" });

        return Ok(new { message = "Doctor deleted successfully" });
    }
}
