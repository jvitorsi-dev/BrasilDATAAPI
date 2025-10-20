using BLL.Models.DTOs.CEP;

namespace BLL.Interfaces
{
    public interface ICEPService
    {
        public Task<CepResponse> GetCepAsync(string cep);
    }
}
