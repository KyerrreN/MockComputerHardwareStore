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
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    BenchmarkId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Fps = table.Column<decimal>(type: "decimal(4,1)", precision: 4, scale: 1, nullable: false),
                    TestingTool = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    { new Guid("0be78fd9-f27c-4e59-a6d6-c26ee4fa93e4"), "Marvel Rivals", 2, 0 },
                    { new Guid("1d519389-f0de-4eef-b3d1-1938df9700f4"), "Cyberpunk 2077", 0, 0 },
                    { new Guid("27817726-bd13-4784-a3f3-0ab9b10d6190"), "Cyberpunk 2077", 2, 0 },
                    { new Guid("4092a98e-efef-480d-9cb0-d72238b62a51"), "Horizon: Zero Dawn", 1, 0 },
                    { new Guid("72cedae8-f648-4a4e-826b-47a85aec960e"), "Horizon: Zero Dawn", 0, 0 },
                    { new Guid("88d38fd2-8b75-4bf2-ba67-d43a7329c7b5"), "Cyberpunk 2077", 1, 0 },
                    { new Guid("ffdaf3aa-0c42-499c-b63b-1cca50a36b97"), "Horizon: Zero Dawn", 1, 0 }
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
                columns: new[] { "BenchmarkId", "GraphicsCardId", "Fps", "TestingTool" },
                values: new object[,]
                {
                    { new Guid("0be78fd9-f27c-4e59-a6d6-c26ee4fa93e4"), new Guid("b5d628f0-d4e2-4d63-920d-9aeaae84c418"), 63.4m, "RivaTuner Statistics Server" },
                    { new Guid("1d519389-f0de-4eef-b3d1-1938df9700f4"), new Guid("b5d628f0-d4e2-4d63-920d-9aeaae84c418"), 96.7m, "Fraps" },
                    { new Guid("27817726-bd13-4784-a3f3-0ab9b10d6190"), new Guid("b5d628f0-d4e2-4d63-920d-9aeaae84c418"), 164.2m, "RivaTuner Statistics Server" },
                    { new Guid("4092a98e-efef-480d-9cb0-d72238b62a51"), new Guid("b5d628f0-d4e2-4d63-920d-9aeaae84c418"), 76.1m, "RivaTuner Statistics Server" },
                    { new Guid("72cedae8-f648-4a4e-826b-47a85aec960e"), new Guid("b5d628f0-d4e2-4d63-920d-9aeaae84c418"), 24.3m, "Shadowplay" },
                    { new Guid("88d38fd2-8b75-4bf2-ba67-d43a7329c7b5"), new Guid("b5d628f0-d4e2-4d63-920d-9aeaae84c418"), 112.3m, "MSI Afterburner" },
                    { new Guid("ffdaf3aa-0c42-499c-b63b-1cca50a36b97"), new Guid("b5d628f0-d4e2-4d63-920d-9aeaae84c418"), 48.5m, "MSI Afterburner" }
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
