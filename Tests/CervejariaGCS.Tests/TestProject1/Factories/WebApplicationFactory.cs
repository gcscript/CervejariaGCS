using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore.Storage;
using CervejariaGCS.Data;

namespace CervejariaGCS.Tests;

internal class CervejariaApplicationFactory : WebApplicationFactory<Program>
{
    protected override IHost CreateHost(IHostBuilder builder)
    {
        var root = new InMemoryDatabaseRoot();

        builder.ConfigureServices(services =>
        {
            // removendo DbContextFactory que usa SqlServer
            services.RemoveAll(typeof(DbContextOptions<GCSDataContext>));
            // adicionando DbContextFactory que usa InMemory
            services.AddDbContext<GCSDataContext>(options =>
            {
                options.UseInMemoryDatabase("Testing", root);
            });
        });

        return base.CreateHost(builder);
    }
}
