# RH Manager

Sistema web de gestão de pessoas, cargos e salários desenvolvido em ASP.NET Web Forms com C# e Oracle Database.

> **Nota:** Este repositório representa a versão original do projeto em Web Forms. A versão 2.0 — com migração para ASP.NET Core Web API + Angular + PostgreSQL e arquitetura em Clean Architecture — está em desenvolvimento em repositório separado.

---

## 📋 Sobre o projeto

O RH Manager é uma aplicação corporativa que permite o gerenciamento completo de funcionários, cargos e folha de pagamento. O sistema foi construído com foco em boas práticas de acesso a dados, separação de responsabilidades entre aplicação e banco de dados, e processamento assíncrono.

---

## ✨ Funcionalidades

- **Gestão de Pessoas** — cadastro, edição, exclusão e listagem com paginação
- **Associação a Cargos** — vínculo de cada pessoa a um cargo com seleção via dropdown
- **Cálculo de Salários** — processamento executado diretamente no banco via stored procedure
- **Reprocessamento sob demanda** — recalcula toda a folha com um clique
- **Listagem de Salários** — visualização da folha com join entre pessoa, cargo e salário

---

## 🏗️ Estrutura do projeto

```
RH-Manager/
├── Pages/              # Telas da aplicação (.aspx + code-behind)
│   ├── Pessoas.aspx    # CRUD de pessoas
│   └── Salarios.aspx   # Gestão e cálculo de salários
├── Layout/             # Master Pages e controles de layout
├── Models/             # Classes de modelo de dados
├── Database/
│   └── Scripts/        # Scripts SQL: criação de tabelas e stored procedures
├── Web.config          # Configuração da aplicação e connection string
└── RHManager.sln       # Solution do Visual Studio
```

---

## 🛠️ Tecnologias

| Tecnologia | Uso |
|---|---|
| ASP.NET Web Forms | Framework web principal |
| C# / .NET Framework | Linguagem e plataforma |
| Oracle Database 11g+ | Banco de dados relacional |
| Oracle.ManagedDataAccess | Driver de conexão com Oracle |
| HTML / CSS / Bootstrap | Interface do usuário |

---

## ⚙️ Como executar

### Pré-requisitos

- Visual Studio 2019 ou superior
- Oracle Database (local ou remoto) — versão 11g ou superior
- .NET Framework 4.7.2+

### Passo a passo

**1. Clone o repositório**
```bash
git clone https://github.com/jacksondls/RH-Manager.git
```

**2. Configure o banco de dados**

Execute os scripts na pasta `Database/Scripts/` na seguinte ordem:
1. Script de criação das tabelas (`tabelas.sql`)
2. Script da stored procedure de cálculo de salários (`procedures.sql`)

**3. Configure a connection string**

No arquivo `Web.config`, atualize com suas credenciais Oracle:
```xml
<connectionStrings>
  <add name="OracleDB"
       connectionString="User Id=SEU_USUARIO;Password=SUA_SENHA;Data Source=localhost:1521/XEPDB1;"
       providerName="Oracle.ManagedDataAccess.Client" />
</connectionStrings>
```

**4. Execute o projeto**

Abra `RHManager.sln` no Visual Studio e pressione `F5`.

---

## 🔍 Destaques técnicos

**Stored Procedure para cálculo de salários**
A lógica de cálculo da folha de pagamento reside inteiramente no banco de dados, chamada via `CommandType.StoredProcedure`. Essa abordagem garante que regras de negócio financeiras sejam executadas de forma atômica e consistente, sem risco de dados parcialmente calculados.

**Processamento assíncrono**
A tela de salários utiliza `async/await` nas operações de banco de dados, evitando que o processamento da folha bloqueie a thread principal da aplicação durante operações longas.

**Proteção contra SQL Injection**
Todas as queries utilizam parâmetros nomeados (`:nome`, `:cargo_id`, `:pessoa_id`) em vez de concatenação de strings, protegendo o sistema contra injeção de SQL.

---

## 🗺️ Roadmap — Versão 2.0

A evolução planejada deste projeto envolve uma migração completa de stack, mantendo as regras de negócio e expandindo a arquitetura:

- [ ] Backend: migração para **ASP.NET Core Web API** com Clean Architecture
- [ ] Frontend: migração para **Angular** com estrutura feature-based
- [ ] Banco: migração de **Oracle** para **PostgreSQL**
- [ ] Arquitetura: implementação de **Repository Pattern** + **Unit of Work**
- [ ] Autenticação via **JWT**
- [ ] Dashboard com métricas da folha de pagamento

---

## 👨‍💻 Autor

Desenvolvido por **Jackson Douglas**

[![LinkedIn](https://img.shields.io/badge/LinkedIn-0077B5?style=for-the-badge&logo=linkedin&logoColor=white)](https://github.com/jacksondls/RH-Manager)
[![GitHub](https://img.shields.io/badge/GitHub-100000?style=for-the-badge&logo=github&logoColor=white)](https://github.com/jacksondls)