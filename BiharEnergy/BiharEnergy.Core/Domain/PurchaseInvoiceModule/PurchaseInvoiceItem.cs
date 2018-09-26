using BiharEnergy.Core.Domain.ProductModule;

namespace BiharEnergy.Core.Domain.PurchaseInvoiceModule
{
   public  class PurchaseInvoiceItem
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int PurchaseInvoiceId { get; set; }

        public double Quantity { get; set; }

        public double UnitPrice { get; set; }

        public double Discount { get; set; }

        public double TaxRate { get; set; }

        public double TaxableValue { get; set; }

        public double TaxAmount { get; set; }
        
        public double IgstAmount { get; set; }
        
        public double CgstAmount { get; set; }
        
        public double SgstAmount { get; set; }


        public double CessAmount { get; set; }
        public double Total { get; set; }

        public double Amount { get; set; }

        public PurchaseInvoice PurchaseInvoice { get; set; }

        public Product Product { get; set; }

        protected PurchaseInvoiceItem()
        {
            //EF
        }

        public PurchaseInvoiceItem(int productId, double quantity, double unitPrice,
            double discount, double taxRate, double taxableValue, double taxAmount,
            double igstAmount, double cgstAmount, double sgstAmount,
            double cessAmount, double total, double amount)
        {
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
            Discount = discount;
            TaxRate = taxRate;
            TaxableValue = taxableValue;
            TaxAmount = taxAmount;
            IgstAmount = igstAmount;
            CgstAmount = cgstAmount;
            SgstAmount = sgstAmount;
            CessAmount = cessAmount;
            Total= total;
            Amount = amount;
        }

    }
}
