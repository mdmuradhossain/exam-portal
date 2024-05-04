using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace exam_portal.Migrations
{
    /// <inheritdoc />
    public partial class exam5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CourseTitle",
                table: "Exams",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DepartmentName",
                table: "Exams",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourseTitle",
                table: "Exams");

            migrationBuilder.DropColumn(
                name: "DepartmentName",
                table: "Exams");
        }
    }
}
