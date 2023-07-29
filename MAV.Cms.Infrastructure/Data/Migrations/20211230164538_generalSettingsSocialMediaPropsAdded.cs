using Microsoft.EntityFrameworkCore.Migrations;

namespace MAV.Cms.Infrastructure.Data.Migrations
{
    public partial class generalSettingsSocialMediaPropsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GeneralSettings_Slide_TestimonailSlideId",
                table: "GeneralSettings");

            migrationBuilder.RenameColumn(
                name: "TestimonailSlideId",
                table: "GeneralSettings",
                newName: "TestimonialSlideId");

            migrationBuilder.RenameIndex(
                name: "IX_GeneralSettings_TestimonailSlideId",
                table: "GeneralSettings",
                newName: "IX_GeneralSettings_TestimonialSlideId");

            migrationBuilder.AddColumn<string>(
                name: "FacebookURL",
                table: "GeneralSettings",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "InstagramURL",
                table: "GeneralSettings",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "LinkedInURL",
                table: "GeneralSettings",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_GeneralSettings_Slide_TestimonialSlideId",
                table: "GeneralSettings",
                column: "TestimonialSlideId",
                principalTable: "Slide",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GeneralSettings_Slide_TestimonialSlideId",
                table: "GeneralSettings");

            migrationBuilder.DropColumn(
                name: "FacebookURL",
                table: "GeneralSettings");

            migrationBuilder.DropColumn(
                name: "InstagramURL",
                table: "GeneralSettings");

            migrationBuilder.DropColumn(
                name: "LinkedInURL",
                table: "GeneralSettings");

            migrationBuilder.RenameColumn(
                name: "TestimonialSlideId",
                table: "GeneralSettings",
                newName: "TestimonailSlideId");

            migrationBuilder.RenameIndex(
                name: "IX_GeneralSettings_TestimonialSlideId",
                table: "GeneralSettings",
                newName: "IX_GeneralSettings_TestimonailSlideId");

            migrationBuilder.AddForeignKey(
                name: "FK_GeneralSettings_Slide_TestimonailSlideId",
                table: "GeneralSettings",
                column: "TestimonailSlideId",
                principalTable: "Slide",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
