using Microsoft.EntityFrameworkCore.Migrations;

namespace MAV.Cms.Infrastructure.Data.Migrations
{
    public partial class generalSettingsProjectsMovedTransToMain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HappyCustomer",
                table: "GeneralSettingsTrans");

            migrationBuilder.DropColumn(
                name: "ProjectDone",
                table: "GeneralSettingsTrans");

            migrationBuilder.DropColumn(
                name: "YearsOfExperienced",
                table: "GeneralSettingsTrans");

            migrationBuilder.AddColumn<int>(
                name: "HappyCustomer",
                table: "GeneralSettings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProjectDone",
                table: "GeneralSettings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "YearsOfExperienced",
                table: "GeneralSettings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HappyCustomer",
                table: "GeneralSettings");

            migrationBuilder.DropColumn(
                name: "ProjectDone",
                table: "GeneralSettings");

            migrationBuilder.DropColumn(
                name: "YearsOfExperienced",
                table: "GeneralSettings");

            migrationBuilder.AddColumn<int>(
                name: "HappyCustomer",
                table: "GeneralSettingsTrans",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProjectDone",
                table: "GeneralSettingsTrans",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "YearsOfExperienced",
                table: "GeneralSettingsTrans",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
