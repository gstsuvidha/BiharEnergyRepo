using BiharEnergy.Core.Domain.CompanyModule;
using BiharEnergy.Core.Domain.CustomerModule;
using BiharEnergy.Core.Domain.ProductModule;
using BiharEnergy.Core.Domain.SalesInvoiceModule;
using BiharEnergy.Core.Domain.SupplierModule;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BiharEnergy.Core.Domain.Accounting
{
    public class AccountingUnit
    {
        [Key]
        public string Id { get; set; }

        public string Name { get; set; }
        public string BusinessName { get; set; }
        public string Subject { get; set; }
        public string GstinPassword { get; set; }
        public string AuthorizedRepresentativeName { get; set; }
        public string Contact { get; set; }
        public string LastGrossTurnOver { get; set; }
        public string CurrentGrossTurnOver { get; set; }
        public string Pan { get; set; }

        public string Address { get; set; }
        public string Gstin { get; set; }
        public string TdsGstin { get; set; }
        public string ContactNumber { get; set; }
        public string TurnOver { get; set; }
        public string IfscCode { get; set; }
        public string RegistrationType { get; set; }
        public string TermsAndCondition { get; set; }
        public string BankAccountName { get; set; }
        public string BankAccountNumber { get; set; }
        public string Email { get; set; }
        
        
        public string PlaceOfSupply { get; set; }

        public string ImgUrl { get; set; }
        public bool InventorySelection { get; set; }

        public int SelectedYear { get; set; }
        public string InvoicePrefix { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        



        #region Getters
        public IEnumerable<Customer> Customers { get; set; }
        public IEnumerable<Supplier> Suppliers { get; set; }
        public IEnumerable<Product> Products { get; set; }

        public IEnumerable<SalesInvoice> B2CSalesInvoices { get; set; }
        //public IEnumerable<IssueNote> UnregisteredIssueNotes { get; set; }


        //public IEnumerable<PurchaseInvoice> B2CPurchaseInvoices { get; set; }



        #endregion

        public AccountingUnit()
        {
            Customers = new List<Customer>();
            Suppliers = new List<Supplier>();
            Products = new List<Product>();

            B2CSalesInvoices = new List<SalesInvoice>();

            //IssueNotes = new List<IssueNote>();
            //AdvanceReceiveds = new List<AdvanceReceived>();

            //PurchaseInvoices =new List<PurchaseInvoice>();
            //ReceiptNotes=new List<ReceiptNote>();
            //AdvancePaids=new List<AdvancePaid>();
        }
        public AccountingUnit(string businessName, string placeOfSupply, string gstin, string tdsGstin,
                                string email, string address, string contactNumber,
                            string turnOver, string bankAccountName, string bankAccountNumber, 
                            string ifscCode, string registrationType,
                            string termsAndCondition, bool inventorySelection, 
                            string currentGrossTurnOver, int selectedYear,string invoicePrefix,string pan)
        {
            BusinessName = businessName;
            PlaceOfSupply = placeOfSupply;
            Gstin = gstin;
            TdsGstin = tdsGstin;
            Email = email;
            Address = address;
            ContactNumber = contactNumber;
            TurnOver = turnOver;
            BankAccountName = bankAccountName;
            BankAccountNumber = bankAccountNumber;
            IfscCode = ifscCode;
            RegistrationType = registrationType;
            TermsAndCondition = termsAndCondition;
            InventorySelection = inventorySelection;
            CurrentGrossTurnOver = currentGrossTurnOver;
            SelectedYear = selectedYear;
            InvoicePrefix = invoicePrefix;
            Pan = pan;
        }

        public void Modify(string businessName, string placeOfSupply, string gstin, string tdsGstin, string email, string address, string contactNumber,
                            string turnOver, string bankAccountName, string bankAccountNumber, string ifscCode, string registrationType,
                            string termsAndCondition, bool inventorySelection, string currentGrossTurnOver, int selectedYear,string invoicePrefix,int companyId,string pan)
        {

            BusinessName = businessName;
            PlaceOfSupply = placeOfSupply;
            Gstin = gstin;
            TdsGstin = tdsGstin;
            Email = email;
            Address = address;
            ContactNumber = contactNumber;
            TurnOver = turnOver;
            BankAccountName = bankAccountName;
            BankAccountNumber = bankAccountNumber;
            IfscCode = ifscCode;
            RegistrationType = registrationType;
            TermsAndCondition = termsAndCondition;
            InventorySelection = inventorySelection;
            CurrentGrossTurnOver = currentGrossTurnOver;
            SelectedYear = selectedYear;
            InvoicePrefix = invoicePrefix;
            CompanyId = companyId;
            Pan = pan;
        }
        public void UpdateImageUrl(string imgUrl)
        {
            ImgUrl = imgUrl;
        }
    }
}
