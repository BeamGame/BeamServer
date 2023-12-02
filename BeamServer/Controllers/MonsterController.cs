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
    public class MonsterController : ControllerBase
    {

        private IConfiguration _config;
        private readonly IMemoryCache _cache;
        private readonly ILogger<MonsterController> _logger;
        private readonly BeamDbContext _dbContext;
        private readonly IAssetsApi _assetsApi;
        private readonly IProfilesApi _profilesApi;
        private readonly ITransactionsApi _transactionsApi;

        public MonsterController(ILogger<MonsterController> logger, IConfiguration config, IMemoryCache cache, BeamDbContext dbContext, IAssetsApi assetsApi, IProfilesApi profilesApi, ITransactionsApi transactionsApi)
        {
            _logger = logger;
            _config = config;
            _cache = cache;
            _dbContext = dbContext;
            _assetsApi = assetsApi;
            _profilesApi = profilesApi;
            _transactionsApi = transactionsApi;
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

        [HttpPost("CreateMinter")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateMinter()
        {
            var minter = GetMinter();
            var profileBeam = await _profilesApi.CreateProfileAsync(new CreateProfileRequestInput(minter));
            string wallet = string.Empty;
            if (profileBeam.IsCreated)
            {
                var profile = JsonSerializer.Deserialize<Profile>(profileBeam.RawContent);
                wallet = profile.wallets[0].address;
            }
            else
            {
                var prof = await _profilesApi.GetProfileAsync(minter);
                if (prof.IsOk)
                {
                    var profile = JsonSerializer.Deserialize<Profile>(prof.RawContent);
                    wallet = profile.wallets[0].address;
                }
                else
                {
                    throw new Exception("Profile not found");
                }
            }
            return Ok(new PlayerName() { Name = wallet }); ;
        }


        [HttpPost("MintStarter")]
        public async Task<List<Monster>> MintStarter()
        {
            try
            {
                var profile = GetProfile(User.Identity.Name);
                var profileBeam = await _profilesApi.CreateProfileAsync(new CreateProfileRequestInput(profile));
                string wallet = string.Empty;
                if (profileBeam.IsCreated)
                {
                    var prof = JsonSerializer.Deserialize<Profile>(profileBeam.RawContent);
                    wallet = prof.wallets[0].address;
                }
                else
                {
                    var prof = await _profilesApi.GetProfileAsync(profile);
                    if (prof.IsOk)
                    {
                        var prof2 = JsonSerializer.Deserialize<Profile>(prof.RawContent);
                        wallet = prof2.wallets[0].address;
                    }
                    else
                    {
                        throw new Exception("Profile not found");
                    }
                }
                var minter = GetMinter();

                var addresBeamon = _config["BeamonContract"];
                var args = new Option<List<object>>(new List<object> { wallet });

                List<CreateTransactionRequestInputInteractionsInner> interactions = new List<CreateTransactionRequestInputInteractionsInner>();
                CreateTransactionRequestInputInteractionsInner interaction = new CreateTransactionRequestInputInteractionsInner(addresBeamon, "safeMint", args);

                CreateTransactionRequestInput request = new CreateTransactionRequestInput(new List<CreateTransactionRequestInputInteractionsInner> { interaction });
                var res = await _transactionsApi.CreateProfileTransactionAsync(request, minter);

                if (res.TryCreated(out CreateTransactionResponse response))
                {
                    var mint = response.Response.Logs.Where(x => x.Topics.Contains(Constants.ERC721Mint)).FirstOrDefault();
                    if (mint != null)
                    {
                        var id = mint.Topics.Last();
                        BigInteger tokenId = BigInteger.Parse(id.Replace("0x",""), NumberStyles.AllowHexSpecifier);
                        var user = await _dbContext.Users.Where(x => x.UserName == User.Identity.Name).FirstAsync();
                        user.RequestStarter = true;

                        var newMonster = new Monster() { BeamonId = 1, Exp = 0, Level = 1, MonsterId = (int)tokenId };
                        _dbContext.Monsters.Add(newMonster);

                        await _dbContext.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,ex.Message);
            }



            return await GetMonsters();

        }

        [HttpPost("MintMonster")]
        public async Task<List<Monster>> MintMonster([FromBody] AddMonsterDto monsterDto)
        {

            /*    var claimsIdentity = User.Identity as ClaimsIdentity;
                var claimAccount = claimsIdentity.FindFirst(JwtRegisteredClaimNames.Name);

                Client client = new Client(new Config()
                {
                    Environment = EnvironmentSelector.Sandbox // Or EnvironmentSelector.Mainnet
                });
                var result = await client.MintsApi.ListMintsAsync(1000, orderBy: "token_id", direction: "desc", tokenAddress: _config["ContractAddress"]);
                var last = result.Result.OrderBy(x => int.Parse(x.Token.Data.TokenId)).LastOrDefault();
                // get next tokenId
                int tokenId = last != null ? int.Parse(last.Token.Data.TokenId) + 1 : 1;

                var getMonster = _dbContext.Monsters.Where(a => EF.Functions.ILike(a.Name, $"{monsterDto.Name.ToLower()}")).FirstOrDefault();
                //var getMoves = _dbContext.Moves.Where(m => monsterDto.Moves.Contains(m.Name)).ToList();
                await _mintService.Mint(tokenId, claimAccount.Value, getMonster);



                var token = new Token()
                {
                    Level = monsterDto.Level,
                    MonsterId = getMonster.MonsterId,
                    Exp = 0,
                    TokenId = tokenId
                };

                _dbContext.Tokens.Add(token);

                _dbContext.SaveChanges();*/

            return await GetMonsters();

        }

        [HttpPost("UpdateMonster")]
        public async Task<bool> UpdateMonster([FromBody] UpdateMonsterDto monsterDto)
        {

            /*    var claimsIdentity = User.Identity as ClaimsIdentity;
                var claimAccount = claimsIdentity.FindFirst(JwtRegisteredClaimNames.Name);

                var getMonster = _dbContext.Tokens.Where(a => a.TokenId == monsterDto.TokenId).FirstOrDefault();

                getMonster.Level = monsterDto.Level;
                getMonster.Exp = monsterDto.Exp;

                _dbContext.SaveChanges();*/

            return true;
        }


        [HttpGet("GetMonsters")]
        public async Task<List<Monster>> GetMonsters()
        {
            var listMonsters = new List<Monster>();
            try
            {
                var profile = GetProfile(User.Identity.Name);
                var addresBeamon = _config["BeamonContract"];
                GetAssetsBodyInput param = new GetAssetsBodyInput();
                param.Limit = 100;
                var assets = await _assetsApi.GetProfileAssetsForGamePostAsync(param, profile);
                if (assets.TryOk(out GetAssetsResponse result))
                {
                    var listIds = result.Data.Where(x => x.AssetAddress.Equals(addresBeamon, StringComparison.OrdinalIgnoreCase)).Select(x => int.Parse(x.AssetId));
                    if (listIds.Any())
                    {
                        listMonsters = _dbContext.Monsters.Where(x => listIds.Contains(x.MonsterId)).ToList();
                    }
                }
            }
            catch (Exception)
            {

            }


            return listMonsters;

        }

        [HttpPost("TransferMonsters")]
        public async Task<List<Monster>> TransferMonster([FromBody] TransferMonsterDto monsterDto)
        {
            /*   var player = _dbContext.Players.Where(a => EF.Functions.ILike(a.Name, $"{monsterDto.UserName}")).FirstOrDefault();

               await _mintService.Transfer(monsterDto.TokenId, player.Account);*/
            return await GetMonsters();

        }

    }
}
