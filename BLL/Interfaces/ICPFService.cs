using BLL.Models.DTOs.CPF;

namespace BrasilDataAPI.Interfaces
{
    public interface ICPFService
    {
        public Task<CPFResult> ValidarCPF(CpfRequest cpfInput);
    }


}
