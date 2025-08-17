using System.Net;
using System.Net.Mail;
using EmailSender.Models;

namespace EmailSender.Services
{
    public class EmailService
    {
        private readonly string _smtpServer;
        private readonly int _smtpPort;
        private readonly string _smtpUsername;
        private readonly string _smtpPassword;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IConfiguration config, ILogger<EmailService> logger)
        {
            _smtpServer = config["Smtp:Server"];
            _smtpPort = int.Parse(config["Smtp:Port"]);
            _smtpUsername = config["Smtp:Username"];
            _smtpPassword = config["Smtp:Password"];
            _logger = logger;
        }

        public async Task<(bool success, int attempts, string errorMessage)> SendEmailAsync(EmailRequest request)
        {
            int maxAttempts = 3;
            int[] delays = { 0, 5000, 25000 };
            _logger.LogInformation("Iniciando envio de email para: {To}", string.Join(", ", request.To));

            for (int attempt = 1; attempt <= maxAttempts; attempt++) 
            {
                try
                {
                    using var smtp = new SmtpClient(_smtpServer, _smtpPort)
                    {
                        Credentials = new NetworkCredential(_smtpUsername, _smtpPassword),
                        EnableSsl = true
                    };

                    var mail = new MailMessage
                    {
                        From = new MailAddress(_smtpUsername),
                        Subject = request.Subject,
                        Body = request.Body
                    };

                    foreach (var to in request.To)
                    {
                        mail.To.Add(to);
                    }

                    if (request.Attachments != null)
                    {
                        foreach (var att in request.Attachments)
                        {
                            var fileBytes = Convert.FromBase64String(att.ContentBase64);
                            var stream = new MemoryStream(fileBytes);
                            mail.Attachments.Add(new Attachment(stream, att.FileName, att.ContentType));
                        }
                    }
                    await smtp.SendMailAsync(mail);

                    _logger.LogInformation("Email enviado com sucesso na tentativa {Attempt}", attempt);
                    return (true, attempt, null);
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Falha ao enviar email na tentativa {Attempt}", attempt);

                    if (attempt < maxAttempts)
                        await Task.Delay(delays[attempt]);
                    else
                        return (false, attempt, ex.Message);
                }
            }
            return (false, maxAttempts, "Limite de reenvios excedidos");
        }
    }
}
