
using System.Text.Json.Serialization;

namespace BLL.Models.DTOs.CNPJ
{
    public class CnpjResponse
    {
        [JsonPropertyName("cnpj")]
        public string Cnpj { get; set; } = string.Empty;
        public string Mensagem { get; set; } = string.Empty;
        public int IdadeEmpresa { get; set; } = 0;          // em anos
        public int NumeroDeSocios { get; set; } = 0;
        public double ScoreConfiabilidade { get; set; } = 0.0; // 0 a 100, fictício

        [JsonPropertyName("razao_social")]
        public string RazaoSocial { get; set; } = string.Empty;

        [JsonPropertyName("nome_fantasia")]
        public string NomeFantasia { get; set; } = string.Empty;

        [JsonPropertyName("capital_social")]
        public decimal CapitalSocial { get; set; } = 0m;

        [JsonPropertyName("porte")]
        public string Porte { get; set; } = string.Empty;

        [JsonPropertyName("codigo_porte")]
        public int CodigoPorte { get; set; } = 0;

        [JsonPropertyName("natureza_juridica")]
        public string NaturezaJuridica { get; set; } = string.Empty;

        [JsonPropertyName("codigo_natureza_juridica")]
        public int CodigoNaturezaJuridica { get; set; } = 0;

        [JsonPropertyName("situacao_cadastral")]
        public int SituacaoCadastral { get; set; } = 0;

        [JsonPropertyName("descricao_situacao_cadastral")]
        public string DescricaoSituacaoCadastral { get; set; } = string.Empty;

        [JsonPropertyName("data_situacao_cadastral")]
        public DateTime? DataSituacaoCadastral { get; set; } = null;

        [JsonPropertyName("descricao_tipo_de_logradouro")]
        public string DescricaoTipoDeLogradouro { get; set; } = string.Empty;

        [JsonPropertyName("logradouro")]
        public string Logradouro { get; set; } = string.Empty;

        [JsonPropertyName("numero")]
        public string Numero { get; set; } = string.Empty;

        [JsonPropertyName("complemento")]
        public string Complemento { get; set; } = string.Empty;

        [JsonPropertyName("bairro")]
        public string Bairro { get; set; } = string.Empty;

        [JsonPropertyName("municipio")]
        public string Municipio { get; set; } = string.Empty;

        [JsonPropertyName("codigo_municipio_ibge")]
        public int CodigoMunicipioIbge { get; set; } = 0;

        [JsonPropertyName("uf")]
        public string Uf { get; set; } = string.Empty;

        [JsonPropertyName("cep")]
        public string Cep { get; set; } = string.Empty;

        [JsonPropertyName("ddd_telefone_1")]
        public string DddTelefone1 { get; set; } = string.Empty;

        [JsonPropertyName("ddd_telefone_2")]
        public string DddTelefone2 { get; set; } = string.Empty;

        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;

        [JsonPropertyName("opcao_pelo_simples")]
        public bool? OpcaoPeloSimples { get; set; } = null;

        [JsonPropertyName("opcao_pelo_mei")]
        public bool? OpcaoPeloMEI { get; set; } = null;

        [JsonPropertyName("data_opcao_pelo_simples")]
        public DateTime? DataOpcaoPeloSimples { get; set; } = null;

        [JsonPropertyName("data_exclusao_do_simples")]
        public DateTime? DataExclusaoDoSimples { get; set; } = null;

        [JsonPropertyName("data_opcao_pelo_mei")]
        public DateTime? DataOpcaoPeloMEI { get; set; } = null;

        [JsonPropertyName("data_exclusao_do_mei")]
        public DateTime? DataExclusaoDoMEI { get; set; } = null;

        [JsonPropertyName("data_inicio_atividade")]
        public DateTime? DataInicioAtividade { get; set; } = null;

        [JsonPropertyName("cnae_fiscal")]
        public int CnaeFiscal { get; set; } = 0;

        [JsonPropertyName("cnae_fiscal_descricao")]
        public string CnaeFiscalDescricao { get; set; } = string.Empty;

        [JsonPropertyName("cnaes_secundarios")]
        public List<CnaeSecundario> CnaesSecundarios { get; set; } = new();

        [JsonPropertyName("regime_tributario")]
        public List<RegimeTributario> RegimeTributario { get; set; } = new();

        [JsonPropertyName("qsa")]
        public List<Qsa> Qsa { get; set; } = new();

        [JsonPropertyName("descricao_identificador_matriz_filial")]
        public string DescricaoIdentificadorMatrizFilial { get; set; } = string.Empty;

        [JsonPropertyName("identificador_matriz_filial")]
        public int IdentificadorMatrizFilial { get; set; } = 0;

        [JsonPropertyName("descricao_motivo_situacao_cadastral")]
        public string DescricaoMotivoSituacaoCadastral { get; set; } = string.Empty;

        [JsonPropertyName("situacao_especial")]
        public string SituacaoEspecial { get; set; } = string.Empty;
    }

    public class CnaeSecundario
    {
        [JsonPropertyName("codigo")]
        public int Codigo { get; set; } = 0;

        [JsonPropertyName("descricao")]
        public string Descricao { get; set; } = string.Empty;
    }

    public class RegimeTributario
    {
        [JsonPropertyName("ano")]
        public int Ano { get; set; } = 0;

        [JsonPropertyName("forma_de_tributacao")]
        public string FormaDeTributacao { get; set; } = string.Empty;

        [JsonPropertyName("quantidade_de_escrituracoes")]
        public int QuantidadeDeEscrituracoes { get; set; } = 0;
    }

    public class Qsa
    {
        [JsonPropertyName("nome_socio")]
        public string NomeSocio { get; set; } = string.Empty;

        [JsonPropertyName("cnpj_cpf_do_socio")]
        public string CnpjCpfDoSocio { get; set; } = string.Empty;

        [JsonPropertyName("qualificacao_socio")]
        public string QualificacaoSocio { get; set; } = string.Empty;

        [JsonPropertyName("faixa_etaria")]
        public string FaixaEtaria { get; set; } = string.Empty;

        [JsonPropertyName("data_entrada_sociedade")]
        public DateTime DataEntradaSociedade { get; set; } = DateTime.MinValue;

        [JsonPropertyName("cpf_representante_legal")]
        public string CpfRepresentanteLegal { get; set; } = string.Empty;

        [JsonPropertyName("qualificacao_representante_legal")]
        public string QualificacaoRepresentanteLegal { get; set; } = string.Empty;
    }


}
