using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WatchTogether3.Migrations
{
    /// <inheritdoc />
    public partial class mssqllocal_migration_307 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Rooms_RoomName",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "RoomName",
                table: "AspNetUsers",
                newName: "CurrentRoomName");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_RoomName",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_CurrentRoomName");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Rooms_CurrentRoomName",
                table: "AspNetUsers",
                column: "CurrentRoomName",
                principalTable: "Rooms",
                principalColumn: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Rooms_CurrentRoomName",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "CurrentRoomName",
                table: "AspNetUsers",
                newName: "RoomName");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_CurrentRoomName",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_RoomName");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Rooms_RoomName",
                table: "AspNetUsers",
                column: "RoomName",
                principalTable: "Rooms",
                principalColumn: "Name");
        }
    }
}
