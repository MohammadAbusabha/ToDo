using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDo.Migrations
{
    /// <inheritdoc />
    public partial class TEST6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationRolePrivilegeRole_PrivilegeRole_PrivilegeRolesId",
                table: "ApplicationRolePrivilegeRole");

            migrationBuilder.DropForeignKey(
                name: "FK_PrivilegePrivilegeRole_PrivilegeRole_PrivilegeRoleId",
                table: "PrivilegePrivilegeRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PrivilegeRole",
                table: "PrivilegeRole");

            migrationBuilder.RenameTable(
                name: "PrivilegeRole",
                newName: "PrivilegeRoles");

            migrationBuilder.AddColumn<Guid>(
                name: "PrivilegesId",
                table: "PrivilegeRoles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "RolesId",
                table: "PrivilegeRoles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_PrivilegeRoles",
                table: "PrivilegeRoles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationRolePrivilegeRole_PrivilegeRoles_PrivilegeRolesId",
                table: "ApplicationRolePrivilegeRole",
                column: "PrivilegeRolesId",
                principalTable: "PrivilegeRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PrivilegePrivilegeRole_PrivilegeRoles_PrivilegeRoleId",
                table: "PrivilegePrivilegeRole",
                column: "PrivilegeRoleId",
                principalTable: "PrivilegeRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationRolePrivilegeRole_PrivilegeRoles_PrivilegeRolesId",
                table: "ApplicationRolePrivilegeRole");

            migrationBuilder.DropForeignKey(
                name: "FK_PrivilegePrivilegeRole_PrivilegeRoles_PrivilegeRoleId",
                table: "PrivilegePrivilegeRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PrivilegeRoles",
                table: "PrivilegeRoles");

            migrationBuilder.DropColumn(
                name: "PrivilegesId",
                table: "PrivilegeRoles");

            migrationBuilder.DropColumn(
                name: "RolesId",
                table: "PrivilegeRoles");

            migrationBuilder.RenameTable(
                name: "PrivilegeRoles",
                newName: "PrivilegeRole");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PrivilegeRole",
                table: "PrivilegeRole",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationRolePrivilegeRole_PrivilegeRole_PrivilegeRolesId",
                table: "ApplicationRolePrivilegeRole",
                column: "PrivilegeRolesId",
                principalTable: "PrivilegeRole",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PrivilegePrivilegeRole_PrivilegeRole_PrivilegeRoleId",
                table: "PrivilegePrivilegeRole",
                column: "PrivilegeRoleId",
                principalTable: "PrivilegeRole",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
