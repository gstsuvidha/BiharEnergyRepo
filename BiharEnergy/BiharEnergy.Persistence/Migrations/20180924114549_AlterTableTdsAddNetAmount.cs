using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BiharEnergy.Persistence.Migrations
{
    public partial class AlterTableTdsAddNetAmount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "NetAmount",
                table: "Tdss",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "TdsGstin",
                table: "AccountingUnits",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NetAmount",
                table: "Tdss");

            migrationBuilder.DropColumn(
                name: "TdsGstin",
                table: "AccountingUnits");
        }
    }
}
