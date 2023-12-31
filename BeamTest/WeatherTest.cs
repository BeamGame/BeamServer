using BeamServer;
using BeamServer.Entities;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using System.Globalization;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Numerics;
using System.Security.Cryptography.Xml;
using Xunit.Abstractions;

namespace BeamTest
{
    public class WeatherTest
    {
        private readonly ITestOutputHelper output;

        public WeatherTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        public class Bearer
        {
            public string AccessToken { get; set; }
        }

        [Fact]
        public void Test()
        {
            var r = BigInteger.Parse("728faf34b64cd55c8d1d500268026ffb", NumberStyles.AllowHexSpecifier);
        }


        [Fact]
        public async Task GetForecast()
        {
            await using var application = new WebApplicationFactory<BeamServer.WeatherForecast>();
            using var client = application.CreateClient();

            LoginRequest log = new LoginRequest()
            {
                Email = "andrew@example.com",
                Password = "SuperSecret1!"
            };

            var loginResponse = await client.PostAsJsonAsync<LoginRequest>("api/account/login", log);
            var token = await loginResponse.Content.ReadFromJsonAsync<Bearer>();
            output.WriteLine("token " + token.AccessToken.ToString());

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);
            var weatherResponse = await client.GetAsync("api/weatherforecast");
            var weather = await weatherResponse.Content.ReadFromJsonAsync<List<WeatherForecast>>();
            var weatherJson = await weatherResponse.Content.ReadAsStringAsync();
            output.WriteLine("weatherResponse " + weatherJson);
            Assert.True(weather.Any());
        }
    }
}