using ICMarkets.Models;

namespace ICMarkets.Services
{
    public interface IBlockchainService
    {
        Task<BlockchainResponse?> SyncBlockchainDataAsync(string blockchain, string network = "main");
        Task<IEnumerable<BlockchainResponse>> GetHistoryAsync(string blockchain);
    }
}
