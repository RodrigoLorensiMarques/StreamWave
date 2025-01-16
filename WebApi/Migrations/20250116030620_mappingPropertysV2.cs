using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class mappingPropertysV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Videos",
                table: "Videos");

            migrationBuilder.RenameTable(
                name: "Videos",
                newName: "videos");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "videos",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "videos",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "DateAdd",
                table: "videos",
                newName: "date_add");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "videos",
                type: "VARCHAR(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "videos",
                type: "VARCHAR(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "date_add",
                table: "videos",
                type: "DATETIME2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddPrimaryKey(
                name: "PK_videos",
                table: "videos",
                column: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_videos",
                table: "videos");

            migrationBuilder.RenameTable(
                name: "videos",
                newName: "Videos");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Videos",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Videos",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "date_add",
                table: "Videos",
                newName: "DateAdd");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Videos",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Videos",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(250)",
                oldMaxLength: 250);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateAdd",
                table: "Videos",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "DATETIME2");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Videos",
                table: "Videos",
                column: "id");
        }
    }
}
