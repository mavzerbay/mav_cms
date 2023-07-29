using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MAV.Cms.Infrastructure.Data.Migrations
{
    public partial class generalSettingsAndSupportTicketPropsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsClosed",
                table: "SupportTicket",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "LatestProjectPageId",
                table: "GeneralSettings",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "PageId",
                table: "GeneralSettings",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_GeneralSettings_PageId",
                table: "GeneralSettings",
                column: "PageId");

            migrationBuilder.AddForeignKey(
                name: "FK_GeneralSettings_Page_PageId",
                table: "GeneralSettings",
                column: "PageId",
                principalTable: "Page",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GeneralSettings_Page_PageId",
                table: "GeneralSettings");

            migrationBuilder.DropIndex(
                name: "IX_GeneralSettings_PageId",
                table: "GeneralSettings");

            migrationBuilder.DropColumn(
                name: "IsClosed",
                table: "SupportTicket");

            migrationBuilder.DropColumn(
                name: "LatestProjectPageId",
                table: "GeneralSettings");

            migrationBuilder.DropColumn(
                name: "PageId",
                table: "GeneralSettings");
        }
    }
}
