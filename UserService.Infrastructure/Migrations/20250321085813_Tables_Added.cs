using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Tables_Added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admin",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
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
                    table.PrimaryKey("PK_Admin", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Instructors",
                columns: table => new
                {
                    Instructor_Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Tag = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: true),
                    Profile = table.Column<string>(type: "text", nullable: true),
                    Is_Blocked = table.Column<bool>(type: "boolean", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    LinkedIn_Url = table.Column<string>(type: "text", nullable: true),
                    Certificate_Url = table.Column<string>(type: "text", nullable: true),
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
                    table.PrimaryKey("PK_Instructors", x => x.Instructor_Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Profile = table.Column<string>(type: "text", nullable: true),
                    Is_blocked = table.Column<bool>(type: "boolean", nullable: false),
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
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VerifyInstructors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Certificate_Url = table.Column<string>(type: "text", nullable: false),
                    Otp = table.Column<int>(type: "integer", nullable: false),
                    Expire_time = table.Column<TimeOnly>(type: "time without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VerifyInstructors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VerifyUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Otp = table.Column<int>(type: "integer", nullable: false),
                    Expire_time = table.Column<TimeOnly>(type: "time without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VerifyUsers", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admin");

            migrationBuilder.DropTable(
                name: "Instructors");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "VerifyInstructors");

            migrationBuilder.DropTable(
                name: "VerifyUsers");
        }
    }
}
