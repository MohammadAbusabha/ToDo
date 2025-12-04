using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDo.Migrations
{
    /// <inheritdoc />
    public partial class TEST8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ToDos_AspNetUsers_ApplicationUserId",
                table: "ToDos");

            migrationBuilder.DropIndex(
                name: "IX_ToDos_ApplicationUserId",
                table: "ToDos");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "ToDos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationUserId",
                table: "ToDos",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ToDos_ApplicationUserId",
                table: "ToDos",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ToDos_AspNetUsers_ApplicationUserId",
                table: "ToDos",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
