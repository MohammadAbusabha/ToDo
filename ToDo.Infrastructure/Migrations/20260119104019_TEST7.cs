using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDo.Migrations
{
    /// <inheritdoc />
    public partial class TEST7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_Projects_ProjectId1",
                table: "Devices");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Organizations_OrganizationId1",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_OrganizationId1",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Devices_ProjectId1",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "OrganizationId1",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ProjectId1",
                table: "Devices");

            migrationBuilder.AlterColumn<int>(
                name: "OrganizationId",
                table: "Projects",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "DeviceId",
                table: "Projects",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "Organizations",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "Devices",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_OrganizationId",
                table: "Projects",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_ProjectId",
                table: "Devices",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_Projects_ProjectId",
                table: "Devices",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Organizations_OrganizationId",
                table: "Projects",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_Projects_ProjectId",
                table: "Devices");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Organizations_OrganizationId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_OrganizationId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Devices_ProjectId",
                table: "Devices");

            migrationBuilder.AlterColumn<int>(
                name: "OrganizationId",
                table: "Projects",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "DeviceId",
                table: "Projects",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId1",
                table: "Projects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "Organizations",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "Devices",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ProjectId1",
                table: "Devices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_OrganizationId1",
                table: "Projects",
                column: "OrganizationId1");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_ProjectId1",
                table: "Devices",
                column: "ProjectId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_Projects_ProjectId1",
                table: "Devices",
                column: "ProjectId1",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Organizations_OrganizationId1",
                table: "Projects",
                column: "OrganizationId1",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
