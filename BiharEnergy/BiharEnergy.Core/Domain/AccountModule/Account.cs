using System.Collections.Generic;
using BiharEnergy.Core.Domain.Accounting;
using BiharEnergy.Core.Domain.AdjustCustomerAmountModule;
using BiharEnergy.Core.Domain.AdjustSupplierAmountModule;
using BiharEnergy.Core.Domain.AdvancedReceivedModule;
using BiharEnergy.Core.Domain.AdvancePaidModule;
using BiharEnergy.Core.Domain.Inferface;
using BiharEnergy.Core.Domain.IssueNoteModule;
using BiharEnergy.Core.Domain.PurchaseInvoiceModule;
using BiharEnergy.Core.Domain.ReceiptNoteModule;
using BiharEnergy.Core.Domain.SalesInvoiceModule;

namespace BiharEnergy.Core.Domain.AccountModule
{
    public class Account : IHaveAccountingUnit, IHaveActiveFilter
    {
        public int Id { get; set; }
        public string AccountingUnitId { get; set; }
        public AccountingUnit AccountingUnit { get; set; }
        public string Nature { get; set; }
        public string AccountName { get; set; }
        public string Description { get; set; }
        public double OpeningBalance { get; set; }
        public bool IsActive { get; set; }

        public IEnumerable<SalesInvoice> SalesInvoices {get; set;}
        public IEnumerable<PurchaseInvoice> PurchaseInvoices {get; set;}
        public IEnumerable<IssueNote> IssueNotes  {get; set;}
        public IEnumerable<ReceiptNote> ReceiptNotes {get; set;}
        public IEnumerable<AdvanceReceived> AdvanceReceiveds {get; set;}
        public IEnumerable<AdvancePaid> AdvancePaids {get; set; }
        public IEnumerable<AdjustCustomerAmount> AdjustCustomerAmounts { get; set; }
        public IEnumerable<AdjustSupplierAmount> AdjustSupplierAmounts { get; set; }




    protected Account()
        {
            //EF
        }
        public Account(string accountName, string description, string nature, double openingBalance, string accountingUnitId)
        {
            AccountName = accountName;
            Nature = nature;
            Description = description;
            OpeningBalance = openingBalance;
            AccountingUnitId = accountingUnitId;
            IsActive = true;
        }
        public void Modify(string accountName, string description, string nature, double openingBalance)
        {

            AccountName = accountName;
            Nature = nature;
            Description = description;
            OpeningBalance = openingBalance;
            IsActive = true;
        }
        public void Delete()
        {
            IsActive = false;
        }


    }
}
