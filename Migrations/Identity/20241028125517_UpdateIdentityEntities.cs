using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AdminHRM.Migrations.Identity
{
    /// <inheritdoc />
    public partial class UpdateIdentityEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "78bf5349-c330-4e84-9ed2-69acbc477526");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a2cc107d-75d8-4396-9f54-58f00394e926");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fd8a07e5-0c5c-48c6-94b0-bffa37b77636");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { "78bf5349-c330-4e84-9ed2-69acbc477526", "12:54:43 PM", "Admin", "ADMIN" },
                    { "a2cc107d-75d8-4396-9f54-58f00394e926", "12:54:43 PM", "Human Resources", "HUMAN RESOURCES" },
                    { "fd8a07e5-0c5c-48c6-94b0-bffa37b77636", "12:54:43 PM", "User", "USER" }
                });
        }
    }
}
