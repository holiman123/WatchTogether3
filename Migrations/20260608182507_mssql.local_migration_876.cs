using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WatchTogether3.Migrations
{
    /// <inheritdoc />
    public partial class mssqllocal_migration_876 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrentRoomId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CurrentRoomId",
                table: "AspNetUsers",
                column: "CurrentRoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Rooms_CurrentRoomId",
                table: "AspNetUsers",
                column: "CurrentRoomId",
                principalTable: "Rooms",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Rooms_CurrentRoomId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CurrentRoomId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CurrentRoomId",
                table: "AspNetUsers");
        }
    }
}
