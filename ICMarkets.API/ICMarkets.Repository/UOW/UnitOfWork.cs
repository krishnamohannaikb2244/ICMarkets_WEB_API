using ICMarkets.Repository.Data;
using ICMarkets.Repository.Repositories;

namespace ICMarkets.Repository.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BlockchainDbContext _context;
        private IBlockchainHistoryRepository? _blockchainHistory;

        public UnitOfWork(BlockchainDbContext context)
        {
            _context = context;
        }

        public IBlockchainHistoryRepository BlockchainHistory => 
            _blockchainHistory ??= new BlockchainHistoryRepository(_context);

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
