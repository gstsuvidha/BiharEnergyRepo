using System;
using System.Collections.Generic;

namespace BiharEnergy.WebApp.Api.PurchaseApi
{
    public class SavePurchaseInvoiceResource
    {
        public DateTime Date { get; set; }
        public DateTime PostingDate { get; set; }

        public string InvoiceNumber { get; set; }

        public string PurchaseInvoiceType { get; set; }

        public bool IsReverseChargeApplicable { get; set; }

      
        public int? SupplierId { get; set; }
    

        public string Etin { get; set; }


        public string PlaceOfSupply { get; set; }
        
        public string ShippingAddress { get; set; }

        public string Reference { get; set; }

        public double TotalTaxableValue { get; set; }
        public double TotalTaxAmount { get; set; }
        public double TotalCessAmount { get; set; }
        public double TotalInvoiceValue { get; set; }
        public bool BilledPassed { get; set; }
        




        public List<SavePurchaseInvoiceItemResource> PurchaseInvoiceItems { get; set; }
    }
}