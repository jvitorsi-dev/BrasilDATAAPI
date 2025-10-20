using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.DTOs.CPF
{
    public class CPFResult
    {
        public string Numero { get; set; } = string.Empty;
        public bool Valido { get; set; }
        public string Tipo { get; set; } = "Pessoa Física";
        public string Mensagem { get; set; } = string.Empty;
        public int ScoreFicticio { get; set; }

    }
}
