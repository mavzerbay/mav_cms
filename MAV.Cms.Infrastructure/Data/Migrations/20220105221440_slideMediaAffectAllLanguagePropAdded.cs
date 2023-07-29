using Microsoft.EntityFrameworkCore.Migrations;

namespace MAV.Cms.Infrastructure.Data.Migrations
{
    public partial class slideMediaAffectAllLanguagePropAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AffectAllLanguage",
                table: "SlideMedia",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AffectAllLanguage",
                table: "SlideMedia");
        }
    }
}
