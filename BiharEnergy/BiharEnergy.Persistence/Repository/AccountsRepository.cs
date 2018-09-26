using System;
using System.Linq;
using System.Threading.Tasks;
using BiharEnergy.Core.Domain.AccountModule;
using BiharEnergy.Core.Domain.CustomerModule;
using Microsoft.EntityFrameworkCore;

namespace BiharEnergy.Persistence.Repository
{
    public class AccountsRepository : RepositoryBase<Account>, IAccountsRepository
    {
        private readonly ApplicationDbContext _context;

        public Func<IQueryable<Customer>, IQueryable<Customer>> IncludeAccountingUnit => cust => cust.Include(c => c.AccountingUnit);

        public AccountsRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public override Task<Account> GetAsync(object id, string accountingUnitId)
        {
            return _context.Account
                .SingleOrDefaultAsync(nt => nt.Id == (int)id && nt.AccountingUnitId == accountingUnitId);

        }

        public async Task<string> GetAccountingUnitAccountName(int accountId)
        {
            var account = await _context.Account.SingleOrDefaultAsync(acc => acc.Id == accountId);

            return account.AccountName;
        }

        public override Task<Account> GetAllAsync(object id)
        {
            throw new NotImplementedException();
        }
    }
}
