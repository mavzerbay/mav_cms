using Microsoft.EntityFrameworkCore.Migrations;

namespace MAV.Cms.Infrastructure.Data.Migrations
{
    public partial class pageAndGeneralSettingsTransKeywordsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OgKeywords",
                table: "PageTrans",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "ContactOgKeywords",
                table: "GeneralSettingsTrans",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "HomeOgKeywords",
                table: "GeneralSettingsTrans",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OgKeywords",
                table: "PageTrans");

            migrationBuilder.DropColumn(
                name: "ContactOgKeywords",
                table: "GeneralSettingsTrans");

            migrationBuilder.DropColumn(
                name: "HomeOgKeywords",
                table: "GeneralSettingsTrans");
        }
    }
}
