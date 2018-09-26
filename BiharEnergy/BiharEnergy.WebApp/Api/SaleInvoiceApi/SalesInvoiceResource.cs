using System;
using BiharEnergy.Core.Enums;
using BiharEnergy.WebApp.Mappings;

namespace BiharEnergy.WebApp.Api.SaleInvoiceApi
{
    public class SalesInvoiceResource
    {


        public int Id { get; set; }

        public DateTime Date { get; set; }

        public int InvoiceNumber { get; set; }

        public int? CustomerId { get; set; }

        public bool IsRegisteredSales { get; set; }

        public string CustomerName { get; set; }

        public string BillingAddress { get; set; }



        public double TotalTaxableValue { get; set; }
        public double TotalTaxAmount { get; set; }
        public double TotalCessAmount { get; set; }
        public double TotalInvoiceValue { get; set; }


        public double TotalIgst { get; set; }
        public double TotalSgst { get; set; }
        public double TotalCgst { get; set; }

        public SupplyType SupplyType { get; set; }

        public KeyValuePairResource Customer { get; set; }
        

        public bool IsEdited { get; set; }

        public DateTime CreatedAt { get; set; }

        public int NoOfTimesEdited { get; set; }

        public DateTime LastUpdated { get; set; }
        public string InvoicePrefix { get; set; }


    }


}