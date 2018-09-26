using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiharEnergy.Core.Domain.CustomerModule;

namespace BiharEnergy.Core.Domain.Accounting
{
    public interface IAccountingUnitRepository : IRepositoryBase<AccountingUnit>
    {
        //Task<Gstr1> GetGstr1Data(string tenantId, int month);

        Task<IEnumerable<Customer>> GetB2b(string accountingUnitId, int month);
        Task<AccountingUnit> GetAsync(object id);
        Task<AccountingUnit> GetInvoicePrefix(string accountingUnitId);
        Task<IEnumerable<AccountingUnit>> GetAccountingUnitsByCompanyId(int companyId);

    }
}