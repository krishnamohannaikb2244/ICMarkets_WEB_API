using System.Text.Json;
using ICMarkets.Models;

namespace ICMarkets.API.Helpers
{
    public interface IBlockcypherHelper
    {
        Task<BlockchainResponse?> GetBlockchainDataAsync(string blockchain, string network = "main");
    }

    public class BlockcypherHelper : IBlockcypherHelper
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<BlockcypherHelper> _logger;
        private const string BaseUrl = "https://api.blockcypher.com/v1/";

        public BlockcypherHelper(HttpClient httpClient, ILogger<BlockcypherHelper> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<BlockchainResponse?> GetBlockchainDataAsync(string blockchain, string network = "main")
        {
            try
            {
                var url = $"{BaseUrl}{blockchain}/{network}";

                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<BlockchainResponse>(content);

                if (data != null)
                {
                    data.CreatedAt = DateTime.UtcNow;
                }

                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching blockchain data for {Blockchain}/{Network}", blockchain, network);
                return null;
            }
        }
    }
}
