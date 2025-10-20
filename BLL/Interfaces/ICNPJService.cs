using BLL.Models.DTOs.CNPJ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ICNPJService
    {
        public Task<CnpjResponse> GetCNPJ(string cnpj);
    }
}
