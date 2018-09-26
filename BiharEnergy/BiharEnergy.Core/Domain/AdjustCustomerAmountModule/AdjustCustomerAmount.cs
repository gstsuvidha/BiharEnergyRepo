using System;
using System.Collections.Generic;
using System.Text;
using BiharEnergy.Core.Domain.Accounting;
using BiharEnergy.Core.Domain.AccountModule;
using BiharEnergy.Core.Domain.CustomerModule;
using BiharEnergy.Core.Domain.Inferface;

namespace BiharEnergy.Core.Domain.AdjustCustomerAmountModule
{
    public class AdjustCustomerAmount : IHaveAccountingUnit
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string ModeOfPayment { get; set; }
        public DateTime Date { get; set; }
        public Account Account { get; set; }
        public int? AccountId { get; set; }
        public double Amount { get; set; }
        public string AccountingUnitId { get; set; }
        public AccountingUnit AccountingUnit { get; set; }
        public Customer Customer { get; set; }

        public AdjustCustomerAmount()
        {

        }

        public AdjustCustomerAmount(int customerId, string modeOfPayment, DateTime date, double amount, string accountingUnitId)
        {
            CustomerId = customerId;
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
