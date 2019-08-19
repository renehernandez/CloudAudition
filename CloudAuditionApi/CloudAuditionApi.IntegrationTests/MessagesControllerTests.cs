using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;

namespace CloudAuditionApi.IntegrationTests
{
    public class MessagesControllerTest
    {
        WebApplicationFactory<CloudAuditionApi.Startup> _factory;
        HttpClient _client;

        [SetUp]
        public void Setup()
        {
            _factory = new WebApplicationFactory<Startup>();
            _client = _factory.CreateClient();  
        }

        [Test]
        public async Task ReturnsValidConnection() 
        {
            var response = await _client.GetAsync("/api/messages");

            response.EnsureSuccessStatusCode();

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
    }
}