using Gerenciamento.db;
using Gerenciamento.Models;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace Gerenciamento.Services
{
    public class EmailManagementService
    {
        private readonly HttpClient _httpClient;
        private readonly AppDbContext _dbContext;

        public EmailManagementService(HttpClient httpClient, AppDbContext dbContext)
        {
            _httpClient = httpClient;
            _dbContext = dbContext;
        }

        public async Task AddEmailAsync(EmailSchedule email)
        {
            _dbContext.EmailSchedules.Add(email);
            await _dbContext.SaveChangesAsync();
        }

        public async Task MonitorAndSendEmailsAsync()
        {
            while (true)
            {
                var now = DateTime.UtcNow;
                var emailsToSend = await _dbContext.EmailSchedules
                    .Where(e => e.DataProgramada <= now && !e.IsSent)
                    .ToListAsync();

                foreach (var email in emailsToSend)
                {
                    var json = JsonSerializer.Serialize(email);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    try
                    {
                        var response = await _httpClient.PostAsync("http://envio:3000/api/Email", content);
                        var responseContent = await response.Content.ReadAsStringAsync();

                        _dbContext.EmailResponses.Add(new EmailResponse
                        {
                            EmailScheduleId = email.Id,
                            Status = response.IsSuccessStatusCode ? "Sucesso" : "Falha",
                            Attempts = 1,
                            ResponseMessage = responseContent
                        });

                        email.IsSent = true;
                    }
                    catch (Exception ex)
                    {
                        _dbContext.EmailResponses.Add(new EmailResponse
                        {
                            EmailScheduleId = email.Id,
                            Status = "Erro",
                            Attempts = 1,
                            ResponseMessage = ex.Message
                        });
                    }
                }

                await _dbContext.SaveChangesAsync();
                await Task.Delay(1000); // Espera 1 segundo antes de verificar novamente
            }
        }
    }
}