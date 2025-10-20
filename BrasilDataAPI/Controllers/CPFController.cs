using BLL.Models.DTOs.CPF;
using BrasilDataAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BrasilDataAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CPFController : ControllerBase
    {
        private readonly ICPFService _cpfService;

        public CPFController(ICPFService cpfService)
        {
            _cpfService = cpfService;
        }

        [HttpPost]
        public async Task<CPFResult> Post([FromBody] CpfRequest cpf)
        {
            return await _cpfService.ValidarCPF(cpf);
        }
    }
}
