using Beam.Api;
using BeamServer.Entities;
using BeamServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

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
        private readonly UserManager<BeamUser> _userManager;

        public LoginController(UserManager<BeamUser> userManager, ILogger<LoginController> logger, IConfiguration config,  BeamDbContext dbContext, IAssetsApi assetsApi)
        {
            _logger = logger;
            _userManager = userManager;
            _config = config;
            _dbContext = dbContext;
            _assetsApi = assetsApi;
        }


        [HttpGet()]
        public async Task<PlayerName> Get()
        {
            return new PlayerName() { Name = User.Identity.Name };
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
