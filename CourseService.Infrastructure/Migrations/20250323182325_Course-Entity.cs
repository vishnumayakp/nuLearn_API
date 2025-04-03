using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CourseEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Course_Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Instructor_Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Category_Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SubCategory_Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Course_Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Image = table.Column<string>(type: "text", nullable: false),
                    MRP = table.Column<decimal>(type: "numeric", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    Is_Bolcked = table.Column<bool>(type: "boolean", nullable: false),
                    Video = table.Column<string>(type: "text", nullable: false),
                    Document = table.Column<string>(type: "text", nullable: false),
                    Created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Updated_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Created_by = table.Column<string>(type: "text", nullable: false),
                    Updated_by = table.Column<string>(type: "text", nullable: true),
                    Deleted_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Deleted_by = table.Column<string>(type: "text", nullable: true),
                    Is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Course_Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Courses");
        }
    }
}
