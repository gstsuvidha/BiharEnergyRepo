using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BiharEnergy.Core.Domain.Accounting;
using BiharEnergy.Core.Domain.AccountModule;
using BiharEnergy.Core.Domain.AdvancedReceivedModule;
using BiharEnergy.Core.Domain.CustomerModule;
using BiharEnergy.Core.Domain.Inferface;
using BiharEnergy.Core.Domain.SalesInvoiceModule;
using BiharEnergy.Core.Enums;

namespace BiharEnergy.Core.Domain.IssueNoteModule
{
    public class IssueNote : IHaveDateFilter, IHaveAccountingUnit, IHaveInvoiceCategory
    {
        public int Id { get; set; }

        public int IssueNoteNumber { get; set; }


        public DateTime Date { get; set; }

        public NoteType NoteType { get; set; }

        public SupplyType SupplyType { get; set; }

        public OutwardInvoiceType OutwardInvoiceType { get; set; }



        public int? CustomerId { get; private set; }
        public int OriginalInvoiceNumber { get; private set; }
        public DateTime OriginalInvoiceDate { get; private set; }
        public double OriginalInvoiceValue { get; private set; }

        public string PlaceOfSupply { get; set; }


        public InvoiceCategory InvoiceCategory { get; private set; }


        public string Pgst { get; set; }
        public string Reason { get; set; }

        public double TotalTaxableValue { get; set; }

        public double TotalTaxAmount { get; set; }
        
        public double TotalCessAmount { get; set; }

        public double TotalNoteValue { get; set; }

        public string Refrence { get; set; }



        public ICollection<IssueNoteItem> IssueNoteItems { get; set; }

        public Customer Customer { get; set; }

        public string AccountingUnitId { get; set; }

        public AccountingUnit AccountingUnit { get; set; }



        protected IssueNote()
        {
            //EF
        }

        public IssueNote(int issueNoteNumber, DateTime date,
                         NoteType noteType,
                        OutwardInvoiceType outwardInvoiceType,
                        string pgst, string reason, 
                        double totalTaxableValue, double totalTaxAmount, double totalCessAmount,
                        double totalNoteValue, string refrence,
        
                        ICollection<IssueNoteItem> issueNoteItems, string accountingUnitId)
        {
            IssueNoteNumber = issueNoteNumber;
            Date = date;
            NoteType = noteType;
            Pgst = pgst;
            Reason = reason;
            Refrence = refrence;
            OutwardInvoiceType = outwardInvoiceType;
            TotalTaxableValue = totalTaxableValue;
            TotalTaxAmount = totalTaxAmount;
            TotalCessAmount = totalCessAmount;
            TotalNoteValue = totalNoteValue;
            IssueNoteItems = issueNoteItems;
            AccountingUnitId = accountingUnitId;
        }

        public void UpdateOriginalInvoiceValues(SalesInvoice salesInvoice)
        {
                if (salesInvoice == null)
                    throw new ArgumentNullException(nameof(salesInvoice));

                SupplyType = salesInvoice.SupplyType;
                OriginalInvoiceNumber = salesInvoice.InvoiceNumber;
                CustomerId = salesInvoice.CustomerId;
                OriginalInvoiceDate = salesInvoice.Date;
                OriginalInvoiceValue = salesInvoice.TotalInvoiceValue;
                InvoiceCategory = salesInvoice.InvoiceCategory;
                PlaceOfSupply = salesInvoice.PlaceOfSupply;



            UpdateTaxCalculation();
        }
        public void UpdateOriginalInvoiceValues(AdvanceReceived advanceReceived)
        {
                if (advanceReceived == null)
                    throw new ArgumentNullException(nameof(advanceReceived));

                SupplyType = advanceReceived.SupplyType;
                OriginalInvoiceNumber = advanceReceived.ReceiptNumber;
                CustomerId = advanceReceived.CustomerId;
                OriginalInvoiceDate = advanceReceived.Date;
                OriginalInvoiceValue = advanceReceived.TotalAdvanceReceive;
                InvoiceCategory = advanceReceived.InvoiceCategory;
                PlaceOfSupply = advanceReceived.PlaceOfSupply;


            UpdateTaxCalculation();
        }

        public void Modify(DateTime date, 
                            NoteType noteType, OutwardInvoiceType outwardInvoiceType,
                            string pgst, string reason,
                            double totalTaxableValue, double totalTaxAmount, double totalCessAmount,
                            double totalNoteValue, string refrence,
             
                            ICollection<IssueNoteItem> issueNoteItems)
        {
            Date = date;
            NoteType = noteType;
            Pgst = pgst;
            Reason = reason;
            OutwardInvoiceType = outwardInvoiceType;
            TotalTaxableValue = totalTaxableValue;
            Refrence = refrence;
            TotalTaxAmount = totalTaxAmount;
            TotalCessAmount = totalCessAmount;
            TotalNoteValue = totalNoteValue;

            IssueNoteItems.Clear();
            IssueNoteItems = issueNoteItems;
        }

        private void UpdateTaxCalculation()
        {
            foreach (var item in IssueNoteItems)
            {
                item.CalculateTax(SupplyType);
            }

        }


    }
}
