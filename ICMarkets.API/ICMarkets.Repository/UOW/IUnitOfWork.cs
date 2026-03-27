using ICMarkets.Repository.Repositories;

namespace ICMarkets.Repository.UOW
{
    public interface IUnitOfWork : IDisposable
    {
        IBlockchainHistoryRepository BlockchainHistory { get; }
        Task<int> CommitAsync();
    }
}
