using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AdminHRM.Migrations.Identity
{
    /// <inheritdoc />
    public partial class UpdateIdentityEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2e1d18ba-81c6-4a02-a9f8-661e36a4a6c3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "418aaaf8-7859-4d17-aebf-f028351ab13a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "98c97d8d-3127-422a-8540-44ba4db6d6cd");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { "2e1d18ba-81c6-4a02-a9f8-661e36a4a6c3", "12:26:27 PM", "Human Resources", "HUMAN RESOURCES" },
                    { "418aaaf8-7859-4d17-aebf-f028351ab13a", "12:26:27 PM", "Admin", "ADMIN" },
                    { "98c97d8d-3127-422a-8540-44ba4db6d6cd", "12:26:27 PM", "User", "USER" }
                });
        }
    }
}
