using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BLL.Models.DTOs.CPF
{
    public class CpfRequest
    {
        [JsonPropertyName("cpf")]
        public string Cpf { get; set; } = string.Empty;

        [JsonPropertyName("dataNascimento")]
        public DateTime? DataNascimento { get; set; }

        [JsonPropertyName("cep")]
        public string? Cep { get; set; }

        [JsonPropertyName("rendaBracket")]
        public int? RendaBracket { get; set; } = null;
    }
}
