using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CourseTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Courses_Course_Id",
                table: "Documents");

            migrationBuilder.DropForeignKey(
                name: "FK_Videos_Courses_Course_Id",
                table: "Videos");

            migrationBuilder.DropIndex(
                name: "IX_Videos_Course_Id",
                table: "Videos");

            migrationBuilder.DropIndex(
                name: "IX_Documents_Course_Id",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "Course_Id",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "Course_Id",
                table: "Documents");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Course_Id",
                table: "Videos",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Course_Id",
                table: "Documents",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Videos_Course_Id",
                table: "Videos",
                column: "Course_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_Course_Id",
                table: "Documents",
                column: "Course_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Courses_Course_Id",
                table: "Documents",
                column: "Course_Id",
                principalTable: "Courses",
                principalColumn: "Course_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_Courses_Course_Id",
                table: "Videos",
                column: "Course_Id",
                principalTable: "Courses",
                principalColumn: "Course_Id");
        }
    }
}
