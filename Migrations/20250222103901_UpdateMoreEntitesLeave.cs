using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminHRM.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMoreEntitesLeave : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CreateBy",
                table: "RequestType",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "RequestType",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "UpdateBy",
                table: "RequestType",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "RequestType",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreateBy",
                table: "RequestStatus",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "RequestStatus",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "UpdateBy",
                table: "RequestStatus",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "RequestStatus",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreateBy",
                table: "RequestReason",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "RequestReason",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "UpdateBy",
                table: "RequestReason",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "RequestReason",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateBy",
                table: "RequestType");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "RequestType");

            migrationBuilder.DropColumn(
                name: "UpdateBy",
                table: "RequestType");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "RequestType");

            migrationBuilder.DropColumn(
                name: "CreateBy",
                table: "RequestStatus");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "RequestStatus");

            migrationBuilder.DropColumn(
                name: "UpdateBy",
                table: "RequestStatus");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "RequestStatus");

            migrationBuilder.DropColumn(
                name: "CreateBy",
                table: "RequestReason");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "RequestReason");

            migrationBuilder.DropColumn(
                name: "UpdateBy",
                table: "RequestReason");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "RequestReason");
        }
    }
}
