// filepath: Gerenciamento/db/AppDbContext.cs
using Gerenciamento.Models;
using Microsoft.EntityFrameworkCore;

namespace Gerenciamento.db
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<EmailSchedule> EmailSchedules { get; set; }
        public DbSet<EmailResponse> EmailResponses { get; set; }
    }
}