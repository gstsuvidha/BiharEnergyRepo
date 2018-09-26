using System.Collections.Generic;
using System.Threading.Tasks;
using BiharEnergy.Core.Domain.Accounting;
using BiharEnergy.Core.Domain.CustomerModule;

namespace BiharEnergy.Core.Domain.Reporting
{
    public interface ITenantRepository : IRepositoryBase<AccountingUnit>
    {
        //Task<Gstr1> GetGstr1Data(string tenantId, int month);

        Task<IEnumerable<Customer>> GetB2b(string accountingUnitId, int month);
        Task<AccountingUnit> GetAsync(object id);

    }
}