using System;
using System.Linq;
using System.Threading.Tasks;

namespace BiharEnergy.Core.Domain.AccountModule
{
  public interface IAccountsRepository : IRepositoryBase<Account>
    {
         Task<string> GetAccountingUnitAccountName(int accountId);
        
    }
}
