using System.Net.Http.Json;

namespace CervejariaGCS.Tests.APIs;

public class HomeTests : BaseApiTest
{
    [Test]
    public async Task GetAsync()
    {
        var response = await Client.GetAsync("");
        response.IsSuccessStatusCode.Should().BeTrue();
    }
}
