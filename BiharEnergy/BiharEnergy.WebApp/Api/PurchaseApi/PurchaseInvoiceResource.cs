using System;
using BiharEnergy.WebApp.Mappings;

namespace BiharEnergy.WebApp.Api.PurchaseApi
{
    public class PurchaseInvoiceResource
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }
        public DateTime PostingDate { get; set; }

        public string InvoiceNumber { get; set; }

        public int? SupplierId { get; set; }
        
        
        
        public double TotalIgst { get; set; }
        public double TotalSgst { get; set; }
        public double TotalCgst { get; set; }
        public bool IsRegisteredPurchase { get; set; }


        public double TotalTaxableValue { get; set; }
        public double TotalTaxAmount { get; set; }
        public double TotalCessAmount { get; set; }
        public double TotalInvoiceValue { get; set; }

        public KeyValuePairResource Supplier { get; set; }

    }
}