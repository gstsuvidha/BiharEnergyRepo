
using System.Threading.Tasks;

namespace BiharEnergy.Core.Domain.ExportInvoiceModule
{
    public interface IExportInvoiceRepository : IRepositoryBase<ExportInvoice>
    {

        Task<int> GetLastInvoiceNumber(string accountingUnitId);
        Task<bool> IsExportInvoiceNumberUnique(string accountingUnitId, int invoiceNumber);
    }
}
