using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminHRM.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSubUnit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_SubUnits_SubUnitId",
                table: "Employees");

            migrationBuilder.AlterColumn<Guid>(
                name: "SubUnitId",
                table: "Employees",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_SubUnits_SubUnitId",
                table: "Employees",
                column: "SubUnitId",
                principalTable: "SubUnits",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_SubUnits_SubUnitId",
                table: "Employees");

            migrationBuilder.AlterColumn<Guid>(
                name: "SubUnitId",
                table: "Employees",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_SubUnits_SubUnitId",
                table: "Employees",
                column: "SubUnitId",
                principalTable: "SubUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
