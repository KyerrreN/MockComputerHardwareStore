using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ComputerHardwareStore.Migrations
{
    /// <inheritdoc />
    public partial class AddingIdentityRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "9184d38f-2ccb-4e94-af86-d6780e9ac68c", null, "Consumer", "CONSUMER" },
                    { "a4304e8f-1255-461c-8b8d-dd25ed8371a0", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9184d38f-2ccb-4e94-af86-d6780e9ac68c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a4304e8f-1255-461c-8b8d-dd25ed8371a0");
        }
    }
}
