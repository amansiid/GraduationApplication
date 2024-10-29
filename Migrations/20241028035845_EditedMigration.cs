using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GraduationApplication.Migrations
{
    /// <inheritdoc />
    public partial class EditedMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppliedYear",
                table: "GraduationApplications");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "GraduationApplications");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "GraduationApplications");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AppliedYear",
                table: "GraduationApplications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "GraduationApplications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "GraduationApplications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
