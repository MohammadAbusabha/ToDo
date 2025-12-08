using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDo.Migrations
{
    /// <inheritdoc />
    public partial class TEST2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_IdentityRole_RoleId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "AspNetUsers",
                newName: "Name");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_RoleId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_Name");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_IdentityRole_Name",
                table: "AspNetUsers",
                column: "Name",
                principalTable: "IdentityRole",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_IdentityRole_Name",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "AspNetUsers",
                newName: "RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_Name",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_IdentityRole_RoleId",
                table: "AspNetUsers",
                column: "RoleId",
                principalTable: "IdentityRole",
                principalColumn: "Id");
        }
    }
}
