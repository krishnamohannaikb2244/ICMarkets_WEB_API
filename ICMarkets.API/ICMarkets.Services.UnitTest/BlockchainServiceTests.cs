using Moq;
using ICMarkets.Models;
using ICMarkets.Services.Helpers;
using ICMarkets.Repository.UOW;
using ICMarkets.Repository.Repositories;
using ICMarkets.Services;

namespace ICMarkets.Services.UnitTest
{
    [TestFixture]
    public class BlockchainServiceTests
    {
        private Mock<IBlockcypherHelper> _mockHelper;
        private Mock<IUnitOfWork> _mockUow;
        private Mock<IBlockchainHistoryRepository> _mockRepo;
        private BlockchainService _service;

        [SetUp]
        public void Setup()
        {
            _mockHelper = new Mock<IBlockcypherHelper>();
            _mockUow = new Mock<IUnitOfWork>();
            _mockRepo = new Mock<IBlockchainHistoryRepository>();

            _mockUow.Setup(u => u.BlockchainHistory).Returns(_mockRepo.Object);

            _service = new BlockchainService(_mockHelper.Object, _mockUow.Object);
        }

        [Test]
        public async Task SyncBlockchainDataAsync_WhenDataFetched_ShouldSaveToDb()
        {
            var blockchain = "btc";
            var network = "main";
            var responseDto = new BlockchainResponse { Name = "BTC.main", Height = 100 };

            _mockHelper.Setup(h => h.GetBlockchainDataAsync(blockchain, network))
                       .ReturnsAsync(responseDto);

            var result = await _service.SyncBlockchainDataAsync(blockchain, network);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo("BTC.main"));
            _mockRepo.Verify(r => r.AddAsync(It.IsAny<BlockchainData>()), Times.Once);
            _mockUow.Verify(u => u.CommitAsync(), Times.Once);
        }

        [Test]
        public async Task SyncBlockchainDataAsync_WhenDataFetchFails_ShouldNotSaveToDb()
        {
            _mockHelper.Setup(h => h.GetBlockchainDataAsync(It.IsAny<string>(), It.IsAny<string>()))
                       .ReturnsAsync((BlockchainResponse?)null);

            var result = await _service.SyncBlockchainDataAsync("btc");

            Assert.That(result, Is.Null);
            _mockRepo.Verify(r => r.AddAsync(It.IsAny<BlockchainData>()), Times.Never);
            _mockUow.Verify(u => u.CommitAsync(), Times.Never);
        }

        [Test]
        public async Task GetHistoryAsync_ShouldReturnHistoryFromRepo()
        {
            var chain = "btc";
            var historyEntities = new List<BlockchainData>
            {
                new BlockchainData { Name = "BTC.main", Height = 100 },
                new BlockchainData { Name = "BTC.main", Height = 99 }
            };

            _mockRepo.Setup(r => r.GetHistoryAsync(chain))
                     .ReturnsAsync(historyEntities);

            var result = await _service.GetHistoryAsync(chain);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(2));
            _mockRepo.Verify(r => r.GetHistoryAsync(chain), Times.Once);
        }
    }
}
