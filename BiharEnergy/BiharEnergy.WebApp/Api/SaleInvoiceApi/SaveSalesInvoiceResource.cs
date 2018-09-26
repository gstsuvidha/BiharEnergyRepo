using System;
using System.Collections.Generic;

namespace BiharEnergy.WebApp.Api.SaleInvoiceApi
{
    public class SaveSalesInvoiceResource
    {


        public DateTime Date { get; set; }

        public string InvoicePrefix { get; set; }

        public int InvoiceNumber { get; set; }


        public bool IsReverseChargeApplicable { get; set; }

        public int? CustomerId { get; set; }


        public string CustomerName { get; set; }

        public string BillingAddress { get; set; }




        public string Etin { get; set; }


        public string PlaceOfSupply { get; set; }
        public bool IsPlaceOfSupplyDifferent { get; set; }
        public string ShippingAddress { get; set; }
        public string Reference { get; set; }

        public double Freight { get; set; }
        public double Insurance { get; set; }
        public double PackingFwdChrg { get; set; }
        public double TotalTaxableValue { get; set; }
        public double TotalTaxAmount { get; set; }
        public double TotalCessAmount { get; set; }
        public double TotalInvoiceValue { get; set; }

        public List<SaveSalesInvoiceItemResource> SalesInvoiceItems { get; set; }



    }

    public class SaveSalesInvoiceItemResource
    {

        public int ProductId { get; set; }

        public double Quantity { get; set; }

        public double UnitPrice { get; set; }

        public double Discount { get; set; }

        public double TaxRate { get; set; }

        public double TaxableValue { get; set; }

        public double TaxAmount { get; set; }

        public double Cess { get; set; }
        public double Total { get; set; }
        public string InvoicePrefix { get; set; }



    }

}