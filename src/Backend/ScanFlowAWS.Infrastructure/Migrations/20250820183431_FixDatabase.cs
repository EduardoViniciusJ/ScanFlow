using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScanFlowAWS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Emotions_DomainUsers_UserId",
                table: "Emotions");

            migrationBuilder.AddForeignKey(
                name: "FK_Emotions_DomainUsers_UserId",
                table: "Emotions",
                column: "UserId",
                principalTable: "DomainUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Emotions_DomainUsers_UserId",
                table: "Emotions");

            migrationBuilder.AddForeignKey(
                name: "FK_Emotions_DomainUsers_UserId",
                table: "Emotions",
                column: "UserId",
                principalTable: "DomainUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
