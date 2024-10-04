using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ComputerHardwareStore.Migrations
{
    /// <inheritdoc />
    public partial class InitialDataSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { new Guid("9e768b71-f27d-426c-ac3d-41e7a7c26729"), "A graphics card (also called a video card)is a computer expansion card that generates a feed of graphics output to a display device such as a monitor. Essential part for gaming.", "GPU" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BaseClockSpeed", "CategoryId", "ConnectorPins", "Discriminator", "Distributor", "IsSupportRtx", "Manufacturer", "MaxClockSpeed", "MemoryClockSpeed", "Model", "Price", "StockQuantity" },
                values: new object[,]
                {
                    { new Guid("50ea126c-b789-4a14-bb74-153ef1cb018b"), "2295", new Guid("9e768b71-f27d-426c-ac3d-41e7a7c26729"), (byte)16, "GraphicsCard", "MSI", true, "NVIDIA", "2595", "23000", "RTX 4080 Super", 1139.99m, 21 },
                    { new Guid("5280896c-12eb-49fc-a011-1bee7b7032f0"), "1210", new Guid("9e768b71-f27d-426c-ac3d-41e7a7c26729"), (byte)14, "GraphicsCard", "Sapphire", false, "AMD", "1411", "8000", "RX 580", 80.39m, 3 },
                    { new Guid("b5d628f0-d4e2-4d63-920d-9aeaae84c418"), "1470", new Guid("9e768b71-f27d-426c-ac3d-41e7a7c26729"), (byte)8, "GraphicsCard", "MSI", true, "NVIDIA", "1650", "14000", "RTX 2060 Super", 262.17m, 17 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("50ea126c-b789-4a14-bb74-153ef1cb018b"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("5280896c-12eb-49fc-a011-1bee7b7032f0"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("b5d628f0-d4e2-4d63-920d-9aeaae84c418"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("9e768b71-f27d-426c-ac3d-41e7a7c26729"));
        }
    }
}
