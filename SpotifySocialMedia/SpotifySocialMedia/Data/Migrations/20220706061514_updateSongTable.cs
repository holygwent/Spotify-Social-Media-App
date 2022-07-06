using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpotifySocialMedia.Data.Migrations
{
    public partial class updateSongTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Songs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8931ce67-348b-48b6-96fc-6fc47a74311e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "603e9fa9-42d3-41d2-9b0c-fb9e40bdf6f3", "AQAAAAEAACcQAAAAECqKuJ3HQO6AcqDARg6Ygo6Z55YtOUbjXsRmYsMh662oob/lFwJ9kIeaSAieTveZYw==", "63797db5-1341-4141-be48-5feeab33f65e" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Songs");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8931ce67-348b-48b6-96fc-6fc47a74311e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "01d446f6-2e93-4e9f-befe-af3cf0d54ae1", "AQAAAAEAACcQAAAAEN34CACcIdn1xT+L1FVPk/hpOwkBsx9ISAJy3BRzKPnDfpkZ0CQeIOt6EkCeVaQAIw==", "8070b73b-3c2b-418b-8d1a-434fa1b975d5" });
        }
    }
}
