using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminHRM.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEntityLeaveTabel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "Leaves",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comment",
                table: "Leaves");
        }
    }
}
