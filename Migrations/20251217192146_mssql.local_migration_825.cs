using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WatchTogether3.Migrations
{
    /// <inheritdoc />
    public partial class mssqllocal_migration_825 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_AspNetUsers_ApplicationUserId",
                table: "Rooms");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_AspNetUsers_OwnerId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_ApplicationUserId",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Rooms");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "Rooms",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_AspNetUsers_OwnerId",
                table: "Rooms",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_AspNetUsers_OwnerId",
                table: "Rooms");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "Rooms",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_AspNetUsers_OwnerId",
                table: "Rooms",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
