using System;
using System.Text.Json.Serialization;

namespace Gerenciamento.Models
{
    public class EmailSchedule
    {
        public Guid Id { get; set; }

        [JsonPropertyName("destinatario")]
        public string Destinatario { get; set; }

        [JsonPropertyName("assunto")]
        public string Assunto { get; set; }

        [JsonPropertyName("corpo")]
        public string Corpo { get; set; }

        [JsonPropertyName("data_programada")]
        public DateTime DataProgramada { get; set; }

        public bool IsSent { get; set; } = false;
    }
}
