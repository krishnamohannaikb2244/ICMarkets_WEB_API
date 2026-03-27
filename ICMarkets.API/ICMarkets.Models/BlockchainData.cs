using System.ComponentModel.DataAnnotations;

namespace ICMarkets.Models
{
    public class BlockchainData
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        public long Height { get; set; }

        [MaxLength(10)]
        public string Version { get; set; } = string.Empty;

        [MaxLength(100)]
        public string MrklRoot { get; set; } = string.Empty;

        public DateTime Time { get; set; }

        public long Bits { get; set; }

        public long Nonce { get; set; }

        public int PeerCount { get; set; }

        public int UnconfirmedCount { get; set; }

        public long LastForkHeight { get; set; }

        [MaxLength(100)]
        public string LastForkHash { get; set; } = string.Empty;

        [MaxLength(100)]
        public string PreviousHash { get; set; } = string.Empty;

        [MaxLength(200)]
        public string PreviousUrl { get; set; } = string.Empty;

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
