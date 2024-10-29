using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AdminHRM.Migrations.Identity
{
    /// <inheritdoc />
    public partial class UpdateEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "353747cf-e825-4275-8510-c01bfb7443ba");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "79e732c7-0368-49ba-8add-558b65d0e496");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e23fbdbc-2ca0-4174-9032-92039767ac67");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "349953c4-6c74-480c-a0d6-195effa9bdff", "1:06:10 PM", "Admin", "ADMIN" },
                    { "b4dcfa99-3241-4082-aed1-7216142ad0bb", "1:06:10 PM", "User", "USER" },
                    { "ddae5372-3476-40af-bf4f-8f5380c8241f", "1:06:10 PM", "Human Resources", "HUMAN RESOURCES" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "349953c4-6c74-480c-a0d6-195effa9bdff");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b4dcfa99-3241-4082-aed1-7216142ad0bb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ddae5372-3476-40af-bf4f-8f5380c8241f");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "353747cf-e825-4275-8510-c01bfb7443ba", "12:55:17 PM", "Admin", "ADMIN" },
                    { "79e732c7-0368-49ba-8add-558b65d0e496", "12:55:17 PM", "User", "USER" },
                    { "e23fbdbc-2ca0-4174-9032-92039767ac67", "12:55:17 PM", "Human Resources", "HUMAN RESOURCES" }
                });
        }
    }
}
