using BLL.Interfaces;
using BLL.Models.DTOs.CEP;
using BLL.Models.DTOs.CPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CEPService : ICEPService
    {
        private readonly HttpService _httpService;
        public string _apiBrasil = "https://viacep.com.br/ws/";

        public CEPService(HttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<CepResponse> GetCepAsync(string cep)
        {
            var response = await _httpService.GetAsync<CepResponse>(_apiBrasil + cep + "/json");

            if (response == null)
                return new CepResponse { Mensagem = "CEP não encontrado!" };

            return response;
        }


    }
}
