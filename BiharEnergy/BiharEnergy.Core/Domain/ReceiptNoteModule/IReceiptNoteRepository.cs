using System.Threading.Tasks;

namespace BiharEnergy.Core.Domain.ReceiptNoteModule
{
    public interface IReceiptNoteRepository : IRepositoryBase<ReceiptNote>
    {
        Task<string> GetLastReceiptNoteNumber(string accountingUnitId);
        Task<bool> IsReceiptNoteNumberUnique(string accountingUnitId, string receiptNoteNumber);
        Task<ReceiptNote> GetAsync_IncludeAllProperties(int id, string tenantId);
    }
}