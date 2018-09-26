using System.Threading.Tasks;

namespace BiharEnergy.Core.Domain.SalesInvoiceModule
{
    public interface ISalesInvoiceRepository : IRepositoryBase<SalesInvoice>
    {
        Task<int> GetLastInvoiceNumber(string accountingUnitId);
        Task<bool> IsInvoiceNumberUnique(string accountingUnitId, int invoiceNumber);
        
        void AddLog(SalesInvoiceLog log);
    }
}