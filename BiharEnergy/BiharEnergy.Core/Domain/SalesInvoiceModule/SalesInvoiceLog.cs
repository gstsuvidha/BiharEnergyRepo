namespace BiharEnergy.Core.Domain.SalesInvoiceModule
{
    public class SalesInvoiceLog
    {
        public int Id { get; set; }
        public int InvoiceNumber { get; set; }
           public string AccountingUnitId { get; set; }
           public double TotalInvoiceValue { get; set; }

        public SalesInvoiceLog()
        {
            //EF
        }

        public SalesInvoiceLog(int invoiceNumber, double totalInvoiceValue, string accountingUnitId)
        {
            InvoiceNumber = invoiceNumber;
            TotalInvoiceValue = totalInvoiceValue;
            AccountingUnitId = accountingUnitId;
        }



    }
}