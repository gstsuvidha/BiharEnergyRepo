using System;
using System.Collections.Generic;
using System.Text;
using BiharEnergy.Core.Domain.Accounting;
using BiharEnergy.Core.Domain.AccountModule;
using BiharEnergy.Core.Domain.Inferface;
using BiharEnergy.Core.Domain.SupplierModule;
using BiharEnergy.Core.Enums;

namespace BiharEnergy.Core.Domain.AdvancePaidModule
{
    public class AdvancePaid : IHaveAccountingUnit,IHaveDateFilter
    {
        public int Id { get; set; }

        public string ReceiptNumber { get; set; }

        public DateTime Date { get; set; }

        public int? SupplierId { get; set; }
        public int? AccountId { get; set; }

        public string PlaceOfSupply { get; set; }
        public string ShippingAddress { get; set; }

        public SupplyType SupplyType { get; set; }

        public double TotalTaxableValue { get; set; }
        public double TotalTaxAmount { get; set; }
        public double TotalCessAmount { get; set; }
        public double TotalAdvancePaid{ get; set; }

        public string ModeOfPayment { get; set; }

        public List<AdvancePaidItem> AdvancePaidItems { get; set; }

        public string AccountingUnitId { get; set; }
        public AccountingUnit AccountingUnit { get; set; }
        public Account Account { get; set; }

        public Supplier Supplier { get; set; }

        protected AdvancePaid()
        {
            //EF
        }
        public AdvancePaid(string receiptNumber, DateTime date, 
                            int? supplierId, 
                            string placeOfSupply,
                             string shippingAddress, double totalTaxableValue, 
                                double totalTaxAmount, double totalCessAmount, double totalAdvancePaid,
                                string modeOfPayment, int? accountId,
                                List<AdvancePaidItem> advancePaidItems,
                                Supplier supplier,AccountingUnit accountingUnit)
        {
            Supplier = supplier;
            PlaceOfSupply = placeOfSupply;
            AccountingUnit = accountingUnit;
            AccountingUnitId = AccountingUnit.Id;
            SupplierId = supplierId;




            ReceiptNumber = receiptNumber;
            Date = date;
    
            ShippingAddress = shippingAddress;
            TotalTaxableValue = totalTaxableValue;
            TotalTaxAmount = totalTaxAmount;
            TotalCessAmount = totalCessAmount;
            TotalAdvancePaid= totalAdvancePaid;
            ModeOfPayment= modeOfPayment;
            AccountId = accountId;
            AdvancePaidItems = advancePaidItems;

                 CalculatePlaceOfSupply();


        }

            private void CalculatePlaceOfSupply()
       {

            if(Supplier != null)
            {
                SupplyType = PlaceOfSupply == Supplier.State

                 ? SupplyType.IntraState
                 : SupplyType.InterState;
               
                 }
                 else
                 {
                    SupplyType = PlaceOfSupply == AccountingUnit.PlaceOfSupply
                    
                    ? SupplyType.IntraState
                    : SupplyType.InterState;
                 }
       }


        public void Modify(DateTime date, int? supplierId, string placeOfSupply, 
                            string shippingAddress,
                            double totalTaxableValue, double totalTaxAmount, double totalCessAmount, 
                            double totalAdvancePaid,string modeOfPayment, int? accountId,
                            List<AdvancePaidItem> advancePaidItems,Supplier supplier,AccountingUnit accountingUnit)
        {
            Supplier = supplier;
            PlaceOfSupply = placeOfSupply;
            AccountingUnit = accountingUnit;

            Date = date;
            SupplierId = supplierId;
            ShippingAddress = shippingAddress;
            TotalTaxableValue = totalTaxableValue;
            ModeOfPayment = modeOfPayment;
            TotalTaxAmount = totalTaxAmount;
            TotalCessAmount = totalCessAmount;
            TotalAdvancePaid= totalAdvancePaid;
            ModeOfPayment=modeOfPayment;
            AccountId = accountId;
            AdvancePaidItems.Clear();
            AdvancePaidItems = advancePaidItems;
            CalculatePlaceOfSupply();

       
            UpdateTaxCalculation();

        }

        private void UpdateTaxCalculation()
        {
            foreach (var item in AdvancePaidItems)
            {
                item.CalculateTax(SupplyType);
            }

        }
    }
}
