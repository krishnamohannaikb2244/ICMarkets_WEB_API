using ICMarkets.Models;

namespace ICMarkets.Common.Mappers
{
    public static class BlockchainMapper
    {
        public static BlockchainData ToEntity(BlockchainResponse response)
        {
            return new BlockchainData
            {
                Name = response.Name,
                Height = response.Height,
                Version = response.Version,
                MrklRoot = response.MrklRoot,
                Time = response.Time,
                Bits = response.Bits,
                Nonce = response.Nonce,
                PeerCount = response.PeerCount,
                UnconfirmedCount = response.UnconfirmedCount,
                LastForkHeight = response.LastForkHeight,
                LastForkHash = response.LastForkHash,
                PreviousHash = response.PreviousHash,
                PreviousUrl = response.PreviousUrl,
                CreatedAt = response.CreatedAt
            };
        }

        public static BlockchainResponse ToDto(BlockchainData entity)
        {
            return new BlockchainResponse
            {
                Name = entity.Name,
                Height = entity.Height,
                Version = entity.Version,
                MrklRoot = entity.MrklRoot,
                Time = entity.Time,
                Bits = entity.Bits,
                Nonce = entity.Nonce,
                PeerCount = entity.PeerCount,
                UnconfirmedCount = entity.UnconfirmedCount,
                LastForkHeight = entity.LastForkHeight,
                LastForkHash = entity.LastForkHash,
                PreviousHash = entity.PreviousHash,
                PreviousUrl = entity.PreviousUrl,
                CreatedAt = entity.CreatedAt
            };
        }
    }
}
