using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BeTaskManagement.Migrations
{
    /// <inheritdoc />
    public partial class NewDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "BeTasks",
                columns: new[] { "BeTaskId", "AssignedToUserId", "CreatedOn", "Description", "DueDate", "Name", "NextActionDate", "Status", "Type" },
                values: new object[] { 1, null, new DateTime(2025, 7, 10, 14, 30, 0, 0, DateTimeKind.Unspecified), "This is just a mockup task.", new DateTime(2025, 7, 14, 14, 30, 0, 0, DateTimeKind.Unspecified), "Mockup Task", new DateTime(2025, 7, 15, 14, 30, 0, 0, DateTimeKind.Unspecified), "ToDo", "Feature" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "EmailAddress", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, "johndoe@test.test", "John", "Doe" },
                    { 2, "mm@test.test", "Maria", "Marinova" },
                    { 3, "lila@test.test", "Lilly", "Alan" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BeTasks",
                keyColumn: "BeTaskId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 3);
        }
    }
}
