using System;
using System.Collections.Generic;
using System.Linq;
using BiharEnergy.Core.Enums;
using BiharEnergy.WebApp.Api.AccountingUnitApi;
using BiharEnergy.WebApp.Api.CustomerApi;
using BiharEnergy.WebApp.Api.ProductApi;

namespace BiharEnergy.WebApp.Api.SaleInvoiceApi
{
    public class PrintSalesInvoice
    {
        public AccountingUnitResource AccountingUnit { get; set; }
        public DateTime Date { get; set; }
        public int InvoiceNumber { get; set; }

        public CustomerResource Customer { get; set; }
        public string CustomerName { get; set; }

        public string BillingAddress { get; set; }
        public string InvoicePrefix { get; set; }


        public double ReceivedAmount { get; set; }
        public string ShippingAddress { get; set; }
        public DateTime ShippingDate { get; set; }
        public string PlaceOfSupply { get; set; }
        public string ModeOfPayment { get; set; }
        public string Reference { get; set; }


        public List<PrintSalesInvoiceItem> SalesInvoiceItems { get; set; }
        public double TotalIgst=>SalesInvoiceItems.Sum(item => item.IgstAmount);
        public double TotalSgst=>SalesInvoiceItems.Sum(item => item.CgstAmount);
        public double TotalCgst=>SalesInvoiceItems.Sum(item => item.SgstAmount);
        public double TotalDiscount => SalesInvoiceItems.Sum(item => item.Discount);       
        public double TotalCess => SalesInvoiceItems.Sum(item => item.Cess);
        public double TotalTaxableValue { get; set; }
        public double TotalTaxAmount { get; set; }
        public double TotalInvoiceValue { get; set; }
        public SupplyType SupplyType { get; set; }



    }
    public class PrintSalesInvoiceItem
    {
        public ProductResource Product { get; set; }
        public double Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double Discount { get; set; }
        public double TaxableValue { get; set; }
        public double TaxRate { get; set; }
        public double IgstAmount { get; set; }
        public double CgstAmount { get; set; }
        public double SgstAmount { get; set; }
        public double Cess { get; set; }
        public double Total { get; set; }

    }

}