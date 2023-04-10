using Amazon.Runtime.Internal;
using Auth0.AuthenticationApi;
using Auth0.AuthenticationApi.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson.Serialization;
using System.Diagnostics;

namespace PortStanleyRun.Api.IntegrationTests
{
    public class RunControllerTests
    {
        private PortStanleyApiFactory _application;
        private readonly HttpClient httpClient;
        private readonly IConfigurationRoot config;
        private readonly IConfigurationSection auth0Settings;

        public RunControllerTests()
        {
            _application = new PortStanleyApiFactory();
            httpClient = _application.CreateClient();

            config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.test.json")
                .AddUserSecrets<RunControllerTests>()
                .Build();

            auth0Settings = config.GetSection("Auth0");
        }

        async Task<string> GetAccessToken()
        {
            var auth0Client = new AuthenticationApiClient(auth0Settings["Domain"]);
            var tokenRequest = new ClientCredentialsTokenRequest()
            {
                ClientId = auth0Settings["ClientId"],
                Audience = auth0Settings["Audience"],
                ClientSecret = config["api-clientsecret"]
            };

            var tokenResponse = await auth0Client.GetTokenAsync(tokenRequest);


            return tokenResponse.AccessToken;
        }

        [Test]
        public async Task Test1()
        {
            //Arrange
            var token = await GetAccessToken();

            var request = new HttpRequestMessage(HttpMethod.Get, $"{config["Urls:Api"]}Run/GetAllRuns");
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            //Act
            var response = await httpClient.SendAsync(request);
            try { 
            response.EnsureSuccessStatusCode();
            } catch (HttpRequestException ex)
            {
                Console.WriteLine(ex.ToString());
            }
            var stringResponse = await response.Content.ReadAsStringAsync();
            
            var terms = BsonSerializer.Deserialize<List<Models.PortStanleyRun>>(stringResponse);

            //Assert
            Assert.That(terms, Has.Count.GreaterThan(0));
        }
    }
} 