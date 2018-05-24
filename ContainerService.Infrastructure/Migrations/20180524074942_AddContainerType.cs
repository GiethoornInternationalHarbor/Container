using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ContainerService.Infrastructure.Migrations
{
    public partial class AddContainerType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ships_ShipService_ShipServiceId",
                table: "Ships");

            migrationBuilder.DropTable(
                name: "ShipService");

            migrationBuilder.DropIndex(
                name: "IX_Ships_ShipServiceId",
                table: "Ships");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Ships");

            migrationBuilder.DropColumn(
                name: "ShipServiceId",
                table: "Ships");

            migrationBuilder.DropColumn(
                name: "ProductType",
                table: "Containers");

            migrationBuilder.AddColumn<Guid>(
                name: "CustomerId",
                table: "Ships",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "ContainerType",
                table: "Containers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Ships");

            migrationBuilder.DropColumn(
                name: "ContainerType",
                table: "Containers");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Ships",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ShipServiceId",
                table: "Ships",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductType",
                table: "Containers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ShipService",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipService", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ships_ShipServiceId",
                table: "Ships",
                column: "ShipServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ships_ShipService_ShipServiceId",
                table: "Ships",
                column: "ShipServiceId",
                principalTable: "ShipService",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
