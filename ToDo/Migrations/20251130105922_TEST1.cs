using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDo.Migrations
{
    /// <inheritdoc />
    public partial class TEST1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Userid",
                table: "toDos",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_toDos_Userid",
                table: "toDos",
                column: "Userid",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_toDos_AspNetUsers_Userid",
                table: "toDos",
                column: "Userid",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_toDos_AspNetUsers_Userid",
                table: "toDos");

            migrationBuilder.DropIndex(
                name: "IX_toDos_Userid",
                table: "toDos");

            migrationBuilder.DropColumn(
                name: "Userid",
                table: "toDos");
        }
    }
}
