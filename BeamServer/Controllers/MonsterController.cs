using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeamServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MonsterController : ControllerBase
    {

        private readonly ILogger<MonsterController> _logger;

        public MonsterController(ILogger<MonsterController> logger)
        {
            _logger = logger;
        }  

    }
}
