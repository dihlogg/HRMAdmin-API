using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminHRM.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLeaveRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Employee_EmployeeId",
                table: "Employee");

            migrationBuilder.DropForeignKey(
                name: "FK_Employee_IdentityUser_UserId",
                table: "Employee");

            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Leaves_EmployeeId",
                table: "Employee");

            migrationBuilder.DropForeignKey(
                name: "FK_Employee_SubUnits_SubUnitId",
                table: "Employee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employee",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "LeaveId",
                table: "Employee");

            migrationBuilder.RenameTable(
                name: "Employee",
                newName: "Employees");

            migrationBuilder.RenameIndex(
                name: "IX_Employee_UserId",
                table: "Employees",
                newName: "IX_Employees_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Employee_SubUnitId",
                table: "Employees",
                newName: "IX_Employees_SubUnitId");

            migrationBuilder.RenameIndex(
                name: "IX_Employee_EmployeeId",
                table: "Employees",
                newName: "IX_Employees_EmployeeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employees",
                table: "Employees",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Leaves_EmployeeId",
                table: "Leaves",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Employees_EmployeeId",
                table: "Employees",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_IdentityUser_UserId",
                table: "Employees",
                column: "UserId",
                principalTable: "IdentityUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_SubUnits_SubUnitId",
                table: "Employees",
                column: "SubUnitId",
                principalTable: "SubUnits",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Leaves_Employees_EmployeeId",
                table: "Leaves",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Employees_EmployeeId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_IdentityUser_UserId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_SubUnits_SubUnitId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Leaves_Employees_EmployeeId",
                table: "Leaves");

            migrationBuilder.DropIndex(
                name: "IX_Leaves_EmployeeId",
                table: "Leaves");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employees",
                table: "Employees");

            migrationBuilder.RenameTable(
                name: "Employees",
                newName: "Employee");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_UserId",
                table: "Employee",
                newName: "IX_Employee_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_SubUnitId",
                table: "Employee",
                newName: "IX_Employee_SubUnitId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_EmployeeId",
                table: "Employee",
                newName: "IX_Employee_EmployeeId");

            migrationBuilder.AddColumn<Guid>(
                name: "LeaveId",
                table: "Employee",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employee",
                table: "Employee",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Employee_EmployeeId",
                table: "Employee",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_IdentityUser_UserId",
                table: "Employee",
                column: "UserId",
                principalTable: "IdentityUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Leaves_EmployeeId",
                table: "Employee",
                column: "EmployeeId",
                principalTable: "Leaves",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_SubUnits_SubUnitId",
                table: "Employee",
                column: "SubUnitId",
                principalTable: "SubUnits",
                principalColumn: "Id");
        }
    }
}
