using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminHRM.Migrations
{
    /// <inheritdoc />
    public partial class LeaveRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leaves_SubUnits_SubUnitId",
                table: "Leaves");

            migrationBuilder.DropIndex(
                name: "IX_Leaves_SubUnitId",
                table: "Leaves");

            migrationBuilder.DropColumn(
                name: "EmployeeName",
                table: "Leaves");

            migrationBuilder.DropColumn(
                name: "SubUnitId",
                table: "Leaves");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmployeeName",
                table: "Leaves",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SubUnitId",
                table: "Leaves",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Leaves_SubUnitId",
                table: "Leaves",
                column: "SubUnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Leaves_SubUnits_SubUnitId",
                table: "Leaves",
                column: "SubUnitId",
                principalTable: "SubUnits",
                principalColumn: "Id");
        }
    }
}
