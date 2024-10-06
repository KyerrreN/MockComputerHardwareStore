using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComputerHardwareStore.Migrations
{
    /// <inheritdoc />
    public partial class AddBenchmarkTableAndSeedIt : Migration
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
                    Fps = table.Column<decimal>(type: "decimal(3,1)", precision: 3, scale: 1, nullable: false),
                    Resolution = table.Column<int>(type: "int", nullable: false),
                    Settings = table.Column<int>(type: "int", nullable: false),
                    GraphicsCardId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Benchmarks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Benchmarks_Products_GraphicsCardId",
                        column: x => x.GraphicsCardId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Benchmarks",
                columns: new[] { "Id", "Fps", "GameName", "GraphicsCardId", "Resolution", "Settings" },
                values: new object[] { 1, 96.7m, "Cyberpunk 2077", new Guid("50ea126c-b789-4a14-bb74-153ef1cb018b"), 0, 0 });

            migrationBuilder.CreateIndex(
                name: "IX_Benchmarks_GraphicsCardId",
                table: "Benchmarks",
                column: "GraphicsCardId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Benchmarks");
        }
    }
}
