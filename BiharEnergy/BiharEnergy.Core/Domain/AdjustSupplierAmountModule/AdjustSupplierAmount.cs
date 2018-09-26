using System;
using BiharEnergy.Core.Domain.Accounting;
using BiharEnergy.Core.Domain.AccountModule;
using BiharEnergy.Core.Domain.Inferface;
using BiharEnergy.Core.Domain.SupplierModule;

namespace BiharEnergy.Core.Domain.AdjustSupplierAmountModule
{
   public class AdjustSupplierAmount:IHaveAccountingUnit
    {
        public int Id { get; set; }
        public int SupplierId { get; set; }
        public string ModeOfPayment { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }
        public int? AccountId { get; set; }
        public Account Account { get; set; }
        public string AccountingUnitId { get; set; }
        public AccountingUnit AccountingUnit { get; set; }
        public Supplier Supplier { get; set; }
        public AdjustSupplierAmount()
        {

        }

        public AdjustSupplierAmount(int supplierId, string modeOfPayment, DateTime date, double amount, string accountingUnitId)
        {
            SupplierId = supplierId;
            ModeOfPayment = modeOfPayment;
            Date = date;
            Amount = amount;
            AccountingUnitId = accountingUnitId;
        }

        public void Modify(string modeOfPayment, DateTime date, double amount)
        {
            ModeOfPayment = modeOfPayment;
            Date = date;
            Amount = amount;
        }
    }

   
}
