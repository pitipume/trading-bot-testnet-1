using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using trading_bot_testnet_1.Services.Interfaces;

namespace trading_bot_testnet_1.Controllers
{
    [ApiController]
    [Route("service/trading")]
    public class TradingController : ControllerBase
    {
        private readonly ITradingService _tradingService;
        public TradingController(
            ITradingService tradingService
            ) 
        {
            _tradingService = tradingService;
        }

        [HttpPost]
        [Route("trading/trigger")]
        [SwaggerOperation(Description = "Trading trigger logic", OperationId = "Trading trigger logic")]
        public async Task<IActionResult> TradingTrigger()
        {
            await _tradingService.TradingTrigger();
            return Ok();
        }


    }
}
