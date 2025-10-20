using BLL.Interfaces;
using BLL.Models.DTOs.CNPJ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CNPJService : ICNPJService
    {
        private readonly HttpService _httpService;
        public string apiCNPJ = "https://brasilapi.com.br/api/cnpj/v1/";

        public CNPJService(HttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<CnpjResponse> GetCNPJ(string cnpj)
        {
            var result = await _httpService.GetAsync<CnpjResponse>(apiCNPJ + cnpj);
            if (result == null)
                return new CnpjResponse { Mensagem = "Houve um erro na solicitação." };

            result.IdadeEmpresa = CalcularIdadeEmpresa(result.DataInicioAtividade);
            result.NumeroDeSocios = ContarSociosAtivos(result.Qsa);
            result.ScoreConfiabilidade = CalcularScoreFicticio(result.IdadeEmpresa, 
                result.NumeroDeSocios, result.CapitalSocial);

            return result;
        }

        public int CalcularIdadeEmpresa(DateTime? dataInicioAtividade)
        {
            if (!dataInicioAtividade.HasValue)
                return 0;

            int anos = DateTime.Now.Year - dataInicioAtividade.Value.Year;
            if (DateTime.Now.DayOfYear < dataInicioAtividade.Value.DayOfYear)
                anos--;

            return anos;
        }

        public int ContarSociosAtivos(List<Qsa> socios)
        {
            if (socios == null)
                return 0;

            return socios.Count;
        }

        public double CalcularScoreFicticio(int idadeEmpresa, int numeroSocios, decimal capitalSocial)
        {
            double pesoTempo = 0.4;
            double pesoSocios = 0.3;
            double pesoCapital = 0.3;

            double scoreTempo = Math.Min(idadeEmpresa, 50) / 50.0;                // normalizado 0..1
            double scoreSocios = Math.Min(numeroSocios, 10) / 10.0;              // normalizado 0..1
            double scoreCapital = Math.Min((double)capitalSocial, 1_000_000) / 1_000_000.0; // normalizado 0..1

            double scoreTotal = (scoreTempo * pesoTempo + scoreSocios * pesoSocios + scoreCapital * pesoCapital) * 100;

            // Ruído aleatório -5..+5
            var rnd = new Random();
            scoreTotal += rnd.NextDouble() * 10 - 5;

            return Math.Round(Math.Clamp(scoreTotal, 0, 100), 2);
        }

    }
}
