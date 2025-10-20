using BLL.Interfaces;
using BLL.Models.DTOs.CNPJ;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BrasilDataAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CNPJController : ControllerBase
    {
        private readonly ICNPJService _cnpjService;

        public CNPJController(ICNPJService cnpjService)
        {
            _cnpjService = cnpjService; 
        }

        [HttpGet("{cnpj}")]
        public async Task<CnpjResponse> GetCNPJ(string cnpj)
        {
            return await _cnpjService.GetCNPJ(cnpj);
        }
    }
}
