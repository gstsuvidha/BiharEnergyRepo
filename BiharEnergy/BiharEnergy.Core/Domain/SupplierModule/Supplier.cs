using System;
using System.Collections.Generic;
using System.Text;
using BiharEnergy.Core.Domain.Accounting;
using BiharEnergy.Core.Domain.AdjustSupplierAmountModule;
using BiharEnergy.Core.Domain.AdvancePaidModule;
using BiharEnergy.Core.Domain.Inferface;
using BiharEnergy.Core.Domain.PurchaseInvoiceModule;
using BiharEnergy.Core.Domain.ReceiptNoteModule;
using BiharEnergy.Core.Enums;

namespace BiharEnergy.Core.Domain.SupplierModule
{
    public class Supplier : IHaveAccountingUnit,IHaveActiveFilter
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Gstin { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public RegistrationType RegistrationType { get; set; }
    
        public bool IsActive { get; set; }

        
        //foreign key
        public string AccountingUnitId { get; set; }
        public AccountingUnit AccountingUnit { get; set; }

        public IEnumerable<PurchaseInvoice> PurchaseInvoices { get; set; }
        public IEnumerable<ReceiptNote> ReceiptNotes { get; set; }
        public IEnumerable<AdvancePaid> AdvancePaids { get; set; }
        public IEnumerable<AdjustSupplierAmount> AdjustSupplierAmount { get; set; }


        protected Supplier()
        {

        }

        public Supplier(string name, string gstin, string address, string state, string contactNumber, string accountingUnitId, 
                        RegistrationType registrationType,string email)
        {
            Name = name;
            Gstin = gstin;
            Address = address;
            State = state;
            ContactNumber = contactNumber;
            AccountingUnitId = accountingUnitId;
            IsActive = true;
            RegistrationType = registrationType;
            Email = email;
                     
        }
        

        public void Modify(string name, string gstin, string address, string state, string contactNumber, 
                            RegistrationType registrationType,string email)
        {
            Name = name;
            Gstin = gstin;
            Address = address;
            State = state;
            ContactNumber = contactNumber;
            IsActive = true;
            RegistrationType = registrationType;
            Email = email;
         
        }

      

        public void Delete()
        {
            IsActive = false;
        }

    }
}
