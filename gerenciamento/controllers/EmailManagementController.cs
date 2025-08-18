using Gerenciamento.Models;
using Gerenciamento.Services;
using Microsoft.AspNetCore.Mvc;

namespace Gerenciamento.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailManagementController : ControllerBase
    {
        private readonly EmailManagementService _emailService;

        public EmailManagementController(EmailManagementService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost]
        public async Task<IActionResult> AddEmail([FromBody] EmailSchedule email)
        {
            await _emailService.AddEmailAsync(email);
            return Ok(new { message = "Email agendado com sucesso!" });
        }
    }
}