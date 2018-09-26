using BiharEnergy.Core.Domain.ProductModule;
using BiharEnergy.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BiharEnergy.Core.Domain.ReceiptNoteModule
{
    public class ReceiptNoteItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public double Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double TaxRate { get; set; }
        public double Discount { get; set; }
        public double TaxableValue { get; set; }
        public double TaxAmount { get; set; }
        public double IgstAmount { get; set; }
        public double CgstAmount { get; set; }
        public double SgstAmount { get; set; }
        public double CessAmount { get; set; }
        public string ItcEligibility { get; set; }
        public double ItcPercentage { get; set; }
        public double Total { get; set; }
        public int ReceiptNoteId { get; set; }
        public ReceiptNote ReceiptNote { get; set; }
        public Product Product { get; set; }
        public ReceiptNoteItem()
        {

        }
        public ReceiptNoteItem(int productId, double quantity,double unitPrice, double taxRate, double taxableValue, double taxAmount,double igstAmount,double sgstAmount,double cgstAmount ,double cessAmount, SupplyType supplyType, double discount,
            string itcEligiblity, double itcPercentage, double total)
        {
            ProductId = productId;
            TaxRate = taxRate;
            UnitPrice = unitPrice;
            Quantity = quantity;
            TaxableValue = taxableValue;
            TaxAmount = taxAmount;
            IgstAmount = igstAmount;
            CgstAmount = cgstAmount;
            SgstAmount = sgstAmount;
            CessAmount = cessAmount;
            ItcEligibility = itcEligiblity;
            ItcPercentage = itcPercentage;
            Discount = discount;
            Total = total;

        }
    }

}

