using Microsoft.EntityFrameworkCore;
using ICMarkets.Models;

namespace ICMarkets.Repository.Data
{
    public class BlockchainDbContext : DbContext
    {
        public BlockchainDbContext(DbContextOptions<BlockchainDbContext> options) : base(options)
        {
        }

        public DbSet<BlockchainData> BlockchainHistory { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BlockchainData>(entity =>
            {
                entity.ToTable("BlockchainHistory");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
                entity.Property(e => e.CreatedAt).IsRequired();
            });
        }
    }
}
