using System.Threading.Tasks;

namespace BiharEnergy.Core.Domain.AdvancedReceivedModule
{
    public interface IAdvanceReceivedRepository : IRepositoryBase<AdvanceReceived>
    {
        Task<int> GetLastReceiptNumber(string tenantId);
        Task<bool> IsReceiptNumberUnique(string tenantId, int receiptNumber);
    }
}