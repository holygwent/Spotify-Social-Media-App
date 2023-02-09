using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpotifySocialMedia.Data.Migrations
{
    public partial class propertyGenresFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "genres",
                table: "Artists",
                newName: "Genres");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8931ce67-348b-48b6-96fc-6fc47a74311e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f6395a47-93a5-45f8-9553-a2aec138ad03", "AQAAAAEAACcQAAAAEOPU6D1aZjihK67eXw8vhk/MXbFgtumkDFvzWxGKLll3WPGeq7iLghAYu0whJfjqRg==", "e62079db-fa5e-43c1-b1b2-f822a8c04d47" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Genres",
                table: "Artists",
                newName: "genres");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8931ce67-348b-48b6-96fc-6fc47a74311e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0c2f39a3-841d-45c0-b8a7-31d1d845d8ac", "AQAAAAEAACcQAAAAELl4qT3HDXrvA28PKFVvM82OQ1jJxC6zwTY7WiX1oM/rFuUGbhVQqbjQWBpVVSSZFA==", "f5e35ef0-5494-43b4-9e86-a8ff72c71764" });
        }
    }
}
