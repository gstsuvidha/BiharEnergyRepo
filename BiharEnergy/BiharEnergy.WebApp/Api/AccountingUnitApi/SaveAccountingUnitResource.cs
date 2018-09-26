namespace BiharEnergy.WebApp.Api.AccountingUnitApi
{
    public class SaveAccountingUnitResource
    {
        public string Id { get; set; }
        public string BusinessName { get; set; }
        public string Address { get; set; }
        public string PlaceOfSupply { get; set; }
        public string Gstin { get; set; }
        public string TdsGstin { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public string TurnOver { get; set; }
        public string BankAccountName { get; set; }
        public string BankAccountNumber { get; set; }
        public string IfscCode { get; set; }
        public string RegistrationType { get; set; }
        public string TermsAndCondition { get; set; }
        public bool InventorySelection { get; set; }
        public string CurrentGrossTurnOver { get; set; }
        public int SelectedYear { get; set; }
        public string InvoicePrefix { get; set; }
        public int CompanyId { get; set; }
        public string Pan { get; set; }
    }
}