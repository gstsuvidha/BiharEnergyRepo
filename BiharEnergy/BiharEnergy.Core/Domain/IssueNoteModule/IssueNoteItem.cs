using System;
using BiharEnergy.Core.Enums;
using System.Collections.Generic;
using System.Text;
using BiharEnergy.Core.Domain.ProductModule;

namespace BiharEnergy.Core.Domain.IssueNoteModule
{
    public class IssueNoteItem
    {

        public int Id { get; set; }

        public int ProductId { get; set; }

        public double Quantity { get; set; }

        public int IssueNoteId { get; set; }

        public double TaxRate { get; set; }

        public double TaxableValue { get; set; }

        public double TaxAmount { get; set; }
        public double IgstAmount { get; set; }
        public double CgstAmount { get; set; }
        public double SgstAmount { get; set; }
     
        public double CessAmount { get; set; }
        public double Total { get; set; }

        public IssueNote IssueNote { get; set; }

        public Product Product { get; set; }

        public IssueNoteItem()
        {

        }
        public IssueNoteItem(int productId, double quantity, double taxRate, double taxableValue, double taxAmount,double cessAmount, 
        double total)
        {
          
            ProductId = productId;
            Quantity = quantity;
            TaxRate = taxRate;
            TaxableValue = taxableValue;
            TaxAmount = taxAmount;
            CessAmount = cessAmount;
            Total = total;

        }

        public void CalculateTax(SupplyType supplyType)
        {
            IgstAmount = supplyType == SupplyType.InterState ? TaxAmount : 0;
            CgstAmount = supplyType == SupplyType.InterState ? 0:TaxAmount / 2;
            SgstAmount = supplyType == SupplyType.InterState ? 0:TaxAmount / 2;
        }




    }
}
