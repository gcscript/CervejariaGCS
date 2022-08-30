using Microsoft.Extensions.DependencyInjection;

namespace CervejariaGCS.Tests;

public abstract class BaseApiTest
{   
    /// <summary>
    /// Instância de web app para consumo das API's
    /// </summary>
    internal CervejariaApplicationFactory Aplication { get; private set; }

    /// <summary>
    /// Instância de escopo para armazenamento dos serviços injetados (Dependency Injection)
    /// </summary>
    public IServiceScope Scope { get; private set; }

    /// <summary>
    /// Provedor dos serviços adicionados (DI)
    /// </summary>
    public IServiceProvider Provider { get; private set; }

    /// <summary>
    /// HttpClient para consumo das api's
    /// </summary>
    public HttpClient Client { get; private set; }

    /// <summary>
    /// Este método é acionado pelo NUnit antes da execução de cada teste.
    /// </summary>
    /// <returns></returns>
    [SetUp]
    public async Task Setup()
    {
        Aplication = new();
        Scope = Aplication.Services.CreateScope();
        Provider = Scope.ServiceProvider;
        Client = Aplication.CreateClient();

        using (var dbContext = Provider.GetRequiredService<CervejariaGCS.Data.GCSDataContext>())
        {
            // estamos assegurando que a base de dados seja reiniciada do ponto zero a cada teste.
            await dbContext.Database.EnsureDeletedAsync();
            await dbContext.Database.EnsureCreatedAsync();
        }
    }

    /// <summary>
    /// Este método é executado ao final de cada teste.
    /// </summary>
    [TearDown]
    public void TearDown()
    {
        Client.Dispose();
        Scope.Dispose();
        Aplication.Dispose();
    }
}
