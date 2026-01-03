using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Moodle.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ProfessorId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Courses_Users_ProfessorId",
                        column: x => x.ProfessorId,
                        principalSchema: "public",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SenderId = table.Column<int>(type: "integer", nullable: false),
                    ReceiverId = table.Column<int>(type: "integer", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    SentAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_Users_ReceiverId",
                        column: x => x.ReceiverId,
                        principalSchema: "public",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Messages_Users_SenderId",
                        column: x => x.SenderId,
                        principalSchema: "public",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Announcements",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CourseId = table.Column<int>(type: "integer", nullable: false),
                    ProfessorId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Announcements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Announcements_Courses_CourseId",
                        column: x => x.CourseId,
                        principalSchema: "public",
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Announcements_Users_ProfessorId",
                        column: x => x.ProfessorId,
                        principalSchema: "public",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Enrollments",
                schema: "public",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    CourseId = table.Column<int>(type: "integer", nullable: false),
                    EnrolledAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrollments", x => new { x.UserId, x.CourseId });
                    table.ForeignKey(
                        name: "FK_Enrollments_Courses_CourseId",
                        column: x => x.CourseId,
                        principalSchema: "public",
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Enrollments_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "public",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Materials",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Url = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CourseId = table.Column<int>(type: "integer", nullable: false),
                    ProfessorId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Materials_Courses_CourseId",
                        column: x => x.CourseId,
                        principalSchema: "public",
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Materials_Users_ProfessorId",
                        column: x => x.ProfessorId,
                        principalSchema: "public",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                schema: "public",
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "Password", "Role" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 12, 2, 12, 0, 0, 0, DateTimeKind.Utc), "admin@moodle.com", "admin123", 2 },
                    { 2, new DateTime(2025, 12, 4, 12, 0, 0, 0, DateTimeKind.Utc), "prof1@moodle.com", "prof123", 1 },
                    { 3, new DateTime(2025, 12, 4, 12, 0, 0, 0, DateTimeKind.Utc), "lanavukadin@moodle.com", "prof123", 1 },
                    { 4, new DateTime(2025, 12, 7, 12, 0, 0, 0, DateTimeKind.Utc), "student1@moodle.com", "student123", 0 },
                    { 5, new DateTime(2025, 12, 8, 12, 0, 0, 0, DateTimeKind.Utc), "student2@moodle.com", "student123", 0 },
                    { 6, new DateTime(2025, 12, 9, 12, 0, 0, 0, DateTimeKind.Utc), "student3@moodle.com", "student123", 0 },
                    { 7, new DateTime(2025, 12, 10, 12, 0, 0, 0, DateTimeKind.Utc), "student4@moodle.com", "student123", 0 },
                    { 8, new DateTime(2026, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), "student5@moodle.com", "student123", 0 },
                    { 9, new DateTime(2025, 12, 22, 12, 0, 0, 0, DateTimeKind.Utc), "student6@moodle.com", "student123", 0 },
                    { 10, new DateTime(2026, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), "prof3@moodle.com", "prof123", 1 },
                    { 11, new DateTime(2025, 12, 31, 12, 0, 0, 0, DateTimeKind.Utc), "student7@moodle.com", "student123", 0 },
                    { 12, new DateTime(2025, 12, 30, 12, 0, 0, 0, DateTimeKind.Utc), "student8@moodle.com", "student123", 0 },
                    { 13, new DateTime(2025, 12, 27, 12, 0, 0, 0, DateTimeKind.Utc), "prof4@moodle.com", "prof123", 1 }
                });

            migrationBuilder.InsertData(
                schema: "public",
                table: "Courses",
                columns: new[] { "Id", "CreatedAt", "Name", "ProfessorId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 12, 4, 12, 0, 0, 0, DateTimeKind.Utc), "Programiranje 1", 2 },
                    { 2, new DateTime(2025, 12, 5, 12, 0, 0, 0, DateTimeKind.Utc), "Objektno programiranje", 2 },
                    { 3, new DateTime(2025, 12, 6, 12, 0, 0, 0, DateTimeKind.Utc), "Baze podataka", 3 },
                    { 4, new DateTime(2025, 12, 7, 12, 0, 0, 0, DateTimeKind.Utc), "Web programiranje", 3 },
                    { 5, new DateTime(2026, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), "Napredno C#", 10 },
                    { 6, new DateTime(2025, 12, 27, 12, 0, 0, 0, DateTimeKind.Utc), "Data Science", 13 },
                    { 7, new DateTime(2025, 12, 29, 12, 0, 0, 0, DateTimeKind.Utc), "Machine Learning", 13 }
                });

            migrationBuilder.InsertData(
                schema: "public",
                table: "Messages",
                columns: new[] { "Id", "Content", "ReceiverId", "SenderId", "SentAt" },
                values: new object[,]
                {
                    { 1, "Dobrodošao na kolegij!", 4, 2, new DateTime(2025, 12, 7, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, "Hvala!", 2, 4, new DateTime(2025, 12, 7, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 3, "Jesi li riješio zadatak?", 6, 5, new DateTime(2025, 12, 8, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 4, "Molim te pošalji zadaću.", 7, 3, new DateTime(2025, 12, 10, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 5, "Poslao sam svoj projekt.", 3, 8, new DateTime(2026, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 6, "Pozdrav profesore!", 10, 9, new DateTime(2025, 12, 22, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 7, "Dobrodošao!", 9, 10, new DateTime(2025, 12, 22, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 8, "Pozdrav profesore!", 13, 11, new DateTime(2025, 12, 31, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 9, "Pošaljem zadatak danas.", 13, 12, new DateTime(2025, 12, 30, 12, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                schema: "public",
                table: "Announcements",
                columns: new[] { "Id", "Content", "CourseId", "CreatedAt", "ProfessorId", "Title" },
                values: new object[,]
                {
                    { 1, "Dobrodošli na Programiranje 1!", 1, new DateTime(2025, 12, 7, 12, 0, 0, 0, DateTimeKind.Utc), 2, "Dobrodošli" },
                    { 2, "Prvi kolokvij održat će se sljedeći tjedan.", 2, new DateTime(2025, 12, 8, 12, 0, 0, 0, DateTimeKind.Utc), 2, "Prvi kolokvij" },
                    { 3, "Dodana nova literatura za kolegij.", 3, new DateTime(2025, 12, 9, 12, 0, 0, 0, DateTimeKind.Utc), 3, "Literatura" },
                    { 4, "Predavanje iz Web programiranja pomaknuto za utorak.", 4, new DateTime(2025, 12, 10, 12, 0, 0, 0, DateTimeKind.Utc), 3, "Predavanje" },
                    { 5, "Novi sadržaj za Napredno C#.", 5, new DateTime(2025, 12, 22, 12, 0, 0, 0, DateTimeKind.Utc), 10, "Napredno predavanje" },
                    { 6, "Prvo predavanje iz Data Science.", 6, new DateTime(2025, 12, 27, 12, 0, 0, 0, DateTimeKind.Utc), 13, "Data Science Intro" },
                    { 7, "Priprema za Machine Learning kolokvij.", 7, new DateTime(2025, 12, 29, 12, 0, 0, 0, DateTimeKind.Utc), 13, "ML Kolokvij" }
                });

            migrationBuilder.InsertData(
                schema: "public",
                table: "Enrollments",
                columns: new[] { "CourseId", "UserId", "EnrolledAt" },
                values: new object[,]
                {
                    { 1, 4, new DateTime(2025, 12, 7, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 3, 4, new DateTime(2026, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 1, 5, new DateTime(2025, 12, 8, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, 6, new DateTime(2025, 12, 9, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 4, 7, new DateTime(2025, 12, 10, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, 8, new DateTime(2026, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 5, 9, new DateTime(2025, 12, 22, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 6, 11, new DateTime(2025, 12, 31, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 7, 11, new DateTime(2025, 12, 31, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 6, 12, new DateTime(2025, 12, 30, 12, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                schema: "public",
                table: "Materials",
                columns: new[] { "Id", "CourseId", "CreatedAt", "Name", "ProfessorId", "Url" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 12, 7, 12, 0, 0, 0, DateTimeKind.Utc), "Uvod u C#", 2, "https://example.com/csharp" },
                    { 2, 2, new DateTime(2025, 12, 8, 12, 0, 0, 0, DateTimeKind.Utc), "OOP principi", 2, "https://example.com/oop" },
                    { 3, 3, new DateTime(2025, 12, 9, 12, 0, 0, 0, DateTimeKind.Utc), "SQL osnove", 3, "https://example.com/sql" },
                    { 4, 4, new DateTime(2025, 12, 10, 12, 0, 0, 0, DateTimeKind.Utc), "HTML & CSS", 3, "https://example.com/html-css" },
                    { 5, 2, new DateTime(2025, 12, 12, 12, 0, 0, 0, DateTimeKind.Utc), "LINQ u C#", 2, "https://example.com/linq" },
                    { 6, 5, new DateTime(2025, 12, 22, 12, 0, 0, 0, DateTimeKind.Utc), "Napredni LINQ", 10, "https://example.com/linq-advanced" },
                    { 7, 6, new DateTime(2025, 12, 27, 12, 0, 0, 0, DateTimeKind.Utc), "Python Basics", 13, "https://example.com/python" },
                    { 8, 7, new DateTime(2025, 12, 29, 12, 0, 0, 0, DateTimeKind.Utc), "ML Algorithms", 13, "https://example.com/ml" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Announcements_CourseId",
                schema: "public",
                table: "Announcements",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Announcements_ProfessorId",
                schema: "public",
                table: "Announcements",
                column: "ProfessorId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_ProfessorId",
                schema: "public",
                table: "Courses",
                column: "ProfessorId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_CourseId",
                schema: "public",
                table: "Enrollments",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Materials_CourseId",
                schema: "public",
                table: "Materials",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Materials_ProfessorId",
                schema: "public",
                table: "Materials",
                column: "ProfessorId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ReceiverId",
                schema: "public",
                table: "Messages",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_SenderId",
                schema: "public",
                table: "Messages",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                schema: "public",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Announcements",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Enrollments",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Materials",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Messages",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Courses",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "public");
        }
    }
}
