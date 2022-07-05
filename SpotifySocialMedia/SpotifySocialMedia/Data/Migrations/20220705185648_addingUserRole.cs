using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpotifySocialMedia.Data.Migrations
{
    public partial class addingUserRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "284183d9-e14e-46be-a224-aae078fa3456", "1", "User", "USER" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8931ce67-348b-48b6-96fc-6fc47a74311e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "01d446f6-2e93-4e9f-befe-af3cf0d54ae1", "AQAAAAEAACcQAAAAEN34CACcIdn1xT+L1FVPk/hpOwkBsx9ISAJy3BRzKPnDfpkZ0CQeIOt6EkCeVaQAIw==", "8070b73b-3c2b-418b-8d1a-434fa1b975d5" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "284183d9-e14e-46be-a224-aae078fa3456");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8931ce67-348b-48b6-96fc-6fc47a74311e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "29c766f0-3d7f-491a-b6ff-e93ada648b9e", "AQAAAAEAACcQAAAAEG/us/wwnYl6DJUXfZm626kF3awWVXAVHYZUm4IkEkl/w56tQQNHEUgfoHq0qQKT9g==", "a1c88747-b10e-47b4-83f1-e67192f670bd" });
        }
    }
}
