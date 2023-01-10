using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpotifySocialMedia.Data.Migrations
{
    public partial class commit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8931ce67-348b-48b6-96fc-6fc47a74311e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f1a44e21-087a-4981-b9e4-bd8f6466b3fb", "AQAAAAEAACcQAAAAEOQS4BirX6Jq7Bo1PJSwcSX9buB5RADazggcBQfIaIQUgld7XJUdsdTAfvI5gFOGFA==", "e3f5e7dd-82e9-49b3-bbf1-dcb6d9cbe9da" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8931ce67-348b-48b6-96fc-6fc47a74311e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fb32d2da-c4b0-4150-a7fb-2f3680b15d5f", "AQAAAAEAACcQAAAAEF/UiCZrt7UrIrUGv+1XvQDLDrcY2uKyUOUQ5absZ+b/6pfBxKxzowqv5fqDEARFuA==", "49357460-030e-451b-a21f-7d4463babcfd" });
        }
    }
}
