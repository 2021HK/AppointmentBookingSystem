using AppointmentBookingSystem.DTOs;
using AppointmentBookingSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentBookingSystem.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SlotController : ControllerBase
{
    private readonly ISlotService _slotService;

    public SlotController(ISlotService slotService)
    {
        _slotService = slotService;
    }

    // GET: api/slot/doctor/{doctorId}
    [HttpGet("doctor/{doctorId}")]
    public async Task<IActionResult> GetAvailableSlotsByDoctor(int doctorId)
    {
        var slots = await _slotService.GetAvailableSlotsByDoctorAsync(doctorId);
        return Ok(slots);
    }

    // POST: api/slot
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Add([FromBody] CreateSlotRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var id = await _slotService.CreateSlotAsync(request);
        return Ok(new { id, message = "Slot created successfully" });
    }

    // DELETE: api/slot/{id}
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _slotService.DeleteSlotAsync(id);
        
        if (!success)
            return NotFound(new { message = "Slot not found" });

        return Ok(new { message = "Slot deleted successfully" });
    }
}
