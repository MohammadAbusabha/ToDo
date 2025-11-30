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
            migrationBuilder.DropIndex(
                name: "IX_toDos_Userid",
                table: "toDos");

            migrationBuilder.CreateIndex(
                name: "IX_toDos_Userid",
                table: "toDos",
                column: "Userid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_toDos_Userid",
                table: "toDos");

            migrationBuilder.CreateIndex(
                name: "IX_toDos_Userid",
                table: "toDos",
                column: "Userid",
                unique: true);
        }
    }
}
