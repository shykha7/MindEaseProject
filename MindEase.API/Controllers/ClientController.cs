using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MindEase.Application.DTOs.Client;
using MindEase.Application.Interfaces.Services;
using System.Security.Claims;

namespace MindEase.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Client,Therapist,Admin")]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        private int GetUserId() =>
    int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

        [HttpPost]
        public async Task<IActionResult> CreateClient([FromBody] CreateClientDto dto)
        {
            int userId = GetUserId();
            var clientId = await _clientService.CreateClientAsync(dto, userId);
            return Ok(new { ClientID = clientId });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var client = await _clientService.GetClientByIdAsync(id);
            if (client == null) return NotFound();
            return Ok(client);
        }

        [HttpGet("user")]
        public async Task<IActionResult> GetByUserId()
        {
            int userId = GetUserId();
            var client = await _clientService.GetClientByUserIdAsync(userId);
            if (client == null) return NotFound();
            return Ok(client);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var clients = await _clientService.GetAllClientsAsync();
            return Ok(clients);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateClientDto dto)
        {
            var updated = await _clientService.UpdateClientAsync(id, dto);
            if (!updated) return NotFound();
            return Ok(new { Message = "Client updated successfully" });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _clientService.DeleteClientAsync(id);
            if (!deleted) return NotFound();
            return Ok(new { Message = "Client deleted successfully" });
        }

    }
}
