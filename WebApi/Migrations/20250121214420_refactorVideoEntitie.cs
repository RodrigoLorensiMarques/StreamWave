using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class refactorVideoEntitie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_videos_Roles_Rolesid",
                table: "videos");

            migrationBuilder.RenameColumn(
                name: "Rolesid",
                table: "videos",
                newName: "Roleid");

            migrationBuilder.RenameIndex(
                name: "IX_videos_Rolesid",
                table: "videos",
                newName: "IX_videos_Roleid");

            migrationBuilder.AddForeignKey(
                name: "FK_videos_Roles_Roleid",
                table: "videos",
                column: "Roleid",
                principalTable: "Roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_videos_Roles_Roleid",
                table: "videos");

            migrationBuilder.RenameColumn(
                name: "Roleid",
                table: "videos",
                newName: "Rolesid");

            migrationBuilder.RenameIndex(
                name: "IX_videos_Roleid",
                table: "videos",
                newName: "IX_videos_Rolesid");

            migrationBuilder.AddForeignKey(
                name: "FK_videos_Roles_Rolesid",
                table: "videos",
                column: "Rolesid",
                principalTable: "Roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
