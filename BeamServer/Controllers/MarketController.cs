using BeamServer.Entities;
using BeamServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Security.Claims;
using Beam.Api;
using Beam.Client;
using Beam.Model;
using System.Numerics;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text.Json;
using System.Globalization;

namespace BeamServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MarketController : ControllerBase
    {

        private IConfiguration _config;
        private readonly IMemoryCache _cache;
        private readonly ILogger<MarketController> _logger;
        private readonly BeamDbContext _dbContext;
        private readonly IAssetsApi _assetsApi;
        private readonly IProfilesApi _profilesApi;
        private readonly ITransactionsApi _transactionsApi;
        private readonly IMarketplaceApi _marketplaceApi;

        public MarketController(ILogger<MarketController> logger, IConfiguration config, IMemoryCache cache, BeamDbContext dbContext, IAssetsApi assetsApi, IProfilesApi profilesApi, ITransactionsApi transactionsApi, IMarketplaceApi marketplaceApi)
        {
            _logger = logger;
            _config = config;
            _cache = cache;
            _dbContext = dbContext;
            _assetsApi = assetsApi;
            _profilesApi = profilesApi;
            _transactionsApi = transactionsApi;
            _marketplaceApi = marketplaceApi;
        }

        private string GetProfile(string userName)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.UserName == userName);
            return user.ProfileId;
        }

        private string GetMinter()
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.UserName == "Minter");
            return user.ProfileId;
        }


        [HttpGet("GetMarket")]
        public async Task<List<MarketDto>> GetMarket()
        {
            var listAssets = new List<MarketDto>();
            try
            {
                var profile = GetProfile(User.Identity.Name);
                var addresBeamon = _config["BeamonContract"];

                var assets = await _marketplaceApi.GetListedAssetsAsync();
                if (assets.TryOk(out GetAssetListingsResponse result))
                {
                    var listIds = result.Data.Where(x => x.Nft.AssetAddress.Equals(addresBeamon, StringComparison.OrdinalIgnoreCase)).Select(x => int.Parse(x.Nft.AssetId));
                    if (listIds.Any())
                    {
                        var monsters = _dbContext.Monsters.Include(x => x.Beamon).Where(x => listIds.Contains(x.MonsterId)).ToList();
                        listAssets = result.Data.Select(x => new MarketDto()
                        {
                            MyAsset = x.SellerEntityId.Equals(profile, StringComparison.OrdinalIgnoreCase),
                            Price = x.Price,
                            Monster = monsters.Where(z => z.MonsterId.ToString() == x.Nft.AssetId).FirstOrDefault()
                        }).ToList();
                    }

                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }


            return listAssets;

        }

    }

}
