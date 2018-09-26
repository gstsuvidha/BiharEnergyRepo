using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BiharEnergy.Persistence.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SalesInvoiceLogs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountingUnitId = table.Column<string>(nullable: true),
                    InvoiceNumber = table.Column<int>(nullable: false),
                    TotalInvoiceValue = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesInvoiceLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AccountingUnits",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    AuthorizedRepresentativeName = table.Column<string>(nullable: true),
                    BankAccountName = table.Column<string>(nullable: true),
                    BankAccountNumber = table.Column<string>(nullable: true),
                    BusinessName = table.Column<string>(nullable: true),
                    CompanyId = table.Column<int>(nullable: false),
                    Contact = table.Column<string>(nullable: true),
                    ContactNumber = table.Column<string>(nullable: true),
                    CurrentGrossTurnOver = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Gstin = table.Column<string>(nullable: true),
                    GstinPassword = table.Column<string>(nullable: true),
                    IfscCode = table.Column<string>(nullable: true),
                    ImgUrl = table.Column<string>(nullable: true),
                    InventorySelection = table.Column<bool>(nullable: false),
                    LastGrossTurnOver = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    PlaceOfSupply = table.Column<string>(nullable: true),
                    RegistrationType = table.Column<string>(nullable: true),
                    SelectedYear = table.Column<int>(nullable: false),
                    Subject = table.Column<string>(nullable: true),
                    TermsAndCondition = table.Column<string>(nullable: true),
                    TurnOver = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountingUnits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountingUnits_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountName = table.Column<string>(nullable: true),
                    AccountingUnitId = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    Nature = table.Column<string>(nullable: true),
                    OpeningBalance = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Account_AccountingUnits_AccountingUnitId",
                        column: x => x.AccountingUnitId,
                        principalTable: "AccountingUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountingUnitId = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    ContactNumber = table.Column<string>(nullable: true),
                    Gstin = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    RegistrationType = table.Column<int>(nullable: false),
                    State = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_AccountingUnits_AccountingUnitId",
                        column: x => x.AccountingUnitId,
                        principalTable: "AccountingUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExportInvoices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountingUnitId = table.Column<string>(nullable: true),
                    ConversionRate = table.Column<double>(nullable: false),
                    CountryOfSupply = table.Column<string>(nullable: true),
                    Currency = table.Column<double>(nullable: false),
                    CustomerBilledAddress = table.Column<string>(nullable: true),
                    CustomerName = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    ExportType = table.Column<string>(nullable: true),
                    GstPayment = table.Column<string>(nullable: true),
                    InvoiceNumber = table.Column<int>(nullable: false),
                    PortCode = table.Column<string>(nullable: true),
                    ShippingAddress = table.Column<string>(nullable: true),
                    ShippingBillNumber = table.Column<int>(nullable: false),
                    ShippingDate = table.Column<DateTime>(nullable: false),
                    SupplyType = table.Column<int>(nullable: false),
                    TotalInvoiceValue = table.Column<double>(nullable: false),
                    TotalTaxAmount = table.Column<double>(nullable: false),
                    TotalTaxableValue = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExportInvoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExportInvoices_AccountingUnits_AccountingUnitId",
                        column: x => x.AccountingUnitId,
                        principalTable: "AccountingUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountingUnitId = table.Column<string>(nullable: true),
                    Cess = table.Column<double>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    HsnSacCode = table.Column<string>(nullable: true),
                    Igst = table.Column<double>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    IsReverseChargeApplicable = table.Column<bool>(nullable: false),
                    IsTaxIncluded = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    PerReverseCharge = table.Column<double>(nullable: false),
                    ProductType = table.Column<int>(nullable: false),
                    Rate = table.Column<double>(nullable: false),
                    Unit = table.Column<string>(nullable: true),
                    UnitOthers = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_AccountingUnits_AccountingUnitId",
                        column: x => x.AccountingUnitId,
                        principalTable: "AccountingUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountingUnitId = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    ContactNumber = table.Column<string>(nullable: true),
                    Gstin = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    RegistrationType = table.Column<int>(nullable: false),
                    State = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Suppliers_AccountingUnits_AccountingUnitId",
                        column: x => x.AccountingUnitId,
                        principalTable: "AccountingUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AdjustCustomerAmount",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountId = table.Column<int>(nullable: true),
                    AccountingUnitId = table.Column<string>(nullable: true),
                    Amount = table.Column<double>(nullable: false),
                    CustomerId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    ModeOfPayment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdjustCustomerAmount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdjustCustomerAmount_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdjustCustomerAmount_AccountingUnits_AccountingUnitId",
                        column: x => x.AccountingUnitId,
                        principalTable: "AccountingUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdjustCustomerAmount_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AdvanceReceiveds",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountId = table.Column<int>(nullable: true),
                    AccountingUnitId = table.Column<string>(nullable: true),
                    BillingAddress = table.Column<string>(nullable: true),
                    CustomerId = table.Column<int>(nullable: true),
                    CustomerName = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    InvoiceCategory = table.Column<int>(nullable: false),
                    IsPlaceOfSupplyDifferent = table.Column<bool>(nullable: false),
                    ModeOfPayment = table.Column<string>(nullable: true),
                    PlaceOfSupply = table.Column<string>(nullable: true),
                    ReceiptNumber = table.Column<int>(nullable: false),
                    Reference = table.Column<string>(nullable: true),
                    SupplyType = table.Column<int>(nullable: false),
                    TotalAdvanceReceive = table.Column<double>(nullable: false),
                    TotalCessAmount = table.Column<double>(nullable: false),
                    TotalTaxAmount = table.Column<double>(nullable: false),
                    TotalTaxableValue = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvanceReceiveds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdvanceReceiveds_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdvanceReceiveds_AccountingUnits_AccountingUnitId",
                        column: x => x.AccountingUnitId,
                        principalTable: "AccountingUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdvanceReceiveds_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IssueNotes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountId = table.Column<int>(nullable: true),
                    AccountingUnitId = table.Column<string>(nullable: true),
                    CustomerId = table.Column<int>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    InvoiceCategory = table.Column<int>(nullable: false),
                    IssueNoteNumber = table.Column<int>(nullable: false),
                    NoteType = table.Column<int>(nullable: false),
                    OriginalInvoiceDate = table.Column<DateTime>(nullable: false),
                    OriginalInvoiceNumber = table.Column<int>(nullable: false),
                    OriginalInvoiceValue = table.Column<double>(nullable: false),
                    OutwardInvoiceType = table.Column<int>(nullable: false),
                    Pgst = table.Column<string>(nullable: true),
                    PlaceOfSupply = table.Column<string>(nullable: true),
                    Reason = table.Column<string>(nullable: true),
                    Refrence = table.Column<string>(nullable: true),
                    SupplyType = table.Column<int>(nullable: false),
                    TotalCessAmount = table.Column<double>(nullable: false),
                    TotalNoteValue = table.Column<double>(nullable: false),
                    TotalTaxAmount = table.Column<double>(nullable: false),
                    TotalTaxableValue = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssueNotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IssueNotes_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IssueNotes_AccountingUnits_AccountingUnitId",
                        column: x => x.AccountingUnitId,
                        principalTable: "AccountingUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IssueNotes_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SalesInvoices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountId = table.Column<int>(nullable: true),
                    AccountingUnitId = table.Column<string>(nullable: true),
                    BillingAddress = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CustomerId = table.Column<int>(nullable: true),
                    CustomerName = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    Etin = table.Column<string>(nullable: true),
                    Freight = table.Column<double>(nullable: false),
                    Insurance = table.Column<double>(nullable: false),
                    InvoiceCategory = table.Column<int>(nullable: false),
                    InvoiceNumber = table.Column<int>(nullable: false),
                    InvoicePrefix = table.Column<string>(nullable: true),
                    InvoiceType = table.Column<int>(nullable: false),
                    IsEdited = table.Column<bool>(nullable: false),
                    IsPlaceOfSupplyDifferent = table.Column<bool>(nullable: false),
                    IsRegisteredSales = table.Column<bool>(nullable: false),
                    IsReverseChargeApplicable = table.Column<bool>(nullable: false),
                    LastUpdated = table.Column<DateTime>(nullable: false),
                    NoOfTimesEdited = table.Column<int>(nullable: false),
                    PackingFwdChrg = table.Column<double>(nullable: false),
                    PlaceOfSupply = table.Column<string>(nullable: true),
                    Reference = table.Column<string>(nullable: true),
                    ShippingAddress = table.Column<string>(nullable: true),
                    SupplyType = table.Column<int>(nullable: false),
                    TotalCessAmount = table.Column<double>(nullable: false),
                    TotalInvoiceValue = table.Column<double>(nullable: false),
                    TotalTaxAmount = table.Column<double>(nullable: false),
                    TotalTaxableValue = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesInvoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalesInvoices_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesInvoices_AccountingUnits_AccountingUnitId",
                        column: x => x.AccountingUnitId,
                        principalTable: "AccountingUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesInvoices_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExportInvoiceItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<double>(nullable: false),
                    Cess = table.Column<double>(nullable: false),
                    Discount = table.Column<double>(nullable: false),
                    ExportInvoiceId = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    Quantity = table.Column<double>(nullable: false),
                    TaxAmount = table.Column<double>(nullable: false),
                    TaxRate = table.Column<double>(nullable: false),
                    TaxableValue = table.Column<double>(nullable: false),
                    UnitPrice = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExportInvoiceItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExportInvoiceItems_ExportInvoices_ExportInvoiceId",
                        column: x => x.ExportInvoiceId,
                        principalTable: "ExportInvoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExportInvoiceItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Inventory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountingUnitId = table.Column<string>(nullable: true),
                    Damage = table.Column<double>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    ProductId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inventory_AccountingUnits_AccountingUnitId",
                        column: x => x.AccountingUnitId,
                        principalTable: "AccountingUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Inventory_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AdjustSupplierAmount",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountId = table.Column<int>(nullable: true),
                    AccountingUnitId = table.Column<string>(nullable: true),
                    Amount = table.Column<double>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    ModeOfPayment = table.Column<string>(nullable: true),
                    SupplierId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdjustSupplierAmount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdjustSupplierAmount_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdjustSupplierAmount_AccountingUnits_AccountingUnitId",
                        column: x => x.AccountingUnitId,
                        principalTable: "AccountingUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdjustSupplierAmount_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AdvancePaids",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountId = table.Column<int>(nullable: true),
                    AccountingUnitId = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    ModeOfPayment = table.Column<string>(nullable: true),
                    PlaceOfSupply = table.Column<string>(nullable: true),
                    ReceiptNumber = table.Column<string>(nullable: true),
                    ShippingAddress = table.Column<string>(nullable: true),
                    SupplierId = table.Column<int>(nullable: true),
                    SupplyType = table.Column<int>(nullable: false),
                    TotalAdvancePaid = table.Column<double>(nullable: false),
                    TotalCessAmount = table.Column<double>(nullable: false),
                    TotalTaxAmount = table.Column<double>(nullable: false),
                    TotalTaxableValue = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvancePaids", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdvancePaids_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdvancePaids_AccountingUnits_AccountingUnitId",
                        column: x => x.AccountingUnitId,
                        principalTable: "AccountingUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdvancePaids_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseInvoices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountId = table.Column<int>(nullable: true),
                    AccountingUnitId = table.Column<string>(nullable: true),
                    BilledPassed = table.Column<bool>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Etin = table.Column<string>(nullable: true),
                    InvoiceCategory = table.Column<int>(nullable: false),
                    InvoiceNumber = table.Column<string>(nullable: true),
                    InvoiceType = table.Column<int>(nullable: false),
                    IsRegisteredPurchase = table.Column<bool>(nullable: false),
                    IsReverseChargeApplicable = table.Column<bool>(nullable: false),
                    PlaceOfSupply = table.Column<string>(nullable: true),
                    PurchaseInvoiceType = table.Column<string>(nullable: true),
                    Reference = table.Column<string>(nullable: true),
                    ShippingAddress = table.Column<string>(nullable: true),
                    SupplierId = table.Column<int>(nullable: true),
                    SupplyType = table.Column<int>(nullable: false),
                    TotalCessAmount = table.Column<double>(nullable: false),
                    TotalInvoiceValue = table.Column<double>(nullable: false),
                    TotalTaxAmount = table.Column<double>(nullable: false),
                    TotalTaxableValue = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseInvoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseInvoices_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseInvoices_AccountingUnits_AccountingUnitId",
                        column: x => x.AccountingUnitId,
                        principalTable: "AccountingUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseInvoices_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReceiptNotes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountId = table.Column<int>(nullable: true),
                    AccountingUnitId = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    InventoryDate = table.Column<DateTime>(nullable: false),
                    InvoiceCategory = table.Column<int>(nullable: false),
                    ModeOfPayment = table.Column<string>(nullable: true),
                    NoteType = table.Column<int>(nullable: false),
                    PaidAmount = table.Column<double>(nullable: false),
                    Pgst = table.Column<string>(nullable: true),
                    PlaceOfSupply = table.Column<string>(nullable: true),
                    PurchaseInvoiceDate = table.Column<DateTime>(nullable: false),
                    PurchaseInvoiceNumber = table.Column<string>(nullable: true),
                    PurchaseInvoiceValue = table.Column<double>(nullable: false),
                    Reason = table.Column<string>(nullable: true),
                    ReceiptNoteNumber = table.Column<string>(nullable: true),
                    Refrence = table.Column<string>(nullable: true),
                    SupplierId = table.Column<int>(nullable: true),
                    SupplyType = table.Column<int>(nullable: false),
                    TotalCessAmount = table.Column<double>(nullable: false),
                    TotalNoteValue = table.Column<double>(nullable: false),
                    TotalTaxAmount = table.Column<double>(nullable: false),
                    TotalTaxableValue = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiptNotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReceiptNotes_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceiptNotes_AccountingUnits_AccountingUnitId",
                        column: x => x.AccountingUnitId,
                        principalTable: "AccountingUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceiptNotes_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AdvanceReceivedItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AdvanceAmount = table.Column<double>(nullable: false),
                    AdvanceReceivedId = table.Column<int>(nullable: false),
                    CessAmount = table.Column<double>(nullable: false),
                    CgstAmount = table.Column<double>(nullable: false),
                    IgstAmount = table.Column<double>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    SgstAmount = table.Column<double>(nullable: false),
                    TaxAmount = table.Column<double>(nullable: false),
                    TaxRate = table.Column<double>(nullable: false),
                    TaxableValue = table.Column<double>(nullable: false),
                    Total = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvanceReceivedItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdvanceReceivedItems_AdvanceReceiveds_AdvanceReceivedId",
                        column: x => x.AdvanceReceivedId,
                        principalTable: "AdvanceReceiveds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdvanceReceivedItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IssueNoteItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CessAmount = table.Column<double>(nullable: false),
                    CgstAmount = table.Column<double>(nullable: false),
                    IgstAmount = table.Column<double>(nullable: false),
                    IssueNoteId = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    Quantity = table.Column<double>(nullable: false),
                    SgstAmount = table.Column<double>(nullable: false),
                    TaxAmount = table.Column<double>(nullable: false),
                    TaxRate = table.Column<double>(nullable: false),
                    TaxableValue = table.Column<double>(nullable: false),
                    Total = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssueNoteItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IssueNoteItems_IssueNotes_IssueNoteId",
                        column: x => x.IssueNoteId,
                        principalTable: "IssueNotes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IssueNoteItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SalesInvoiceItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<double>(nullable: false),
                    Cess = table.Column<double>(nullable: false),
                    CgstAmount = table.Column<double>(nullable: false),
                    Discount = table.Column<double>(nullable: false),
                    IgstAmount = table.Column<double>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    Quantity = table.Column<double>(nullable: false),
                    SalesInvoiceId = table.Column<int>(nullable: false),
                    SgstAmount = table.Column<double>(nullable: false),
                    TaxAmount = table.Column<double>(nullable: false),
                    TaxRate = table.Column<double>(nullable: false),
                    TaxableValue = table.Column<double>(nullable: false),
                    Total = table.Column<double>(nullable: false),
                    UnitPrice = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesInvoiceItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalesInvoiceItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesInvoiceItems_SalesInvoices_SalesInvoiceId",
                        column: x => x.SalesInvoiceId,
                        principalTable: "SalesInvoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AdvancePaidItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AdvanceAmount = table.Column<double>(nullable: false),
                    AdvancePaidId = table.Column<int>(nullable: false),
                    CessAmount = table.Column<double>(nullable: false),
                    CgstAmount = table.Column<double>(nullable: false),
                    Discount = table.Column<double>(nullable: false),
                    IgstAmount = table.Column<double>(nullable: false),
                    ItcEligibility = table.Column<string>(nullable: true),
                    ItcPercentage = table.Column<double>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    Quantity = table.Column<double>(nullable: false),
                    SgstAmount = table.Column<double>(nullable: false),
                    TaxAmount = table.Column<double>(nullable: false),
                    TaxRate = table.Column<double>(nullable: false),
                    TaxableValue = table.Column<double>(nullable: false),
                    Total = table.Column<double>(nullable: false),
                    UnitPrice = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvancePaidItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdvancePaidItems_AdvancePaids_AdvancePaidId",
                        column: x => x.AdvancePaidId,
                        principalTable: "AdvancePaids",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdvancePaidItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseInvoiceItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<double>(nullable: false),
                    CessAmount = table.Column<double>(nullable: false),
                    CgstAmount = table.Column<double>(nullable: false),
                    Discount = table.Column<double>(nullable: false),
                    IgstAmount = table.Column<double>(nullable: false),
                    ItcEligibility = table.Column<string>(nullable: true),
                    ItcPercentage = table.Column<double>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    PurchaseInvoiceId = table.Column<int>(nullable: false),
                    Quantity = table.Column<double>(nullable: false),
                    SgstAmount = table.Column<double>(nullable: false),
                    TaxAmount = table.Column<double>(nullable: false),
                    TaxRate = table.Column<double>(nullable: false),
                    TaxableValue = table.Column<double>(nullable: false),
                    Total = table.Column<double>(nullable: false),
                    UnitPrice = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseInvoiceItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseInvoiceItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseInvoiceItems_PurchaseInvoices_PurchaseInvoiceId",
                        column: x => x.PurchaseInvoiceId,
                        principalTable: "PurchaseInvoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReceiptNoteItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CessAmount = table.Column<double>(nullable: false),
                    CgstAmount = table.Column<double>(nullable: false),
                    Discount = table.Column<double>(nullable: false),
                    IgstAmount = table.Column<double>(nullable: false),
                    ItcEligibility = table.Column<string>(nullable: true),
                    ItcPercentage = table.Column<double>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    Quantity = table.Column<double>(nullable: false),
                    ReceiptNoteId = table.Column<int>(nullable: false),
                    SgstAmount = table.Column<double>(nullable: false),
                    TaxAmount = table.Column<double>(nullable: false),
                    TaxRate = table.Column<double>(nullable: false),
                    TaxableValue = table.Column<double>(nullable: false),
                    Total = table.Column<double>(nullable: false),
                    UnitPrice = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiptNoteItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReceiptNoteItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceiptNoteItems_ReceiptNotes_ReceiptNoteId",
                        column: x => x.ReceiptNoteId,
                        principalTable: "ReceiptNotes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Account_AccountingUnitId",
                table: "Account",
                column: "AccountingUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountingUnits_CompanyId",
                table: "AccountingUnits",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_AdjustCustomerAmount_AccountId",
                table: "AdjustCustomerAmount",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AdjustCustomerAmount_AccountingUnitId",
                table: "AdjustCustomerAmount",
                column: "AccountingUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_AdjustCustomerAmount_CustomerId",
                table: "AdjustCustomerAmount",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_AdjustSupplierAmount_AccountId",
                table: "AdjustSupplierAmount",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AdjustSupplierAmount_AccountingUnitId",
                table: "AdjustSupplierAmount",
                column: "AccountingUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_AdjustSupplierAmount_SupplierId",
                table: "AdjustSupplierAmount",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_AdvancePaidItems_AdvancePaidId",
                table: "AdvancePaidItems",
                column: "AdvancePaidId");

            migrationBuilder.CreateIndex(
                name: "IX_AdvancePaidItems_ProductId",
                table: "AdvancePaidItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_AdvancePaids_AccountId",
                table: "AdvancePaids",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AdvancePaids_AccountingUnitId",
                table: "AdvancePaids",
                column: "AccountingUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_AdvancePaids_SupplierId",
                table: "AdvancePaids",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_AdvanceReceivedItems_AdvanceReceivedId",
                table: "AdvanceReceivedItems",
                column: "AdvanceReceivedId");

            migrationBuilder.CreateIndex(
                name: "IX_AdvanceReceivedItems_ProductId",
                table: "AdvanceReceivedItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_AdvanceReceiveds_AccountId",
                table: "AdvanceReceiveds",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AdvanceReceiveds_AccountingUnitId",
                table: "AdvanceReceiveds",
                column: "AccountingUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_AdvanceReceiveds_CustomerId",
                table: "AdvanceReceiveds",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_AdvanceReceiveds_ReceiptNumber_AccountingUnitId",
                table: "AdvanceReceiveds",
                columns: new[] { "ReceiptNumber", "AccountingUnitId" },
                unique: true,
                filter: "[AccountingUnitId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_AccountingUnitId",
                table: "Customers",
                column: "AccountingUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_ExportInvoiceItems_ExportInvoiceId",
                table: "ExportInvoiceItems",
                column: "ExportInvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ExportInvoiceItems_ProductId",
                table: "ExportInvoiceItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ExportInvoices_AccountingUnitId",
                table: "ExportInvoices",
                column: "AccountingUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_AccountingUnitId",
                table: "Inventory",
                column: "AccountingUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_ProductId",
                table: "Inventory",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueNoteItems_IssueNoteId",
                table: "IssueNoteItems",
                column: "IssueNoteId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueNoteItems_ProductId",
                table: "IssueNoteItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueNotes_AccountId",
                table: "IssueNotes",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueNotes_AccountingUnitId",
                table: "IssueNotes",
                column: "AccountingUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueNotes_CustomerId",
                table: "IssueNotes",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueNotes_IssueNoteNumber_AccountingUnitId",
                table: "IssueNotes",
                columns: new[] { "IssueNoteNumber", "AccountingUnitId" },
                unique: true,
                filter: "[AccountingUnitId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Products_AccountingUnitId",
                table: "Products",
                column: "AccountingUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseInvoiceItems_ProductId",
                table: "PurchaseInvoiceItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseInvoiceItems_PurchaseInvoiceId",
                table: "PurchaseInvoiceItems",
                column: "PurchaseInvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseInvoices_AccountId",
                table: "PurchaseInvoices",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseInvoices_AccountingUnitId",
                table: "PurchaseInvoices",
                column: "AccountingUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseInvoices_SupplierId",
                table: "PurchaseInvoices",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptNoteItems_ProductId",
                table: "ReceiptNoteItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptNoteItems_ReceiptNoteId",
                table: "ReceiptNoteItems",
                column: "ReceiptNoteId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptNotes_AccountId",
                table: "ReceiptNotes",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptNotes_AccountingUnitId",
                table: "ReceiptNotes",
                column: "AccountingUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptNotes_SupplierId",
                table: "ReceiptNotes",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesInvoiceItems_ProductId",
                table: "SalesInvoiceItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesInvoiceItems_SalesInvoiceId",
                table: "SalesInvoiceItems",
                column: "SalesInvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesInvoices_AccountId",
                table: "SalesInvoices",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesInvoices_AccountingUnitId",
                table: "SalesInvoices",
                column: "AccountingUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesInvoices_CustomerId",
                table: "SalesInvoices",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesInvoices_InvoiceNumber_AccountingUnitId",
                table: "SalesInvoices",
                columns: new[] { "InvoiceNumber", "AccountingUnitId" },
                unique: true,
                filter: "[AccountingUnitId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_AccountingUnitId",
                table: "Suppliers",
                column: "AccountingUnitId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdjustCustomerAmount");

            migrationBuilder.DropTable(
                name: "AdjustSupplierAmount");

            migrationBuilder.DropTable(
                name: "AdvancePaidItems");

            migrationBuilder.DropTable(
                name: "AdvanceReceivedItems");

            migrationBuilder.DropTable(
                name: "ExportInvoiceItems");

            migrationBuilder.DropTable(
                name: "Inventory");

            migrationBuilder.DropTable(
                name: "IssueNoteItems");

            migrationBuilder.DropTable(
                name: "PurchaseInvoiceItems");

            migrationBuilder.DropTable(
                name: "ReceiptNoteItems");

            migrationBuilder.DropTable(
                name: "SalesInvoiceItems");

            migrationBuilder.DropTable(
                name: "SalesInvoiceLogs");

            migrationBuilder.DropTable(
                name: "AdvancePaids");

            migrationBuilder.DropTable(
                name: "AdvanceReceiveds");

            migrationBuilder.DropTable(
                name: "ExportInvoices");

            migrationBuilder.DropTable(
                name: "IssueNotes");

            migrationBuilder.DropTable(
                name: "PurchaseInvoices");

            migrationBuilder.DropTable(
                name: "ReceiptNotes");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "SalesInvoices");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "AccountingUnits");

            migrationBuilder.DropTable(
                name: "Companies");
        }
    }
}
