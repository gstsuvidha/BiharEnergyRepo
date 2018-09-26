using System.Collections.Generic;
using BiharEnergy.Core.Domain.ProductModule;
using BiharEnergy.Core.Enums;
using BiharEnergy.Core.Helpers;


namespace BiharEnergy.Core.Domain.SalesInvoiceModule
{
    public class SalesInvoiceItem
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int SalesInvoiceId { get; set; }

        public double Quantity { get; set; }

        public double UnitPrice { get; set; }

        public double Discount { get; set; }

        public double TaxRate { get; set; }

        public double TaxableValue { get; set; }

        public double TaxAmount { get; set; }

        public double IgstAmount { get; set; }

        public double CgstAmount { get; set; }

        public double SgstAmount { get; set; }


        public double Cess { get; set; }

        public double Total { get; set; }

        public double Amount { get; set; }

        public SalesInvoice SalesInvoice { get; set; }

        public Product Product { get; set; }

        protected SalesInvoiceItem()
        {
            //EF
        }

        private SalesInvoiceItem(int productId, double quantity, double unitPrice,
                                double discount, double taxRate, double taxableValue, double taxAmount,
                                double cess, double total)
        {
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
            Discount = discount;
            TaxRate = taxRate;
            TaxableValue = taxableValue;
            TaxAmount = taxAmount;
            Cess = cess;
            Total = total;
            Amount = taxAmount + taxableValue;
        }


        public static SalesInvoiceItem Add(int productId, double quantity, double unitPrice,
                                double discount, double taxRate, double taxableValue, double taxAmount,
                                double cess, double total)
        {
            return new SalesInvoiceItem(productId, quantity, unitPrice, discount, taxRate, taxableValue, taxAmount, cess, total);
        }

        public void CalculateTax(SupplyType supplyType)
        {
            IgstAmount = supplyType == SupplyType.InterState ? TaxAmount : 0;
            CgstAmount = supplyType == SupplyType.InterState ? 0 : TaxAmount / 2;
            SgstAmount = supplyType == SupplyType.InterState ? 0 : TaxAmount / 2;

        }

    }
}
