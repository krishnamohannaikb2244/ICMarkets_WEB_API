using Microsoft.AspNetCore.Mvc;
using ICMarkets.API.Helpers;
using ICMarkets.Models;

namespace ICMarkets.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlockchainController : ControllerBase
    {
        private readonly IBlockcypherHelper _blockcypherHelper;
        private readonly ILogger<BlockchainController> _logger;

        public BlockchainController(IBlockcypherHelper blockcypherHelper, ILogger<BlockchainController> logger)
        {
            _blockcypherHelper = blockcypherHelper;
            _logger = logger;
        }

        [HttpGet("{chain}/{network}")]
        public async Task<ActionResult<BlockchainResponse>> GetBlockchainDataAsync(string chain, string network = "main")
        {
            var data = await _blockcypherHelper.GetBlockchainDataAsync(chain, network);
            
            if (data == null)
            {
                return NotFound($"Could not fetch data for {chain}/{network}");
            }

            return Ok(data);
        }

        [HttpGet("history/{chain}")]
        public async Task<ActionResult<IEnumerable<BlockchainResponse>>> GetHistoryAsync(string chain)
        {
            return Ok(new List<BlockchainResponse>());
        }
    }
}
