using BLL.Interfaces;
using BLL.Models.DTOs.CEP;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BrasilDataAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CEPController : ControllerBase
    {
        private ICEPService _cepService;

        public CEPController(ICEPService cepService)
        {
            _cepService = cepService;
        }

        [HttpGet("{cep}")]
        public async Task<CepResponse> GetCEP(string cep)
        {
            if (string.IsNullOrEmpty(cep))
                throw new ArgumentNullException();

            return await _cepService.GetCepAsync(cep);
        }

    }
}
