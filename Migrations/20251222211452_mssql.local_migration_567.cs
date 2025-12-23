using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WatchTogether3.Migrations
{
    /// <inheritdoc />
    public partial class mssqllocal_migration_567 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "Rooms",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<double>(
                name: "CurrentTime",
                table: "Rooms",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<bool>(
                name: "IsPlaying",
                table: "Rooms",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastPlayTime",
                table: "Rooms",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "CurrentTime",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "IsPlaying",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "LastPlayTime",
                table: "Rooms");
        }
    }
}
