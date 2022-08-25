# CervejariaGCS API

Projeto feito utilizando o Visual Studio 2022, C# (Web API ASP.Net Core), EF Core (SQL Server) utilizando a abordagem Code First (Model First) e Swagger para documenta��o da API.

### PACOTES NECESS�RIOS:

#### Entity Framework Core SQL Server
`dotnet add package Microsoft.EntityFrameworkCore.SqlServer`

#### Entity Framework Core Design
    dotnet add package Microsoft.EntityFrameworkCore.Design

### TUTORIAL:
 1. Instale os **pacotes necess�rios** listados acima
 2. Crie uma Migration com o seguinte comando: `dotnet ef migrations add Start`
 3. Execute a Migration criada com o seguinte comando: `dotnet ef database update`
 4. Pronto, banco de dados criado com sucesso!
 5. Inicie o projeto

### OBSERVA��ES:

 - Por padr�o e requisito do desafio, ser� preenchido uma lista de Cervejas (Tabela Product) e Cashbacks (Tabela Cashback)
 - O projeto possui uma tela (Front-End) criada pelo Swagger com todas as opera��es poss�veis na API

### CONTATO:
 - **E-mail**: *gustavo@gcscript.dev*
 - **Whatsapp**: *(21) 9-6647-3139*
 - **Reposit�rio do Projeto**: *https://github.com/gcscript/CervejariaGCS*