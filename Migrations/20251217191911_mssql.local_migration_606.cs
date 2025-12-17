using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WatchTogether3.Migrations
{
    /// <inheritdoc />
    public partial class mssqllocal_migration_606 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Rooms",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_ApplicationUserId",
                table: "Rooms",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_AspNetUsers_ApplicationUserId",
                table: "Rooms",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_AspNetUsers_ApplicationUserId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_ApplicationUserId",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Rooms");
        }
    }
}
