using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpotifySocialMedia.Data.Migrations
{
    public partial class dbCheck : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8931ce67-348b-48b6-96fc-6fc47a74311e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0c2f39a3-841d-45c0-b8a7-31d1d845d8ac", "AQAAAAEAACcQAAAAELl4qT3HDXrvA28PKFVvM82OQ1jJxC6zwTY7WiX1oM/rFuUGbhVQqbjQWBpVVSSZFA==", "f5e35ef0-5494-43b4-9e86-a8ff72c71764" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8931ce67-348b-48b6-96fc-6fc47a74311e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a583af8e-15e4-4a8a-b949-307109d4a80a", "AQAAAAEAACcQAAAAEOGlKSSTR85cBr5pxJ0LSRQ2+tSrtEhi//ZtlLsBNlIfrH8jdSUNQF1AzAFiT1Jbog==", "ff6dd925-ca8d-4eb1-9222-51474975e9d3" });
        }
    }
}
