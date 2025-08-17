using EmailSender.Models;
using Microsoft.AspNetCore.Mvc;
using EmailSender.Services;

namespace EmailSender.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmailController : ControllerBase
{
    private readonly EmailService _emailService;

    public EmailController(EmailService emailService)
    {
        _emailService = emailService;
    }

    [HttpPost]
    public async Task<IActionResult> SendEmail([FromBody] EmailRequest request)
    {
        var (success, attempts, error) = await _emailService.SendEmailAsync(request);

        if (success)
        {
            return Ok(new
            {
                messageId = request.MessageId,
                status = "Sucesso",
                tentativas = attempts,
                mensagem = "Email enviado com sucesso"
            });
        }
        else
        {
            return StatusCode(500, new
            {
                messageId = request.MessageId,
                status = "Falha",
                tentativas = attempts,
                erro = error
            });
        }
    }
}