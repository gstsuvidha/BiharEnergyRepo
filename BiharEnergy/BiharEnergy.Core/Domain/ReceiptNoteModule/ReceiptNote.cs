using System;
using System.Collections.Generic;
using System.Text;
using BiharEnergy.Core.Domain.Accounting;
using BiharEnergy.Core.Domain.AccountModule;
using BiharEnergy.Core.Domain.Inferface;
using BiharEnergy.Core.Domain.SupplierModule;
using BiharEnergy.Core.Enums;

namespace BiharEnergy.Core.Domain.ReceiptNoteModule
{
   public  class ReceiptNote:IHaveAccountingUnit,IHaveDateFilter,IHaveInvoiceCategory
    {
        public int Id { get; set; }

        public string ReceiptNoteNumber { get; set; }

        public DateTime Date { get; set; }

        public NoteType NoteType { get; set; }
        
        public string PlaceOfSupply { get; private set; }

        public SupplyType SupplyType { get; set; }

        public string PurchaseInvoiceNumber { get; set; }

        public int? SupplierId { get; set; }
        public int? AccountId { get; set; }

        public DateTime PurchaseInvoiceDate { get; set; }

        public double PurchaseInvoiceValue { get; set; }

        public InvoiceCategory InvoiceCategory { get; set; }

        public string Pgst { get; set; }

        public string Reason { get; set; }

        public double TotalTaxableValue { get; set; }

        public double TotalTaxAmount { get; set; }
        
        public double TotalCessAmount { get; set; }

        public double TotalNoteValue { get; set; }

        public string Refrence { get; set; }

        public double PaidAmount { get; set; }

        public DateTime InventoryDate { get; set; }

        public string ModeOfPayment { get; set; }

        public ICollection<ReceiptNoteItem> ReceiptNoteItems { get; set; }

        public Account Account { get; set; }
        public Supplier Supplier { get; set; }

        public string AccountingUnitId { get; set; }

        public AccountingUnit AccountingUnit { get; set; }

        public ReceiptNote()
        {

        }
      

        public ReceiptNote(string receiptNoteNumber, DateTime date, NoteType noteType, string placeOfSupply,
            string purchaseInvoiceNumber, int? supplierId, DateTime purchaseInvoiceDate,
            double purchaseInvoiceValue, InvoiceCategory invoiceCategory, string pgst,
            string reason, double totalTaxableValue, double totalTaxAmount, double totalCessAmount, 
            double totalNoteValue, string refrence,double paidAmount, string modeOfPayment, int? accountId, DateTime inventoryDate,
            ICollection<ReceiptNoteItem> receiptNoteItems, string accountingUnitId)
        {
            ReceiptNoteNumber = receiptNoteNumber;
            Date = date;
            NoteType = noteType;
            PurchaseInvoiceNumber = purchaseInvoiceNumber;
            PlaceOfSupply = placeOfSupply;
            SupplierId = supplierId;
            PurchaseInvoiceDate = purchaseInvoiceDate;
            PurchaseInvoiceValue = purchaseInvoiceValue;
            InvoiceCategory = invoiceCategory;
            Pgst = pgst;
            Reason = reason;
            TotalTaxableValue = totalTaxableValue;
            TotalTaxAmount = totalTaxAmount;
            TotalCessAmount = totalCessAmount;
            TotalNoteValue = totalNoteValue;
            ReceiptNoteItems = receiptNoteItems;
            AccountingUnitId = accountingUnitId;
            Refrence = refrence;
            ModeOfPayment = modeOfPayment;
            AccountId = accountId;
            InventoryDate = inventoryDate;
            PaidAmount = paidAmount;
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


                                   

            // SupplyType = PlaceOfSupply == Tenant.PlaceOfSupply
            //     ? SupplyType.IntraState
            //     : SupplyType.InterState;

        

        public void Modify(DateTime date, NoteType noteType,
            string purchaseInvoiceNumber, string placeOfSupply, int? supplierId, DateTime purchaseInvoiceDate,
            double purchaseInvoiceValue, InvoiceCategory invoiceCategory, string pgst,
            string reason, double totalTaxableValue, double totalTaxAmount, double totalCessAmount,
             double totalNoteValue, string refrence,double paidAmount, string modeOfPayment, int? accountId, DateTime inventoryDate,
            ICollection<ReceiptNoteItem> receiptNoteItems)
        {
            Date = date;
            NoteType = noteType;
            PurchaseInvoiceNumber = purchaseInvoiceNumber;
            PlaceOfSupply = placeOfSupply;
            SupplierId = supplierId;
            PurchaseInvoiceDate = purchaseInvoiceDate;
            PurchaseInvoiceValue = purchaseInvoiceValue;
            InvoiceCategory = invoiceCategory;
            Pgst = pgst;
            Reason = reason;
            TotalTaxableValue = totalTaxableValue;
            TotalTaxAmount = totalTaxAmount;
            TotalCessAmount = totalCessAmount;
            TotalNoteValue = totalNoteValue;
            Refrence = refrence;
            PaidAmount = paidAmount;
            ModeOfPayment = modeOfPayment;
            AccountId = accountId;
            InventoryDate = inventoryDate;
            ReceiptNoteItems = receiptNoteItems;
            CalculatePlaceOfSupply();
        }
    }
}
