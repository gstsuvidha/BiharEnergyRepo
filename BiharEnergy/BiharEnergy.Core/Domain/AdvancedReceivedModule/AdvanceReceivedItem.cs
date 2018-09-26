using System;
using System.Collections.Generic;
using System.Text;
using BiharEnergy.Core.Domain.ProductModule;
using BiharEnergy.Core.Enums;

namespace BiharEnergy.Core.Domain.AdvancedReceivedModule
{
    public class AdvanceReceivedItem
    {

        public int Id { get; set; }

        public int AdvanceReceivedId { get; set; }

        public int ProductId { get; set; }

        public double TaxRate { get; set; }

        public double TaxableValue { get; set; }

        public double TaxAmount { get; set; }

        public double IgstAmount { get; set; }
        public double CgstAmount { get; set; }
        public double SgstAmount { get; set; }
        public double CessAmount { get; set; }
        public double Total { get; set; }

        public double AdvanceAmount { get; set; }

        public Product Product { get; set; }
        public AdvanceReceived AdvanceReceived { get; set; }


        public AdvanceReceivedItem()
        {
            
        }

        public AdvanceReceivedItem(int productId, double taxRate, double taxableValue, double taxAmount, 
                                   double cessAmount, double total, double advanceAmount)
        {
            ProductId = productId;
            TaxRate = taxRate;
            TaxableValue = taxableValue;
            TaxAmount = taxAmount;
            CessAmount = cessAmount;
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
