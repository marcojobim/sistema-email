using EmailSender.Services;
using EmailSender.Config;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://0.0.0.0:5000");

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
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCors();
app.MapControllers();
app.Run();
