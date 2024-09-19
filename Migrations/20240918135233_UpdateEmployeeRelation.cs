using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminHRM.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEmployeeRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_IdentityUser_UserId",
                table: "Employees");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Employees",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_IdentityUser_UserId",
                table: "Employees",
                column: "UserId",
                principalTable: "IdentityUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_IdentityUser_UserId",
                table: "Employees");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Employees",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_IdentityUser_UserId",
                table: "Employees",
                column: "UserId",
                principalTable: "IdentityUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
