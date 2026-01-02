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
                    Role = table.Column<int>(type: "integer", nullable: false)
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
                columns: new[] { "Id", "Email", "Password", "Role" },
                values: new object[,]
                {
                    { 1, "admin@moodle.com", "admin123", 2 },
                    { 2, "lanavukadin@moodle.com", "prof123", 1 },
                    { 3, "prof2@moodle.com", "prof123", 1 },
                    { 4, "student1@moodle.com", "student123", 0 },
                    { 5, "student2@moodle.com", "student123", 0 },
                    { 6, "student3@moodle.com", "student123", 0 }
                });

            migrationBuilder.InsertData(
                schema: "public",
                table: "Courses",
                columns: new[] { "Id", "Name", "ProfessorId" },
                values: new object[,]
                {
                    { 1, "Programiranje 1", 2 },
                    { 2, "Objektno programiranje", 2 },
                    { 3, "Baze podataka", 3 }
                });

            migrationBuilder.InsertData(
                schema: "public",
                table: "Messages",
                columns: new[] { "Id", "Content", "ReceiverId", "SenderId", "SentAt" },
                values: new object[,]
                {
                    { 1, "Dobrodošao na kolegij!", 4, 2, new DateTime(2026, 1, 2, 19, 6, 14, 723, DateTimeKind.Utc).AddTicks(2644) },
                    { 2, "Hvala!", 2, 4, new DateTime(2026, 1, 2, 19, 6, 14, 723, DateTimeKind.Utc).AddTicks(2646) },
                    { 3, "Jesi li riješio zadatak?", 6, 5, new DateTime(2026, 1, 2, 19, 6, 14, 723, DateTimeKind.Utc).AddTicks(2648) }
                });

            migrationBuilder.InsertData(
                schema: "public",
                table: "Announcements",
                columns: new[] { "Id", "Content", "CourseId", "CreatedAt", "ProfessorId", "Title" },
                values: new object[,]
                {
                    { 1, "Dobrodošli na Programiranje 1!", 1, new DateTime(2026, 1, 2, 19, 6, 14, 723, DateTimeKind.Utc).AddTicks(2590), 2, "Dobrodošli" },
                    { 2, "Prvi kolokvij održat će se sljedeći tjedan.", 2, new DateTime(2026, 1, 2, 19, 6, 14, 723, DateTimeKind.Utc).AddTicks(2592), 2, "Prvi kolokvij" },
                    { 3, "Dodana je nova literatura.", 3, new DateTime(2026, 1, 2, 19, 6, 14, 723, DateTimeKind.Utc).AddTicks(2594), 3, "Literatura" }
                });

            migrationBuilder.InsertData(
                schema: "public",
                table: "Enrollments",
                columns: new[] { "CourseId", "UserId", "EnrolledAt" },
                values: new object[,]
                {
                    { 1, 4, new DateTime(2026, 1, 2, 19, 6, 14, 723, DateTimeKind.Utc).AddTicks(2564) },
                    { 3, 4, new DateTime(2026, 1, 2, 19, 6, 14, 723, DateTimeKind.Utc).AddTicks(2569) },
                    { 1, 5, new DateTime(2026, 1, 2, 19, 6, 14, 723, DateTimeKind.Utc).AddTicks(2566) },
                    { 2, 6, new DateTime(2026, 1, 2, 19, 6, 14, 723, DateTimeKind.Utc).AddTicks(2568) }
                });

            migrationBuilder.InsertData(
                schema: "public",
                table: "Materials",
                columns: new[] { "Id", "CourseId", "CreatedAt", "Name", "ProfessorId", "Url" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2026, 1, 2, 19, 6, 14, 723, DateTimeKind.Utc).AddTicks(2610), "Uvod u C#", 2, "https://example.com/csharp" },
                    { 2, 2, new DateTime(2026, 1, 2, 19, 6, 14, 723, DateTimeKind.Utc).AddTicks(2612), "OOP principi", 2, "https://example.com/oop" },
                    { 3, 3, new DateTime(2026, 1, 2, 19, 6, 14, 723, DateTimeKind.Utc).AddTicks(2614), "SQL osnove", 3, "https://example.com/sql" }
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
