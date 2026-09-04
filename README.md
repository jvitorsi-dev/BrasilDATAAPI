<div align="center">

# 🇧🇷 BrasilDATAAPI

**API REST em .NET 8 para consulta de dados públicos brasileiros — CEP, CNPJ e CPF**

[![.NET](https://img.shields.io/badge/.NET-8.0-512BD4)](https://dotnet.microsoft.com/)
[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE)

</div>

## 💡 O problema

Dados públicos brasileiros (endereços, empresas, documentos) estão espalhados em APIs diferentes, com formatos e disponibilidade variados. Esta API unifica o acesso a eles por trás de **endpoints REST consistentes**, com DTOs tipados.

## 🔌 Endpoints

| Método | Rota | Fonte |
|---|---|---|
| `GET` | `/api/CEP/{cep}` | [ViaCEP](https://viacep.com.br) |
| `GET` | `/api/CNPJ/{cnpj}` | [BrasilAPI](https://brasilapi.com.br) |
| `POST` | `/api/CPF` · body `{"cpf":"..."}` | Validação local (dígito verificador) + score |

## 🏗️ Camadas

```
BrasilDataAPI.sln
├── BrasilDataAPI/   # 🌐 Controllers e configuração da API
└── BLL/             # ⚙️ Serviços de negócio, DTOs e acesso HTTP às fontes
```

## 🚀 Como rodar

```bash
dotnet run --project BrasilDataAPI
```

## 🗺️ Roadmap

- [ ] Swagger UI
- [ ] Testes de unidade (BLL)
- [ ] Cache e resiliência (Polly) nas chamadas às fontes
- [ ] **Próximo capítulo:** evoluir para um assistente de IA (RAG + function calling) sobre dados públicos brasileiros — .NET + Semantic Kernel

## 📄 Licença

Distribuído sob a licença [MIT](LICENSE).
