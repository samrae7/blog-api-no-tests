using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogApi.Migrations
{
    public partial class seedposts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Body", "DateCreated", "Title" },
                values: new object[] { 1L, "Much ado about nothing", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "William Shakespeare" });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Body", "DateCreated", "Title" },
                values: new object[] { 2L, "Pride and Prejudice", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jane Austen" });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Body", "DateCreated", "Title" },
                values: new object[] { 3L, "Decline anf Fall", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Evelyn Waugh" });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Body", "DateCreated", "Title" },
                values: new object[] { 4L, "Middlemarch", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "George Eliot" });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Body", "DateCreated", "Title" },
                values: new object[] { 5L, "Great Expectations", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Charles Dickens" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 5L);
        }
    }
}
