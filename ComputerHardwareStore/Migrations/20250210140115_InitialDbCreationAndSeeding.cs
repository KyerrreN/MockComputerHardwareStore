using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ComputerHardwareStore.Migrations
{
    /// <inheritdoc />
    public partial class InitialDbCreationAndSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Benchmarks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Resolution = table.Column<int>(type: "int", nullable: false),
                    Settings = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Benchmarks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GraphicsCards",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Distributor = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Manufacturer = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Model = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    BaseClockSpeed = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: false),
                    MaxClockSpeed = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: false),
                    MemoryClockSpeed = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: false),
                    ConnectorPins = table.Column<byte>(type: "tinyint", nullable: false),
                    IsSupportRtx = table.Column<bool>(type: "bit", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    StockQuantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GraphicsCards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GraphicsCardBenchmarks",
                columns: table => new
                {
                    GraphicsCardId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BenchmarkId = table.Column<int>(type: "int", nullable: false),
                    Fps = table.Column<decimal>(type: "decimal(4,1)", precision: 4, scale: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GraphicsCardBenchmarks", x => new { x.GraphicsCardId, x.BenchmarkId });
                    table.ForeignKey(
                        name: "FK_GraphicsCardBenchmarks_Benchmarks_BenchmarkId",
                        column: x => x.BenchmarkId,
                        principalTable: "Benchmarks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GraphicsCardBenchmarks_GraphicsCards_GraphicsCardId",
                        column: x => x.GraphicsCardId,
                        principalTable: "GraphicsCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Benchmarks",
                columns: new[] { "Id", "GameName", "Resolution", "Settings" },
                values: new object[,]
                {
                    { 1, "Cyberpunk 2077", 0, 0 },
                    { 2, "Cyberpunk 2077", 1, 0 },
                    { 3, "Cyberpunk 2077", 2, 0 },
                    { 4, "Horizon: Zero Dawn", 0, 0 },
                    { 5, "Horizon: Zero Dawn", 1, 0 },
                    { 6, "Horizon: Zero Dawn", 1, 0 },
                    { 7, "Marvel Rivals", 2, 0 }
                });

            migrationBuilder.InsertData(
                table: "GraphicsCards",
                columns: new[] { "Id", "BaseClockSpeed", "ConnectorPins", "Distributor", "IsSupportRtx", "Manufacturer", "MaxClockSpeed", "MemoryClockSpeed", "Model", "Price", "StockQuantity" },
                values: new object[,]
                {
                    { new Guid("50ea126c-b789-4a14-bb74-153ef1cb018b"), "2295", (byte)16, "MSI", true, "NVIDIA", "2595", "23000", "RTX 4080 Super", 1139.99m, 21 },
                    { new Guid("5280896c-12eb-49fc-a011-1bee7b7032f0"), "1210", (byte)14, "Sapphire", false, "AMD", "1411", "8000", "RX 580", 80.39m, 3 },
                    { new Guid("b5d628f0-d4e2-4d63-920d-9aeaae84c418"), "1470", (byte)8, "MSI", true, "NVIDIA", "1650", "14000", "RTX 2060 Super", 262.17m, 17 }
                });

            migrationBuilder.InsertData(
                table: "GraphicsCardBenchmarks",
                columns: new[] { "BenchmarkId", "GraphicsCardId", "Fps" },
                values: new object[,]
                {
                    { 1, new Guid("b5d628f0-d4e2-4d63-920d-9aeaae84c418"), 96.7m },
                    { 2, new Guid("b5d628f0-d4e2-4d63-920d-9aeaae84c418"), 112.3m },
                    { 3, new Guid("b5d628f0-d4e2-4d63-920d-9aeaae84c418"), 164.2m },
                    { 4, new Guid("b5d628f0-d4e2-4d63-920d-9aeaae84c418"), 24.3m },
                    { 5, new Guid("b5d628f0-d4e2-4d63-920d-9aeaae84c418"), 48.5m },
                    { 6, new Guid("b5d628f0-d4e2-4d63-920d-9aeaae84c418"), 76.1m },
                    { 7, new Guid("b5d628f0-d4e2-4d63-920d-9aeaae84c418"), 63.4m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_GraphicsCardBenchmarks_BenchmarkId",
                table: "GraphicsCardBenchmarks",
                column: "BenchmarkId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GraphicsCardBenchmarks");

            migrationBuilder.DropTable(
                name: "Benchmarks");

            migrationBuilder.DropTable(
                name: "GraphicsCards");
        }
    }
}
