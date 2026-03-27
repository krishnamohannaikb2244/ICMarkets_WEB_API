using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ICMarkets.Repository.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BlockchainHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Height = table.Column<long>(type: "bigint", nullable: false),
                    Version = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    MrklRoot = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Bits = table.Column<long>(type: "bigint", nullable: false),
                    Nonce = table.Column<long>(type: "bigint", nullable: false),
                    PeerCount = table.Column<int>(type: "int", nullable: false),
                    UnconfirmedCount = table.Column<int>(type: "int", nullable: false),
                    LastForkHeight = table.Column<long>(type: "bigint", nullable: false),
                    LastForkHash = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PreviousHash = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PreviousUrl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlockchainHistory", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlockchainHistory");
        }
    }
}
