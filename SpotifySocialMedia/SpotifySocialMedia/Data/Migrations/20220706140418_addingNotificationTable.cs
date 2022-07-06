using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpotifySocialMedia.Data.Migrations
{
    public partial class addingNotificationTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Communicat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SongId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AddedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notification_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notification_Songs_SongId",
                        column: x => x.SongId,
                        principalTable: "Songs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8931ce67-348b-48b6-96fc-6fc47a74311e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "19dbfb1e-aa4a-4705-80b1-f03a6f64e841", "AQAAAAEAACcQAAAAEPz0fxsSSWd7IxtSyEtDhEVXQyBL+C222h30gqnfGO3xaNVfgn1SnKRnomJqiNT/8g==", "93134723-36ff-4473-90f2-770e6ecdabc5" });

            migrationBuilder.CreateIndex(
                name: "IX_Notification_SongId",
                table: "Notification",
                column: "SongId");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_UserId",
                table: "Notification",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8931ce67-348b-48b6-96fc-6fc47a74311e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9c4aa49f-b956-4f0c-93ec-bc266af21954", "AQAAAAEAACcQAAAAEOVo2vCpgz4mcwxgx2ssLio30Tvn83PAhcHopH5emT+SBHXBv2r0lD0Tj6dis5CIsw==", "16b8ff71-f95f-4ee5-936f-e47de7f8794d" });
        }
    }
}
