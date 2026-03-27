using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ICMarkets.Models;
using ICMarkets.Services;
using ICMarkets.API.Controllers;

namespace ICMarkets.API.UnitTest
{
    [TestFixture]
    public class BlockchainControllerTests
    {
        private Mock<IBlockchainService> _mockService;
        private Mock<ILogger<BlockchainController>> _mockLogger;
        private BlockchainController _controller;

        [SetUp]
        public void Setup()
        {
            _mockService = new Mock<IBlockchainService>();
            _mockLogger = new Mock<ILogger<BlockchainController>>();
            _controller = new BlockchainController(_mockService.Object, _mockLogger.Object);
        }

        [Test]
        public async Task GetBlockchainDataAsync_ReturnsOk_WhenDataExists()
        {
            var response = new BlockchainResponse { Name = "btc" };
            _mockService.Setup(s => s.SyncBlockchainDataAsync("btc", "main"))
                       .ReturnsAsync(response);

            var result = await _controller.GetBlockchainDataAsync("btc", "main");

            Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult?.Value, Is.EqualTo(response));
        }

        [Test]
        public async Task GetBlockchainDataAsync_ReturnsNotFound_WhenDataIsNull()
        {
            _mockService.Setup(s => s.SyncBlockchainDataAsync(It.IsAny<string>(), It.IsAny<string>()))
                       .ReturnsAsync((BlockchainResponse?)null);

            var result = await _controller.GetBlockchainDataAsync("invalid", "main");

            Assert.That(result.Result, Is.InstanceOf<NotFoundObjectResult>());
        }

        [Test]
        public async Task GetHistoryAsync_ReturnsOk_WithHistory()
        {
            var history = new List<BlockchainResponse> { new BlockchainResponse { Name = "btc" } };
            _mockService.Setup(s => s.GetHistoryAsync("btc"))
                       .ReturnsAsync(history);

            var result = await _controller.GetHistoryAsync("btc");

            Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult?.Value, Is.EqualTo(history));
        }
    }
}
