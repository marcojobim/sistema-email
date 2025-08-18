using System;

namespace Gerenciamento.Models
{
    public class EmailSchedule
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Recipient { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime SendTime { get; set; }
        public bool IsSent { get; set; } = false;
    }
}