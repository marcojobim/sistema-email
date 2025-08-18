using System;

namespace Gerenciamento.Models
{
    public class EmailResponse
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid EmailScheduleId { get; set; }
        public string Status { get; set; }
        public int Attempts { get; set; }
        public string ResponseMessage { get; set; }
        public DateTime ResponseTime { get; set; } = DateTime.UtcNow;
    }
}