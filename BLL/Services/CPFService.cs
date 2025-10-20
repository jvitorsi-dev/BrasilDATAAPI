using BLL.Models.DTOs.CPF;
using BrasilDataAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CPFService : ICPFService
    {
        private readonly string secretKey = "asiudhuaiwheiuahsuidhauisd";

        public async Task<CPFResult> ValidarCPF(CpfRequest cpfInput)
        {
            var result = new CPFResult
            {
                Numero = cpfInput.Cpf,
                Valido = EhCpfValido(cpfInput.Cpf),
                Tipo = "pessoa_fisica",
                ScoreFicticio = GerarScoreFicticio(cpfInput),
                Mensagem = "CPF válido"
            };

            if (!result.Valido)
                result.Mensagem = "CPF inválido";

            return result;
        }

        private static double Clamp(double v, double min, double max) => Math.Max(min, Math.Min(max, v));

        // Mapear idade para score (exemplo simples)
        private static double MapIdadeParaScore(DateTime nascimento)
        {
            int idade = GetAge(nascimento, DateTime.UtcNow);
            // Exemplo de mapeamento: faixa com "menor risco" = 30-60 anos
            if (idade < 18) return 40.0;
            if (idade <= 25) return 50.0;
            if (idade <= 30) return 60.0;
            if (idade <= 45) return 80.0;
            if (idade <= 60) return 70.0;
            if (idade <= 75) return 55.0;
            return 45.0;
        }

        private static int GetAge(DateTime birth, DateTime now)
        {
            int age = now.Year - birth.Year;
            if (now < birth.AddYears(age)) age--;
            return age;
        }

        // Mapear renda (bracket int) para score (exemplo)
        // rendaBracket: 0 = sem renda informada, 1 = baixa, 2 = média, 3 = alta
        private static double MapRendaBracketParaScore(int bracket)
        {
            return bracket switch
            {
                1 => 40.0,
                2 => 65.0,
                3 => 85.0,
                _ => 50.0
            };
        }

        // Mapear CEP para heurística (exemplo: usa apenas o primeiro dígito do CEP)
        private static double MapCepParaScore(string cep)
        {
            var digits = new string(cep.Where(char.IsDigit).ToArray());
            if (digits.Length < 1) return 50.0;
            int region = digits[0] - '0'; // 0..9
                                          // Apenas um exemplo: regiões 0..3 -> score menor, 4..6 médio, 7..9 maior
            if (region <= 3) return 45.0;
            if (region <= 6) return 55.0;
            return 65.0;
        }

        // Heurística simples a partir dos dígitos do CPF
        private static double ScoreFromCpfDigits(string cpf)
        {
            if (string.IsNullOrEmpty(cpf) || cpf.Length != 11) return 50.0;

            int soma = 0;
            for (int i = 0; i < 11; i++) soma += (cpf[i] - '0') * (i + 1);
            // normaliza modulo 101 -> 0..100
            return soma % 101;
        }

        // Gera ruído determinístico em -5..+5
        private static double DeterministicNoiseFromCpf(string cpf, string secretKey)
        {
            if (string.IsNullOrEmpty(cpf) || string.IsNullOrEmpty(secretKey)) return 0.0;

            byte[] keyBytes = Encoding.UTF8.GetBytes(secretKey);
            byte[] msgBytes = Encoding.UTF8.GetBytes(cpf);

            using var hmac = new HMACSHA256(keyBytes);
            var hash = hmac.ComputeHash(msgBytes);

            // pega os primeiros 4 bytes como uint e transforma em valor 0..1
            uint v = BitConverter.ToUInt32(hash, 0);
            double normalized = (v / (double)uint.MaxValue); // 0..1
                                                             // mapear para -5..+5
            return (normalized * 10.0) - 5.0;
        }

        public int GerarScoreFicticio(CpfRequest cpfInput)
        {
            var cleanCpf = new string((cpfInput.Cpf ?? "").Where(char.IsDigit).ToArray());
            var valido = EhCpfValido(cleanCpf);

            // PESOS (somam 1.0)
            const double wValidade = 0.30;
            const double wIdade = 0.20;
            const double wRenda = 0.20;
            const double wRegiao = 0.10;
            const double wCpfHeur = 0.10;
            const double wRuido = 0.10;

            // 1) score de validade: 0 ou 100
            double scoreValidade = valido ? 100.0 : 0.0;

            // 2) score de idade (se data disponível)
            double scoreIdade = 50.0; // default neutro
            if (cpfInput.DataNascimento.HasValue)
                scoreIdade = MapIdadeParaScore(cpfInput.DataNascimento.Value);

            // 3) score de renda (se informado como bracket - int)
            double scoreRenda = 50.0;
            if (cpfInput.RendaBracket.HasValue)
                scoreRenda = MapRendaBracketParaScore(cpfInput.RendaBracket.Value);

            // 4) score regional (apenas heurística via CEP)
            double scoreRegiao = 50.0;
            if (!string.IsNullOrWhiteSpace(cpfInput.Cep))
                scoreRegiao = MapCepParaScore(cpfInput.Cep);

            // 5) heurística do CPF (soma/dígitos)
            double scoreCpfHeur = ScoreFromCpfDigits(cleanCpf);

            // 6) ruído determinístico [-5 .. +5] mapeado para [45..55] escala parcial
            double ruido = DeterministicNoiseFromCpf(cleanCpf, secretKey); // -5..+5

            // Combina sem ruído primeiro (base)
            double baseCombined =
                scoreValidade * wValidade +
                scoreIdade * wIdade +
                scoreRenda * wRenda +
                scoreRegiao * wRegiao +
                scoreCpfHeur * wCpfHeur;

            // Aplica ruído ponderado
            double final = baseCombined * (1 - wRuido) + (50.0 + ruido) * wRuido;

            // Normaliza para 0-100 e arredonda
            int finalInt = (int)Math.Round(Clamp(final, 0, 100));
            int baseInt = (int)Math.Round(Clamp(baseCombined, 0, 100));

            return baseInt; 
        }

        private bool EhCpfValido(string cpf)
        {
            // Remove caracteres não numéricos
            cpf = new string(cpf.Where(char.IsDigit).ToArray());

            if (cpf.Length != 11)
                return false;

            // Verifica se todos os dígitos são iguais (CPF inválido)
            if (cpf.Distinct().Count() == 1)
                return false;

            // Valida dígitos verificadores
            int[] multiplicador1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCpf = cpf.Substring(0, 9);
            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            int resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            string digito = resto.ToString();
            tempCpf += digito;

            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito += resto.ToString();

            return cpf.EndsWith(digito);
        }
    }
}



