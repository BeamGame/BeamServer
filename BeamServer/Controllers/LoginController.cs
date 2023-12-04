using Beam.Api;
using Beam.Client;
using Beam.Model;
using BeamServer.Entities;
using BeamServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace BeamServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        private readonly ILogger<LoginController> _logger;
        private readonly BeamDbContext _dbContext;
        private readonly IAssetsApi _assetsApi;
        private readonly IProfilesApi _profilesApi;
        private readonly UserManager<BeamUser> _userManager;

        public LoginController(UserManager<BeamUser> userManager, ILogger<LoginController> logger, IConfiguration config, BeamDbContext dbContext, IAssetsApi assetsApi, IProfilesApi profilesApi)
        {
            _logger = logger;
            _userManager = userManager;
            _config = config;
            _dbContext = dbContext;
            _assetsApi = assetsApi;
            _profilesApi = profilesApi;
        }

        private string GetProfile(string userName)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.UserName == userName);
            return user.ProfileId;
        }


        [HttpGet()]
        public async Task<PlayerName> Get()
        {
            return new PlayerName() { Name = User.Identity.Name };
        }

        [HttpGet("ConnectionRequest")]
        public async Task<PlayerName> GetConnectionRequest()
        {
            try
            {

                var profile = GetProfile(User.Identity.Name);
                var test = new GenerateLinkCodeRequestInput("https://beam-server.azurewebsites.net/");
                //var req = await _profilesApi.CreateConnectionRequestAsync(test, profile, string.Empty);
                HttpClient client = new HttpClient();

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post,
                    $"https://api.testnet.onbeam.com/v1/profiles/{profile}/create-connection-request");

                request.Headers.Add("accept", "application/json");
                request.Headers.Add("x-api-key", _config["ApiKey"]);

                request.Content = new StringContent("{\n  \"callbackUrl\": \"toto\"\n}");
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                HttpResponseMessage response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadFromJsonAsync<GenerateLinkCodeResponse>();

                if (responseBody != null)
                {
                    return new PlayerName() { Name = responseBody.Challenge };
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return new PlayerName() { Name = string.Empty };
        }

        [HttpPost("Update")]
        public async Task<IActionResult> UpdateUsername([FromBody] PlayerName playerName)
        {

            var user = _dbContext.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
            user.UserName = playerName.Name;
            await _userManager.UpdateAsync(user);
            await _userManager.UpdateNormalizedUserNameAsync(user);
            return Ok(playerName);
        }
    }
}
