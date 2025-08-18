using System;
using System.Text.Json.Serialization;

namespace Gerenciamento.Models
{
    public class EmailSchedule
    {
        public int Id { get; set; }
        public string Destinatario { get; set; }
        public string Assunto { get; set; }
        public string Corpo { get; set; }
        public DateTime DataProgramada { get; set; }
        public bool IsSent { get; set; } = false;
    }
}