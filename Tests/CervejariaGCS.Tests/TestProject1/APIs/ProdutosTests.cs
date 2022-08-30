using CervejariaGCS.Models;
using CervejariaGCS.ViewModels;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text.Json;

namespace CervejariaGCS.Tests.APIs;

public class ProdutosTests : BaseApiTest
{
    [Test]
    [TestCase(0, 5)] // página 0 deve ser traduzida para página 1 (na api)
    [TestCase(1, 5)] // página 1, 5 itens: 1 a 5
    [TestCase(2, 5)] // página 2, 5 itens: 6 a 10
    [TestCase(3, 5)] // página 3, 3 itens: 11 a 13
    [TestCase(4, 5)] // página 4, 0 itens
    [TestCase(0, 10)] // página 1 (0): 10 itens: 1 a 10
    [TestCase(1, 10)] // página 1: 10 itens: 1 a 10
    [TestCase(2, 10)] // página 2: 3 itens: 11 a 13
    [TestCase(0, 15)] // página 1 (0): 10 itens: 1 a 10 (paginação limitada a 10 itens)
    [TestCase(1, 15)] // página 1: 10 itens: 1 a 10 (paginação limitada a 10 itens)
    [TestCase(2, 15)] // página 2: 3 itens: de 11 a 13
    [TestCase(0, 7)] // página 1 (0): 7 itens: 1 a 7
    [TestCase(1, 7)] // página 1: 6 itens: 8 a 13
    [TestCase(2, 7)] // página 2: 3 itens: 11 a 13
    [TestCase(3, 7)] // página 3, 0 itens
    public async Task GetAsync(int page, int pageSize)
    {
        var response = await Client.GetAsync($"/v1/products?page={page}&pageSize={pageSize}");
        response.IsSuccessStatusCode.Should().BeTrue();

        var test = await response.Content.ReadAsStringAsync();

        // ## System.Text.Json aparentemente não gosta de genéricos ( <T> ).
        //var results = await response.Content.ReadFromJsonAsync<ResultViewModel<List<Product>>>(new System.Text.Json.JsonSerializerOptions()
        //{
        //    AllowTrailingCommas = true,
        //    PropertyNameCaseInsensitive = true
        //});

        using JsonDocument doc = JsonDocument.Parse(test);
        {
            var data = doc.RootElement.GetProperty("data").Deserialize<List<Product>>();

            // a lista de produtos deve ser limitada a {pageSize} produtos, ou menor, dependendo de {page}, num total de 13 produtos disponíveis no cadastro.
            data.Should().HaveCount(Math.Max(0, Math.Min(13, Math.Max(page, 1) * Math.Min(pageSize, 10)) - (Math.Min(pageSize, 10) * (Math.Max(page, 1) - 1))));

            Console.WriteLine(Math.Min(13, Math.Max(page, 1) * Math.Min(pageSize, 10))); // quantidade de itens para página (apenas verificação da linha 42)
            Console.WriteLine(pageSize * (Math.Max(page, 1) - 1)); // limite de itens possíveis (apenas verificação da linha 42)

        }

    }
}
