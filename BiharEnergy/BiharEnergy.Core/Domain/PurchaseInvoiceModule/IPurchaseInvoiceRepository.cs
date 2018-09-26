using System.Threading.Tasks;

namespace BiharEnergy.Core.Domain.PurchaseInvoiceModule
{
    public interface IPurchaseInvoiceRepository : IRepositoryBase<PurchaseInvoice>
    {
        Task<bool> IsInvoiceNumberUnique(string tenantId, string invoiceNumber);

    }
}