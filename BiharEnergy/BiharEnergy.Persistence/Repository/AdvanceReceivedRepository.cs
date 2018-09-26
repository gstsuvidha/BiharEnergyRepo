using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BiharEnergy.Core.Domain.AdvancedReceivedModule;
using Microsoft.EntityFrameworkCore;

namespace BiharEnergy.Persistence.Repository
{
    public class AdvanceReceivedRepository:RepositoryBase<AdvanceReceived>,IAdvanceReceivedRepository
    {
        private readonly ApplicationDbContext _context;

        public AdvanceReceivedRepository(ApplicationDbContext context) : base((DbContext) context)
        {
            _context = context;
        }

        public override Task<AdvanceReceived> GetAllAsync(object id)
        {
            throw new NotImplementedException();
        }

        public override Task<AdvanceReceived> GetAsync(object id, string accountingUnitId)
        {
            return _context.AdvanceReceiveds.Include(ar => ar.AdvanceReceivedItems)
                .SingleOrDefaultAsync(nt => nt.Id == (int)id && nt.AccountingUnitId == accountingUnitId);

        }
        
        public Task<int> GetLastReceiptNumber(string accountingUnitId)
        {
            return _context.AdvanceReceiveds.Where(inv => inv.AccountingUnitId == accountingUnitId).MaxAsync(inv => inv.ReceiptNumber);
        }

        public async Task<bool> IsReceiptNumberUnique(string accountingUnitId, int invoiceNumber)
        {
            var advanceReceived =
                await _context.AdvanceReceiveds.SingleOrDefaultAsync(inv => inv.ReceiptNumber == invoiceNumber && inv.AccountingUnitId == accountingUnitId);

            if (advanceReceived == null)
                return true;


            return false;
        } 
       
    }
}