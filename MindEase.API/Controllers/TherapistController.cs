using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MindEase.Application.DTOs.Therapist;
using MindEase.Application.Interfaces.Services;
using System.Security.Claims;

namespace MindEase.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Therapist,Admin")]

    public class TherapistController : ControllerBase
    {
        private readonly ITherapistService _therapistService;

        public TherapistController(ITherapistService therapistService)
        {
            _therapistService = therapistService;
        }

        private int GetUserId() =>
            int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

        [HttpPost]
        public async Task<IActionResult> CreateTherapist([FromBody] CreateTherapistDto dto)
        {
            int userId = GetUserId();

            var therapistId = await _therapistService.CreateTherapistAsync(dto, userId);

            return Ok(new { TherapistID = therapistId });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var therapist = await _therapistService.GetTherapistByIdAsync(id);
            if (therapist == null) return NotFound();

            return Ok(therapist);
        }

        [HttpGet("user")]
        public async Task<IActionResult> GetByUserId()
        {
            int userId = GetUserId();
            var therapist = await _therapistService.GetTherapistByUserIdAsync(userId);
            if (therapist == null) return NotFound();

            return Ok(therapist);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var list = await _therapistService.GetAllTherapistsAsync();
            return Ok(list);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateTherapistDto dto)
        {
            var updated = await _therapistService.UpdateTherapistAsync(id, dto);
            if (!updated) return NotFound();

            return Ok(new { Message = "Therapist updated successfully" });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _therapistService.DeleteTherapistAsync(id);
            if (!deleted) return NotFound();

            return Ok(new { Message = "Therapist deleted successfully" });
        }
    }
}
