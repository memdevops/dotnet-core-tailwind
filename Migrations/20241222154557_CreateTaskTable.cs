using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MvcProject.web.Migrations
{
    /// <inheritdoc />
    public partial class CreateTaskTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Priority = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CommentsCount = table.Column<int>(type: "int", nullable: false),
                    Tags = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "CommentsCount", "DueDate", "Priority", "Tags", "Title" },
                values: new object[,]
                {
                    { 1, 12, new DateTime(2024, 1, 15, 18, 0, 0, 0, DateTimeKind.Unspecified), "High", "[\"Marketing\",\"Presentation\"]", "Prepare Marketing Presentation" },
                    { 2, 8, new DateTime(2024, 1, 10, 12, 0, 0, 0, DateTimeKind.Unspecified), "Urgent", "[\"Finance\",\"Report\"]", "Submit Financial Report" },
                    { 3, 4, new DateTime(2024, 1, 18, 14, 0, 0, 0, DateTimeKind.Unspecified), "Medium", "[\"Meeting\",\"Team\"]", "Team Meeting Preparation" },
                    { 4, 6, new DateTime(2024, 1, 22, 17, 0, 0, 0, DateTimeKind.Unspecified), "Low", "[\"Software\",\"Documentation\"]", "Update Software Documentation" },
                    { 5, 3, new DateTime(2024, 1, 12, 10, 0, 0, 0, DateTimeKind.Unspecified), "High", "[\"Client\",\"Follow-up\"]", "Client Follow-up Calls" },
                    { 6, 0, new DateTime(2024, 1, 25, 15, 0, 0, 0, DateTimeKind.Unspecified), "Low", "[\"Organization\",\"Workspace\"]", "Organize Workspace" },
                    { 7, 15, new DateTime(2024, 1, 5, 11, 0, 0, 0, DateTimeKind.Unspecified), "Urgent", "[\"Product\",\"Launch\"]", "Product Launch Planning" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");
        }
    }
}
