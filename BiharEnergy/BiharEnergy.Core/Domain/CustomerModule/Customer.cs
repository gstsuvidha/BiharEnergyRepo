using System;
using System.Collections.Generic;
using System.Text;
using BiharEnergy.Core.Domain.Inferface;
using BiharEnergy.Core.Enums;
using BiharEnergy.Core.Domain.Accounting;
using BiharEnergy.Core.Domain.SalesInvoiceModule;
using BiharEnergy.Core.Domain.IssueNoteModule;
using BiharEnergy.Core.Domain.AdvancedReceivedModule;
using BiharEnergy.Core.Domain.AdjustCustomerAmountModule;

namespace BiharEnergy.Core.Domain.CustomerModule
{
    public class Customer : IHaveActiveFilter, IHaveAccountingUnit
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gstin { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public RegistrationType RegistrationType { get; set; }
        public bool IsActive { get; set; }

        // foreign key
        public string AccountingUnitId { get; set; }
        public AccountingUnit AccountingUnit { get; set; }



        public IEnumerable<SalesInvoice> SalesInvoices { get; set; }
        public IEnumerable<IssueNote> IssueNotes { get; set; }
        public IEnumerable<AdvanceReceived> AdvanceReceiveds { get; set; }
        public IEnumerable<AdjustCustomerAmount> AdjustCustomerAmount { get; set; }

        public Customer()
        {
            //EF
            SalesInvoices = new List<SalesInvoice>();
        }
        public Customer(string name, string gstin, string address, string accountingUnitId,
                        string state, string contactNumber, RegistrationType registrationType,string email
                        )
        {
            Name = name;
            Gstin = gstin;
            Address = address;
            State = state;
            ContactNumber = contactNumber;
            AccountingUnitId = accountingUnitId;
            RegistrationType = registrationType;
            IsActive = true;
            Email = email;
        }


        public void Modify(string name, string gstin, string address,
            string state, string contactNumber, RegistrationType registrationType,string email
            )
        {
            Name = name;
            Gstin = gstin;
            Address = address;
            State = state;
            ContactNumber = contactNumber;
            RegistrationType = registrationType;
            IsActive = true;
            Email = email;
        }

        public void Delete()
        {
            IsActive = false;
        }


    }
}
