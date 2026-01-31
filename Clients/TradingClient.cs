using System.Security.Cryptography;
using System.Text;

namespace trading_bot_testnet_1.Clients
{
    public class TradingClient
    {
        private static readonly string apiKey = "otxmDplYYTRNKePnUFAaj5A2dLBpSl7ef4PcEsYiooG8h1138SYU9sTgDBThYsXG";
        private static readonly string secretKey = "OcPgWZycak6RGJw8F3DBqVimq2DHVBOdoNwdOUYYt1M4Bndn25slPVXfKdf1aX2Y";
        //private static readonly string baseUrl = "https://testnet.binancefuture.com/";
        static readonly string baseUrl = "https://testnet.binance.vision";
        public TradingClient() { }

        public async Task TradingTrigger()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("X-MBX-APIKEY", apiKey);

            var symbol = "BTCUSDT";
            var side = "BUY";
            var type = "MARKET";
            var quantity = "0.0001";

            var timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            var query = $"symbol={symbol}&side={side}&type={type}&quantity={quantity}&timestamp={timestamp}";

            var signature = Sign(query, secretKey);
            var fullQuery = $"{query}&signature={signature}";

            var url = $"{baseUrl}/api/v3/order";

            var content = new StringContent("", Encoding.UTF8, "application/x-www-form-urlencoded");
            var response = await client.PostAsync($"{url}?{fullQuery}", content);
            var result = await response.Content.ReadAsStringAsync();

            Console.WriteLine("Order Response:");
            Console.WriteLine(result);
        }
        public static string Sign(string message, string secret)
        {
            var encoding = Encoding.UTF8;
            byte[] keyBytes = encoding.GetBytes(secret);
            byte[] messageBytes = encoding.GetBytes(message);

            using var hmac = new HMACSHA256(keyBytes);
            byte[] hash = hmac.ComputeHash(messageBytes);

            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }
    }
}
