using System;
using System.Linq;
using System.Threading.Tasks;
using BiharEnergy.Core.Domain.ReceiptNoteModule;
using Microsoft.EntityFrameworkCore;

namespace BiharEnergy.Persistence.Repository
{

    public class ReceiptNoteRepository : RepositoryBase<ReceiptNote>, IReceiptNoteRepository
    {
        private readonly ApplicationDbContext _context;

        public ReceiptNoteRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public override Task<ReceiptNote> GetAsync(object id, string accountingUnitId)
        {
            return _context.ReceiptNotes
                            .Include(si => si.ReceiptNoteItems)
                            .SingleOrDefaultAsync(nt => nt.Id== (int)id && nt.AccountingUnitId == accountingUnitId);
        }




        
        public Task<string> GetLastReceiptNoteNumber(string accountingUnitId)
        {
            return _context.ReceiptNotes.Where(inv => inv.AccountingUnitId == accountingUnitId).MaxAsync(inv => inv.ReceiptNoteNumber);

        }

        public async Task<bool> IsReceiptNoteNumberUnique(string accountingUnitId, string receiptNoteNumber)
        {
            var receiptNote =
                await _context.ReceiptNotes.SingleOrDefaultAsync(inv => inv.ReceiptNoteNumber == receiptNoteNumber && inv.AccountingUnitId == accountingUnitId);

            if (receiptNote == null)
                return true;


            return false;
        }
        public Task<ReceiptNote> GetAsync_IncludeAllProperties(int id, string accountingUnitId)
        {
            return _context.ReceiptNotes.Include(t => t.AccountingUnit)
                                                .Include(si => si.Supplier)
                                                .Include(si => si.ReceiptNoteItems)
                                                .ThenInclude(it => it.Product)
                                                .SingleOrDefaultAsync(si => si.Id == id && si.AccountingUnitId == accountingUnitId);

        }

        public override Task<ReceiptNote> GetAllAsync(object id)
        {
            throw new NotImplementedException();
        }
    }
}