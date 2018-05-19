using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ContainerService.Infrastructure.Migrations
{
    public partial class Updated_container_model : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContainerZone",
                table: "Containers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContainerZone",
                table: "Containers",
                nullable: false,
                defaultValue: 0);
        }
    }
}
