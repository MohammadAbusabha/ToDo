using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDo.Migrations
{
    /// <inheritdoc />
    public partial class TEST3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RolePermissions");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "DataTable",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "DataTable",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Privilege",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Privilege", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RolePrivilege",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrivilegeId = table.Column<int>(type: "int", nullable: false),
                    ApplicationRoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePrivilege", x => new { x.RoleId, x.PrivilegeId });
                    table.ForeignKey(
                        name: "FK_RolePrivilege_AspNetRoles_ApplicationRoleId",
                        column: x => x.ApplicationRoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePrivilege_Privilege_PrivilegeId",
                        column: x => x.PrivilegeId,
                        principalTable: "Privilege",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RolePrivilege_ApplicationRoleId",
                table: "RolePrivilege",
                column: "ApplicationRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePrivilege_PrivilegeId",
                table: "RolePrivilege",
                column: "PrivilegeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RolePrivilege");

            migrationBuilder.DropTable(
                name: "Privilege");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "DataTable",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "DataTable",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RolePermissions",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PermissionId = table.Column<int>(type: "int", nullable: false),
                    ApplicationRoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    permissionsId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissions", x => new { x.RoleId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_RolePermissions_AspNetRoles_ApplicationRoleId",
                        column: x => x.ApplicationRoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RolePermissions_Permissions_permissionsId",
                        column: x => x.permissionsId,
                        principalTable: "Permissions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_ApplicationRoleId",
                table: "RolePermissions",
                column: "ApplicationRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_permissionsId",
                table: "RolePermissions",
                column: "permissionsId");
        }
    }
}
