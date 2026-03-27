using Microsoft.EntityFrameworkCore;
using ICMarkets.Models;
using ICMarkets.Repository.Data;

namespace ICMarkets.Repository.DAO
{
    public class BlockchainHistoryDao : IBlockchainHistoryDao
    {
        private readonly BlockchainDbContext _context;

        public BlockchainHistoryDao(BlockchainDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(BlockchainData data)
        {
            await _context.BlockchainHistory.AddAsync(data);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<BlockchainData>> GetHistoryAsync(string name)
        {
            return await _context.BlockchainHistory
                .Where(x => x.Name.ToLower() == name.ToLower())
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }
    }
}
