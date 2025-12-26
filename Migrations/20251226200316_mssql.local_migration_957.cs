using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WatchTogether3.Migrations
{
    /// <inheritdoc />
    public partial class mssqllocal_migration_957 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RoomName",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_RoomName",
                table: "AspNetUsers",
                column: "RoomName");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Rooms_RoomName",
                table: "AspNetUsers",
                column: "RoomName",
                principalTable: "Rooms",
                principalColumn: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Rooms_RoomName",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_RoomName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RoomName",
                table: "AspNetUsers");
        }
    }
}
