using Gerenciamento.Services;
using Gerenciamento.db;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddHttpClient<EmailManagementService>();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

var emailService = app.Services.GetRequiredService<EmailManagementService>();
_ = Task.Run(() => emailService.MonitorAndSendEmailsAsync());

app.Run();