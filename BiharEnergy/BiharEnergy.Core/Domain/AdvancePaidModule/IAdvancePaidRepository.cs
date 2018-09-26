using System.Threading.Tasks;
namespace BiharEnergy.Core.Domain.AdvancePaidModule
{
    public interface IAdvancePaidRepository : IRepositoryBase<AdvancePaid>
    {
        Task<string> GetLastReceiptNumber(string tenantId);
        Task<bool> IsReceiptNumberUnique(string tenantId, string receiptNumber);
    }
}
