#  Sistema de Gestão de Pessoas e Salários

Aplicação desenvolvida em ASP.NET Web Forms integrada ao Oracle Database, com foco na gestão de pessoas, cargos e cálculo de salários baseado em regras de negócio.

---

##  Contexto do Projeto

Este projeto foi desenvolvido como parte de um desafio técnico para uma vaga de estágio, com o objetivo de simular um sistema corporativo real de gestão de funcionários.

---

##  Funcionalidades

*  CRUD completo de Pessoas (Cadastro, edição, exclusão e listagem)
*  Associação de Pessoas a Cargos
*  Geração da tabela `pessoa_salario`
*  Cálculo de salários realizado diretamente no banco de dados
*  Tela de listagem de salários
*  Reprocessamento de salários sob demanda

---

##  Diferenciais

* Processamento assíncrono para cálculo de salários
* Separação de responsabilidades entre aplicação e banco de dados
* Uso de procedures para lógica de negócio

---

##  Tecnologias utilizadas

* ASP.NET Web Forms
* C#
* Oracle Database (11g+)
* Oracle.ManagedDataAccess
* .NET Framework

---

##  Como executar o projeto

### 1. Clonar o repositório

```bash
git clone https://github.com/seuusuario/DesafioEstagio.git
```

### 2. Configurar o banco de dados

Atualize a connection string no arquivo `Web.config`:

```xml
<connectionStrings>
  <add name="OracleDB" 
       connectionString="User Id=SEU_USUARIO;Password=SUA_SENHA;Data Source=localhost:1521/XEPDB1;" 
       providerName="Oracle.ManagedDataAccess.Client" />
</connectionStrings>
```

### 3. Executar scripts SQL

Execute os scripts disponíveis na pasta `/Database`.

### 4. Rodar o projeto

Abra no Visual Studio e pressione `F5`.

---

##  Principais desafios

* Implementar o cálculo de salários diretamente no banco de dados
* Garantir consistência entre tabelas relacionadas
* Integrar ASP.NET Web Forms com Oracle

---

##  Autor

Desenvolvido por Jackson Douglas 
