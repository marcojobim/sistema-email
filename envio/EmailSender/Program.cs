using EmailSender.Services;
using EmailSender.Config;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var smtpSettings = new SmtpSettings
{
    Host = Environment.GetEnvironmentVariable("SMTP_HOST") ?? "smtp.gmail.com",
    Port = int.Parse(Environment.GetEnvironmentVariable("SMTP_PORT") ?? "587"),
    User = Environment.GetEnvironmentVariable("SMTP_USER") ?? "",
    Pass = Environment.GetEnvironmentVariable("SMTP_PASS") ?? ""
};
builder.Services.AddSingleton(smtpSettings);
builder.Services.AddSingleton<EmailService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
