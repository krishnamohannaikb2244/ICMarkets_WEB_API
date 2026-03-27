using ICMarkets.Models;
using ICMarkets.Repository.UOW;
using ICMarkets.Services.Helpers;
using ICMarkets.Common.Mappers;

namespace ICMarkets.Services
{
    public class BlockchainService : IBlockchainService
    {
        private readonly IBlockcypherHelper _blockcypherHelper;
        private readonly IUnitOfWork _unitOfWork;

        public BlockchainService(IBlockcypherHelper blockcypherHelper, IUnitOfWork unitOfWork)
        {
            _blockcypherHelper = blockcypherHelper;
            _unitOfWork = unitOfWork;
        }

        public async Task<BlockchainResponse?> SyncBlockchainDataAsync(string blockchain, string network = "main")
        {
            var dataDto = await _blockcypherHelper.GetBlockchainDataAsync(blockchain, network);

            if (dataDto != null)
            {
                var entity = BlockchainMapper.ToEntity(dataDto);
                await _unitOfWork.BlockchainHistory.AddAsync(entity);
                await _unitOfWork.CommitAsync();
            }

            return dataDto;
        }

        public async Task<IEnumerable<BlockchainResponse>> GetHistoryAsync(string blockchain)
        {
            var historyEntities = await _unitOfWork.BlockchainHistory.GetHistoryAsync(blockchain);
            return historyEntities.Select(BlockchainMapper.ToDto);
        }
    }
}
