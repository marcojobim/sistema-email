using Gerenciamento.db;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://0.0.0.0:8080");

// Recupera a string de conexão do .env (variável de ambiente)
var connectionString = builder.Configuration["DATABASE_CONNECTION"];

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString)
);




// Adiciona serviços de controllers, Swagger, etc
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Adiciona o HttpClient para ser injetado como dependencia
builder.Services.AddHttpClient();

// Adiciona o EmailManagementService e suas dependencias (HttpClient e IConfiguration)
builder.Services.AddScoped<Gerenciamento.Services.EmailManagementService>();
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

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Mude a ordem de "app.UseCors()" para vir antes de "app.UseAuthorization()"
app.UseCors();

//app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();