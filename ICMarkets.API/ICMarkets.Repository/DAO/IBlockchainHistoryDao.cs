using ICMarkets.Models;

namespace ICMarkets.Repository.DAO
{
    public interface IBlockchainHistoryDao
    {
        Task AddAsync(BlockchainData data);
        Task<IEnumerable<BlockchainData>> GetHistoryAsync(string name);
    }
}
