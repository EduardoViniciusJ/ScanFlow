using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScanFlowAWS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveRevokeFromToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Revoke",
                table: "Tokens");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Revoke",
                table: "Tokens",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
