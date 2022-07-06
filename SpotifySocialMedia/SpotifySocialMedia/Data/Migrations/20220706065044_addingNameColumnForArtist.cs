using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpotifySocialMedia.Data.Migrations
{
    public partial class addingNameColumnForArtist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Artists",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8931ce67-348b-48b6-96fc-6fc47a74311e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5d465562-6ffa-4e4d-9ff8-3809eee2201d", "AQAAAAEAACcQAAAAELgp+8F5uATnkOJURYvT6GpiNLbUUfxRE0+E73aDr+FnQ75WXkK+zbFAxlI6/LLMdA==", "ed90e8c0-951d-4baa-be9c-5a45b5669b26" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Artists");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8931ce67-348b-48b6-96fc-6fc47a74311e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "603e9fa9-42d3-41d2-9b0c-fb9e40bdf6f3", "AQAAAAEAACcQAAAAECqKuJ3HQO6AcqDARg6Ygo6Z55YtOUbjXsRmYsMh662oob/lFwJ9kIeaSAieTveZYw==", "63797db5-1341-4141-be48-5feeab33f65e" });
        }
    }
}
