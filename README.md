# CervejariaGCS API

Projeto feito utilizando o Visual Studio 2022, C# (Web API ASP.Net Core), EF Core (SQL Server) utilizando a abordagem Code First (Model First) e Swagger para documentação da API.

### PACOTES NECESSÁRIOS:

#### Entity Framework Core SQL Server
`dotnet add package Microsoft.EntityFrameworkCore.SqlServer`

#### Entity Framework Core Design
`dotnet add package Microsoft.EntityFrameworkCore.Design`

#### Entity Framework Core .NET Tools
`dotnet tool install --global dotnet-ef`

### TUTORIAL:
 1. Instale os **pacotes necessários** listados acima
 2. Crie uma Migration com o seguinte comando: `dotnet ef migrations add InitialDataBase`
 3. Execute a Migration criada com o seguinte comando: `dotnet ef database update`
 4. Pronto, banco de dados criado com sucesso!
 5. Inicie o projeto

### OBSERVAÇÕES:

 - Por padrão e requisito do desafio, será preenchido uma lista de Cervejas (Tabela Product) e Cashbacks (Tabela Cashback)
 - O projeto possui uma tela (Front-End) criada pelo Swagger com todas as operações possíveis na API

### CONTATO:
 - **E-mail**: *gustavo@gcscript.dev*
 - **Whatsapp**: *(21) 9-6647-3139*
 - **Repositório do Projeto**: *https://github.com/gcscript/CervejariaGCS*
