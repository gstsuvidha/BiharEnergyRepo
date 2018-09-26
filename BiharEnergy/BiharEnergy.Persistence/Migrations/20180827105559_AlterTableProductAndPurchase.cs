using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BiharEnergy.Persistence.Migrations
{
    public partial class AlterTableProductAndPurchase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItcEligibility",
                table: "PurchaseInvoiceItems");

            migrationBuilder.DropColumn(
                name: "ItcPercentage",
                table: "PurchaseInvoiceItems");

            migrationBuilder.AddColumn<string>(
                name: "ItcEligibility",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "ItcPercentage",
                table: "Products",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItcEligibility",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ItcPercentage",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "ItcEligibility",
                table: "PurchaseInvoiceItems",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "ItcPercentage",
                table: "PurchaseInvoiceItems",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
