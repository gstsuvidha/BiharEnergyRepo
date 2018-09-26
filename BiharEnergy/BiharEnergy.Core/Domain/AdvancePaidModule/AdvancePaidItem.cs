using System;
using System.Collections.Generic;
using System.Text;
using BiharEnergy.Core.Domain.ProductModule;
using BiharEnergy.Core.Enums;

namespace BiharEnergy.Core.Domain.AdvancePaidModule
{
    public class AdvancePaidItem
    {
        public int Id { get; set; }

        public int AdvancePaidId { get; set; }

        public int ProductId { get; set; }
        public double Quantity {get; set;}
        public double UnitPrice {get; set;}
        public double Discount {get; set;}

        public double TaxRate { get; set; }
        public double CessAmount { get; set; }
         public double ItcPercentage { get; set; }
        public string ItcEligibility { get; set; }

        public double Total { get; set; }
       
        public double TaxableValue { get; set; }

        public double TaxAmount { get; set; }

        public double IgstAmount { get; set; }
        public double CgstAmount { get; set; }
        public double SgstAmount { get; set; }

        public double AdvanceAmount { get; set; }

        public Product Product { get; set; }
        public AdvancePaid AdvancePaid { get; set; }


        public AdvancePaidItem()
        {
            
        }

        public AdvancePaidItem(int productId, double quantity, double unitPrice, double discount,  double taxRate, double taxableValue, double taxAmount, double igstAmount,
                               double cgstAmount, double sgstAmount,
                                double cessAmount, string itcEligibility, double itcPercentage,
                                double total, double advanceAmount)
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
            ItcEligibility = itcEligibility;
            ItcPercentage = itcPercentage;
            Total = total;
            AdvanceAmount = advanceAmount;
        }

        public void CalculateTax(SupplyType supplyType)
        {
            IgstAmount = supplyType == SupplyType.InterState ? TaxAmount : 0;
            CgstAmount = supplyType == SupplyType.InterState ? 0:TaxAmount / 2;
            SgstAmount = supplyType == SupplyType.InterState ? 0:TaxAmount / 2;

        }
    }
}
