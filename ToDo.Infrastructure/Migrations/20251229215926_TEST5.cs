using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDo.Migrations
{
    /// <inheritdoc />
    public partial class TEST5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Value",
                table: "AspNetRoles");

            migrationBuilder.CreateTable(
                name: "PrivilegeRole",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrivilegeRole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PrivilegeTable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrivilegeTable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationRolePrivilegeRole",
                columns: table => new
                {
                    PrivilegeRolesId = table.Column<int>(type: "int", nullable: false),
                    RolesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationRolePrivilegeRole", x => new { x.PrivilegeRolesId, x.RolesId });
                    table.ForeignKey(
                        name: "FK_ApplicationRolePrivilegeRole_AspNetRoles_RolesId",
                        column: x => x.RolesId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationRolePrivilegeRole_PrivilegeRole_PrivilegeRolesId",
                        column: x => x.PrivilegeRolesId,
                        principalTable: "PrivilegeRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrivilegePrivilegeRole",
                columns: table => new
                {
                    PrivilegeRoleId = table.Column<int>(type: "int", nullable: false),
                    PrivilegesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrivilegePrivilegeRole", x => new { x.PrivilegeRoleId, x.PrivilegesId });
                    table.ForeignKey(
                        name: "FK_PrivilegePrivilegeRole_PrivilegeRole_PrivilegeRoleId",
                        column: x => x.PrivilegeRoleId,
                        principalTable: "PrivilegeRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrivilegePrivilegeRole_PrivilegeTable_PrivilegesId",
                        column: x => x.PrivilegesId,
                        principalTable: "PrivilegeTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationRolePrivilegeRole_RolesId",
                table: "ApplicationRolePrivilegeRole",
                column: "RolesId");

            migrationBuilder.CreateIndex(
                name: "IX_PrivilegePrivilegeRole_PrivilegesId",
                table: "PrivilegePrivilegeRole",
                column: "PrivilegesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationRolePrivilegeRole");

            migrationBuilder.DropTable(
                name: "PrivilegePrivilegeRole");

            migrationBuilder.DropTable(
                name: "PrivilegeRole");

            migrationBuilder.DropTable(
                name: "PrivilegeTable");

            migrationBuilder.AddColumn<int>(
                name: "Value",
                table: "AspNetRoles",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
