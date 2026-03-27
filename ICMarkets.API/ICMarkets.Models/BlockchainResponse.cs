namespace ICMarkets.Models
{
    public class BlockchainResponse
    {
        public string Name { get; set; } = string.Empty;

        public long Height { get; set; }

        public string Version { get; set; } = string.Empty;

        public string MrklRoot { get; set; } = string.Empty;

        public DateTime Time { get; set; }

        public long Bits { get; set; }

        public long Nonce { get; set; }

        public int PeerCount { get; set; }

        public int UnconfirmedCount { get; set; }

        public long LastForkHeight { get; set; }

        public string LastForkHash { get; set; } = string.Empty;

        public string PreviousHash { get; set; } = string.Empty;

        public string PreviousUrl { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
