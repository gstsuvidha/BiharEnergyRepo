using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BiharEnergy.Core.Domain.AdvancePaidModule;
using Microsoft.EntityFrameworkCore;

namespace BiharEnergy.Persistence.Repository
{
    public class AdvancePaidRepository:RepositoryBase<AdvancePaid>,IAdvancePaidRepository
    {
        private readonly ApplicationDbContext _context;

        public AdvancePaidRepository(ApplicationDbContext context) : base((DbContext) context)
        {
            _context = context;
        }

        public override Task<AdvancePaid> GetAllAsync(object id)
        {
            throw new NotImplementedException();
        }

        public override Task<AdvancePaid> GetAsync(object id, string accountingUnitId)
        {
            return _context.AdvancePaids.Include(ap => ap.AdvancePaidItems)
                .SingleOrDefaultAsync(nt => nt.Id == (int)id && nt.AccountingUnitId == accountingUnitId);

        }

        public Task<string> GetLastReceiptNumber(string accountingUnitId)
        {
            return _context.AdvancePaids.Where(inv => inv.AccountingUnitId == accountingUnitId).MaxAsync(inv => inv.ReceiptNumber);
        }

        public async Task<bool> IsReceiptNumberUnique(string accountingUnitId, string invoiceNumber)
        {
            var advancePaid =
                await _context.AdvancePaids.SingleOrDefaultAsync(inv => inv.ReceiptNumber == invoiceNumber && inv.AccountingUnitId == accountingUnitId);

            if (advancePaid == null)
                return true;


            return false;
        }
      
    }
}