using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpotifySocialMedia.Data.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8931ce67-348b-48b6-96fc-6fc47a74311e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a583af8e-15e4-4a8a-b949-307109d4a80a", "AQAAAAEAACcQAAAAEOGlKSSTR85cBr5pxJ0LSRQ2+tSrtEhi//ZtlLsBNlIfrH8jdSUNQF1AzAFiT1Jbog==", "ff6dd925-ca8d-4eb1-9222-51474975e9d3" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8931ce67-348b-48b6-96fc-6fc47a74311e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f1a44e21-087a-4981-b9e4-bd8f6466b3fb", "AQAAAAEAACcQAAAAEOQS4BirX6Jq7Bo1PJSwcSX9buB5RADazggcBQfIaIQUgld7XJUdsdTAfvI5gFOGFA==", "e3f5e7dd-82e9-49b3-bbf1-dcb6d9cbe9da" });
        }
    }
}
