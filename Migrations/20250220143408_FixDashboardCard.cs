using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminHRM.Migrations
{
    /// <inheritdoc />
    public partial class FixDashboardCard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DashboardCards",
                columns: table => new
                {
                    CardId = table.Column<string>(type: "text", nullable: false),
                    CardName = table.Column<string>(type: "text", nullable: false),
                    CardIcon = table.Column<string>(type: "text", nullable: true),
                    DisplayOrder = table.Column<int>(type: "integer", nullable: false),
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreateBy = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdateBy = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DashboardCards", x => x.CardId);
                });

            migrationBuilder.CreateTable(
                name: "RequestReason",
                columns: table => new
                {
                    ReasonId = table.Column<string>(type: "text", nullable: false),
                    ReasonName = table.Column<string>(type: "text", nullable: true),
                    DisplayOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestReason", x => x.ReasonId);
                });

            migrationBuilder.CreateTable(
                name: "RequestStatus",
                columns: table => new
                {
                    StatusId = table.Column<string>(type: "text", nullable: false),
                    StatusName = table.Column<string>(type: "text", nullable: true),
                    DisplayOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestStatus", x => x.StatusId);
                });

            migrationBuilder.CreateTable(
                name: "RequestType",
                columns: table => new
                {
                    RequestId = table.Column<string>(type: "text", nullable: false),
                    TypeName = table.Column<string>(type: "text", nullable: true),
                    DisplayOrder = table.Column<int>(type: "integer", nullable: false),
                    CardId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestType", x => x.RequestId);
                    table.ForeignKey(
                        name: "FK_RequestType_DashboardCards_CardId",
                        column: x => x.CardId,
                        principalTable: "DashboardCards",
                        principalColumn: "CardId");
                });

            migrationBuilder.CreateTable(
                name: "LeaveRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    LeaveId = table.Column<Guid>(type: "uuid", nullable: false),
                    RequestTypeId = table.Column<string>(type: "text", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DetailReason = table.Column<string>(type: "text", nullable: true),
                    ExpectApprove = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ApprovedId = table.Column<Guid>(type: "uuid", nullable: true),
                    SuppervisorId = table.Column<Guid>(type: "uuid", nullable: true),
                    ReasonId = table.Column<string>(type: "text", nullable: true),
                    StatusId = table.Column<string>(type: "text", nullable: true),
                    RequestReasonReasonId = table.Column<string>(type: "text", nullable: false),
                    RequestStatusStatusId = table.Column<string>(type: "text", nullable: false),
                    CardId = table.Column<string>(type: "text", nullable: true),
                    SupervisorId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreateBy = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdateBy = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeaveRequests_DashboardCards_CardId",
                        column: x => x.CardId,
                        principalTable: "DashboardCards",
                        principalColumn: "CardId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LeaveRequests_Employees_ApprovedId",
                        column: x => x.ApprovedId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LeaveRequests_Employees_SuppervisorId",
                        column: x => x.SuppervisorId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LeaveRequests_RequestReason_RequestReasonReasonId",
                        column: x => x.RequestReasonReasonId,
                        principalTable: "RequestReason",
                        principalColumn: "ReasonId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LeaveRequests_RequestStatus_RequestStatusStatusId",
                        column: x => x.RequestStatusStatusId,
                        principalTable: "RequestStatus",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LeaveRequests_RequestType_RequestTypeId",
                        column: x => x.RequestTypeId,
                        principalTable: "RequestType",
                        principalColumn: "RequestId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RequestInformUser",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    EmployeeId = table.Column<string>(type: "text", nullable: false),
                    LeaveId = table.Column<string>(type: "text", nullable: false),
                    EmployeeId1 = table.Column<Guid>(type: "uuid", nullable: false),
                    LeaveRequestId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestInformUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestInformUser_Employees_EmployeeId1",
                        column: x => x.EmployeeId1,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequestInformUser_LeaveRequests_LeaveRequestId",
                        column: x => x.LeaveRequestId,
                        principalTable: "LeaveRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequests_ApprovedId",
                table: "LeaveRequests",
                column: "ApprovedId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequests_CardId",
                table: "LeaveRequests",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequests_RequestReasonReasonId",
                table: "LeaveRequests",
                column: "RequestReasonReasonId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequests_RequestStatusStatusId",
                table: "LeaveRequests",
                column: "RequestStatusStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequests_RequestTypeId",
                table: "LeaveRequests",
                column: "RequestTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequests_SuppervisorId",
                table: "LeaveRequests",
                column: "SuppervisorId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestInformUser_EmployeeId1",
                table: "RequestInformUser",
                column: "EmployeeId1");

            migrationBuilder.CreateIndex(
                name: "IX_RequestInformUser_LeaveRequestId",
                table: "RequestInformUser",
                column: "LeaveRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestType_CardId",
                table: "RequestType",
                column: "CardId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RequestInformUser");

            migrationBuilder.DropTable(
                name: "LeaveRequests");

            migrationBuilder.DropTable(
                name: "RequestReason");

            migrationBuilder.DropTable(
                name: "RequestStatus");

            migrationBuilder.DropTable(
                name: "RequestType");

            migrationBuilder.DropTable(
                name: "DashboardCards");
        }
    }
}
