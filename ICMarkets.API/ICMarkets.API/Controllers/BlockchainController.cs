using Microsoft.AspNetCore.Mvc;
using ICMarkets.Models;
using ICMarkets.Services;

namespace ICMarkets.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlockchainController : ControllerBase
    {
        private readonly IBlockchainService _blockchainService;
        private readonly ILogger<BlockchainController> _logger;

        public BlockchainController(IBlockchainService blockchainService, ILogger<BlockchainController> logger)
        {
            _blockchainService = blockchainService;
            _logger = logger;
        }

        [HttpGet("{chain}/{network}")]
        public async Task<ActionResult<BlockchainResponse>> GetBlockchainDataAsync(string chain, string network = "main")
        {
            try
            {
                var data = await _blockchainService.SyncBlockchainDataAsync(chain, network);
                
                if (data == null)
                {
                    return NotFound($"Could not fetch data for {chain}/{network}");
                }

                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetBlockchainDataAsync for {Chain}/{Network}", chain, network);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("history/{chain}")]
        public async Task<ActionResult<IEnumerable<BlockchainResponse>>> GetHistoryAsync(string chain)
        {
            try
            {
                var history = await _blockchainService.GetHistoryAsync(chain);
                return Ok(history);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetHistoryAsync for {Chain}", chain);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
