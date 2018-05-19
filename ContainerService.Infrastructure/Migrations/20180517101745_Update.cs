using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ContainerService.Infrastructure.Migrations
{
    public partial class Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ContainerId",
                table: "Trucks",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ContainerZone",
                table: "Containers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Trucks_ContainerId",
                table: "Trucks",
                column: "ContainerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trucks_Containers_ContainerId",
                table: "Trucks",
                column: "ContainerId",
                principalTable: "Containers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trucks_Containers_ContainerId",
                table: "Trucks");

            migrationBuilder.DropIndex(
                name: "IX_Trucks_ContainerId",
                table: "Trucks");

            migrationBuilder.DropColumn(
                name: "ContainerId",
                table: "Trucks");

            migrationBuilder.DropColumn(
                name: "ContainerZone",
                table: "Containers");
        }
    }
}
