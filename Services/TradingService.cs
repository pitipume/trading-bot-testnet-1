using trading_bot_testnet_1.Clients.Interfaces;

namespace trading_bot_testnet_1.Services
{
    public class TradingService
    {
        private readonly ITradingClient _tradingClient;
        public TradingService(
            ITradingClient tradingClient
            ) 
        {
            _tradingClient = tradingClient;
        }

        public async Task TradingTrigger()
        {
            await _tradingClient.TradingTrigger();
        }
    }
}
