using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BiharEnergy.Persistence.Migrations
{
    public partial class AddTableTds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tdss",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountingUnitId = table.Column<string>(nullable: true),
                    AmountPaid = table.Column<double>(nullable: false),
                    CgstAmount = table.Column<double>(nullable: false),
                    IgstAmount = table.Column<double>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    PlaceOfSupply = table.Column<string>(nullable: true),
                    SgstAmount = table.Column<double>(nullable: false),
                    SupplierId = table.Column<int>(nullable: false),
                    TdsAmount = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tdss", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tdss_AccountingUnits_AccountingUnitId",
                        column: x => x.AccountingUnitId,
                        principalTable: "AccountingUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tdss_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tdss_AccountingUnitId",
                table: "Tdss",
                column: "AccountingUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Tdss_SupplierId",
                table: "Tdss",
                column: "SupplierId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tdss");
        }
    }
}
