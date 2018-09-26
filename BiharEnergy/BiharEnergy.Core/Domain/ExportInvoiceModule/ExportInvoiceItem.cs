using System;
using System.Collections.Generic;
using System.Text;
using BiharEnergy.Core.Domain.ProductModule;

namespace BiharEnergy.Core.Domain.ExportInvoiceModule
{
   public class ExportInvoiceItem
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int ExportInvoiceId { get; set; } 

        public double Quantity { get; set; }

        public double UnitPrice { get; set; }

        public double Discount { get; set; }

        public double TaxRate { get; set; }

        public double TaxableValue { get; set; }

        public double TaxAmount { get; set; }

        public double Cess { get; set; }

        public double Amount { get; set; }

        //foreign keys

        public ExportInvoice ExportInvoice { get; set; }

        public Product Product { get; set; }
        protected ExportInvoiceItem()
        {
            //EF
        }

        public ExportInvoiceItem(int productId, double quantity, double unitPrice,
                                double discount, double taxRate, double taxableValue, double taxAmount,
                                double cess, double amount)
        {
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
            Discount = discount;
            TaxRate = taxRate;
            TaxableValue = taxableValue;
            TaxAmount = taxAmount;
            Cess = cess;
            Amount = amount;
        }
    }
}
