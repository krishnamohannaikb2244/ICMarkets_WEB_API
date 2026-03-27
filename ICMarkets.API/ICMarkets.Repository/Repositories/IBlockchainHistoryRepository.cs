using ICMarkets.Models;

namespace ICMarkets.Repository.Repositories
{
    public interface IBlockchainHistoryRepository
    {
        Task AddAsync(BlockchainData data);
        Task<IEnumerable<BlockchainData>> GetHistoryAsync(string keyword);
    }
}
