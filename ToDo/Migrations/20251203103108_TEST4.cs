using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDo.Migrations
{
    /// <inheritdoc />
    public partial class TEST4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_toDos_AspNetUsers_Userid",
                table: "toDos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_toDos",
                table: "toDos");

            migrationBuilder.DropIndex(
                name: "IX_toDos_Userid",
                table: "toDos");

            migrationBuilder.RenameTable(
                name: "toDos",
                newName: "ToDos");

            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationUserId",
                table: "ToDos",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ToDos",
                table: "ToDos",
                column: "Id");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ToDos_AspNetUsers_ApplicationUserId",
                table: "ToDos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ToDos",
                table: "ToDos");

            migrationBuilder.DropIndex(
                name: "IX_ToDos_ApplicationUserId",
                table: "ToDos");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "ToDos");

            migrationBuilder.RenameTable(
                name: "ToDos",
                newName: "toDos");

            migrationBuilder.AddPrimaryKey(
                name: "PK_toDos",
                table: "toDos",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_toDos_Userid",
                table: "toDos",
                column: "Userid");

            migrationBuilder.AddForeignKey(
                name: "FK_toDos_AspNetUsers_Userid",
                table: "toDos",
                column: "Userid",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
