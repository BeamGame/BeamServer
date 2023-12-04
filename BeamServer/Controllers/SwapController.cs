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
using Nethereum.Util;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Numerics;
using System.Text.Json;

namespace BeamServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SwapController : ControllerBase
    {
        private IConfiguration _config;
        private readonly ILogger<SwapController> _logger;
        private readonly BeamDbContext _dbContext;
        private readonly IAssetsApi _assetsApi;
        private readonly IProfilesApi _profilesApi;
        private readonly IExchangeApi _exchangeApi;
        private readonly UserManager<BeamUser> _userManager;

        public SwapController(UserManager<BeamUser> userManager, ILogger<SwapController> logger, IConfiguration config, BeamDbContext dbContext, IAssetsApi assetsApi, IProfilesApi profilesApi, IExchangeApi exchangeApi)
        {
            _logger = logger;
            _userManager = userManager;
            _config = config;
            _dbContext = dbContext;
            _assetsApi = assetsApi;
            _profilesApi = profilesApi;
            _exchangeApi = exchangeApi;
        }

        private string GetProfile(string userName)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.UserName == userName);
            return user.ProfileId;
        }


        [HttpGet()]
        public async Task<BalanceDto> Get()
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

            decimal beamcoin = 0;
            decimal native = 0;

            var profCurrency = await _assetsApi.GetProfileCurrenciesAsync(profile);
            if (profCurrency.TryOk(out GetProfileCurrenciesResponse res))
            {
                var addresCoin = _config["BeamCoinContract"];
                var addressZero = "0x0000000000000000000000000000000000000000";
                beamcoin = res.Data.Where(x => x.Address == addresCoin).Select(x =>
                  {
                      return decimal.Parse(x.Balance);
                      /* var eth = UnitConversion.Convert.FromWeiToBigDecimal(bal);
                       return (decimal)eth;*/
                  }).FirstOrDefault();

                var profCurrency2 = await _assetsApi.GetProfileNativeCurrencyAsync(profile);
                if (profCurrency2.TryOk(out var res2))
                {
                    native = decimal.Parse(res2.NativeTokenBalance.Balance);
                }
            }
            return new BalanceDto() { Address = wallet, BeamonCoin = beamcoin, Native = native };
        }

        [HttpGet("Price")]
        public async Task<decimal> GetPrice(decimal price)
        {
            try
            {
                var addresCoin = _config["BeamCoinContract"];
                var addressZero = "0x0000000000000000000000000000000000000000";
                var profile = GetProfile(User.Identity.Name);
                var profileBeam = await _profilesApi.CreateProfileAsync(new CreateProfileRequestInput(profile));
                var quoteOutput = await _exchangeApi.GetQuoteForOutputAsync(addresCoin, addressZero, price.ToString());

                if (quoteOutput.TryOk(out var result))
                {
                    return decimal.Parse(result.AmountOut);
                    /* var bal = BigInteger.Parse(result.AmountOut);
                     var eth = UnitConversion.Convert.FromWeiToBigDecimal(bal);
                     return (decimal)eth;*/
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }

            return 0;
        }


        [HttpPost()]
        public async Task Swap([FromBody] BalanceDto price)
        {
            try
            {
                var addresCoin = _config["BeamCoinContract"];
                var addressZero = "0x0000000000000000000000000000000000000000";
                var profile = GetProfile(User.Identity.Name);
                var profileBeam = await _profilesApi.CreateProfileAsync(new CreateProfileRequestInput(profile));
                ConvertTokenRequestInput input = new ConvertTokenRequestInput(price.BeamonCoin.ToString(), price.Native.ToString(), addresCoin, addressZero);

                var quoteOutput = await _exchangeApi.ConvertInputAsync(input, profile);

                if (!quoteOutput.IsOk)
                {
                    throw new Exception(quoteOutput.RawContent);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        [HttpPost("Update")]
        public async Task<IActionResult> UpdateUsername([FromBody] PlayerName playerName)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
            user.UserName = playerName.Name;
            await _userManager.UpdateAsync(user);
            await _userManager.UpdateNormalizedUserNameAsync(user);
            _profilesApi.CreateProfileAsync(new CreateProfileRequestInput(user.ProfileId));
            return Ok(playerName);
        }
    }
}
