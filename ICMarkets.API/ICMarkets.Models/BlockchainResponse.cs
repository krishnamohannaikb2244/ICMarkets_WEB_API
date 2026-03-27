using System.Text.Json.Serialization;

namespace ICMarkets.Models
{
    public class BlockchainResponse
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("height")]
        public long Height { get; set; }

        [JsonPropertyName("version")]
        public string Version { get; set; } = string.Empty;

        [JsonPropertyName("mrkl_root")]
        public string MerkleRoot { get; set; } = string.Empty;

        [JsonPropertyName("time")]
        public DateTime Time { get; set; }

        [JsonPropertyName("bits")]
        public long Bits { get; set; }

        [JsonPropertyName("nonce")]
        public long Nonce { get; set; }

        [JsonPropertyName("peer_count")]
        public int PeerCount { get; set; }

        [JsonPropertyName("unconfirmed_count")]
        public int UnconfirmedCount { get; set; }

        [JsonPropertyName("last_fork_height")]
        public long LastForkHeight { get; set; }

        [JsonPropertyName("last_fork_hash")]
        public string LastForkHash { get; set; } = string.Empty;

        [JsonPropertyName("previous_hash")]
        public string PreviousHash { get; set; } = string.Empty;

        [JsonPropertyName("previous_url")]
        public string PreviousUrl { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
