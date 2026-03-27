using Microsoft.EntityFrameworkCore;
using ICMarkets.Models;
using ICMarkets.Repository.Data;

namespace ICMarkets.Repository.Repositories
{
    public class BlockchainHistoryRepository : IBlockchainHistoryRepository
    {
        private readonly BlockchainDbContext _context;

        public BlockchainHistoryRepository(BlockchainDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(BlockchainData data)
        {
            await _context.BlockchainHistory.AddAsync(data);
        }

        public async Task<IEnumerable<BlockchainData>> GetHistoryAsync(string keyword)
        {
            return await _context.BlockchainHistory
                .Where(x => x.Name.ToLower().Contains(keyword.ToLower()))
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }
    }
}
