using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AdminHRM.Migrations.Identity
{
    /// <inheritdoc />
    public partial class InitialIdentityContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3b3fe165-65d1-43da-9e9c-8d4d3609286d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "61641272-6575-4997-a08d-a521f541c3ad");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7e77ff29-e56c-4c6f-83fd-adf78192385f");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "25d057e5-ac75-4511-85e8-c42df1749322", "10:13:35 AM", "Human Resources", "HUMAN RESOURCES" },
                    { "a4e97957-c0ff-4e51-a077-e2dee802a748", "10:13:35 AM", "User", "USER" },
                    { "bdf2cac0-f62c-4fb5-8074-6ae16c7a1daa", "10:13:35 AM", "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "25d057e5-ac75-4511-85e8-c42df1749322");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a4e97957-c0ff-4e51-a077-e2dee802a748");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bdf2cac0-f62c-4fb5-8074-6ae16c7a1daa");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3b3fe165-65d1-43da-9e9c-8d4d3609286d", "4:00:32 PM", "User", "USER" },
                    { "61641272-6575-4997-a08d-a521f541c3ad", "4:00:32 PM", "Human Resources", "HUMAN RESOURCES" },
                    { "7e77ff29-e56c-4c6f-83fd-adf78192385f", "4:00:32 PM", "Admin", "ADMIN" }
                });
        }
    }
}
