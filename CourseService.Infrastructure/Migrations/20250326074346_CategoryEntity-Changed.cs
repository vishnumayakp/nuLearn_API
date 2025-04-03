using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CategoryEntityChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Courses_Category_Id",
                table: "Courses",
                column: "Category_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Categories_Category_Id",
                table: "Courses",
                column: "Category_Id",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Categories_Category_Id",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_Category_Id",
                table: "Courses");
        }
    }
}
