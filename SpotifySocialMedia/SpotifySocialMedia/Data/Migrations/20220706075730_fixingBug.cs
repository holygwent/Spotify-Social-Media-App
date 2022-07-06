using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpotifySocialMedia.Data.Migrations
{
    public partial class fixingBug : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Songs_ArtistId",
                table: "Songs");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8931ce67-348b-48b6-96fc-6fc47a74311e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9c4aa49f-b956-4f0c-93ec-bc266af21954", "AQAAAAEAACcQAAAAEOVo2vCpgz4mcwxgx2ssLio30Tvn83PAhcHopH5emT+SBHXBv2r0lD0Tj6dis5CIsw==", "16b8ff71-f95f-4ee5-936f-e47de7f8794d" });

            migrationBuilder.CreateIndex(
                name: "IX_Songs_ArtistId",
                table: "Songs",
                column: "ArtistId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Songs_ArtistId",
                table: "Songs");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8931ce67-348b-48b6-96fc-6fc47a74311e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5d465562-6ffa-4e4d-9ff8-3809eee2201d", "AQAAAAEAACcQAAAAELgp+8F5uATnkOJURYvT6GpiNLbUUfxRE0+E73aDr+FnQ75WXkK+zbFAxlI6/LLMdA==", "ed90e8c0-951d-4baa-be9c-5a45b5669b26" });

            migrationBuilder.CreateIndex(
                name: "IX_Songs_ArtistId",
                table: "Songs",
                column: "ArtistId",
                unique: true);
        }
    }
}
