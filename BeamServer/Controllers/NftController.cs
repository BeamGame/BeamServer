using BeamServer.Entities;
using BeamServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace BeamServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NftController : ControllerBase
    {
        private IConfiguration _config;
        private readonly IMemoryCache _cache;
        private readonly ILogger<NftController> _logger;
        private readonly BeamDbContext _dbContext;

        public NftController(ILogger<NftController> logger, IConfiguration config, IMemoryCache cache, BeamDbContext dbContext)
        {
            _logger = logger;
            _config = config;
            _cache = cache;
            _dbContext = dbContext;
        }

        [HttpGet("{id}")]
        public NftDto Get(int id)
        {
            var uri = BaseUrl(Request);
            var monster = _dbContext.Monsters.Include(x => x.Beamon).Where(x => x.MonsterId == id).FirstOrDefault();
            if (monster != null)
            {
                return new NftDto()
                {
                    Name = monster.Beamon.Name,
                    Description = "Beamon",
                    Id = monster.Beamon.BeamonId,
                    Image = $"{uri}api/nft/image/{monster.Beamon.BeamonId}"
                };
            }
            return new NftDto()
            {
                Name = "Egg",
                Description = "Unrevelated monster",
                Id = 0,
                Image = $"{uri}api/nft/image/0"
            };
        }


        [HttpGet("Image/{id}")]
        public IActionResult GetImage(int id)
        {
            if (id < 1 || id > 6)
            {
                var imageNF = System.IO.File.OpenRead("wwwroot/images/monsters/0_0.png");
                return File(imageNF, "image/jpeg");
            }
            var image = System.IO.File.OpenRead($"wwwroot/images/monsters/{id}_0.png");
            return File(image, "image/jpeg");
        }

        public static string? BaseUrl(HttpRequest req)
        {
            if (req == null) return null;
            var uriBuilder = new UriBuilder(req.Scheme, req.Host.Host, req.Host.Port ?? -1);
            if (uriBuilder.Uri.IsDefaultPort)
            {
                uriBuilder.Port = -1;
            }

            return uriBuilder.Uri.AbsoluteUri;
        }
    }
}
