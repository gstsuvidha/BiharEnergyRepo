
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BiharEnergy.Core.Enums;
using BiharEnergy.Persistence.QueryFilter.Filters;
using BiharEnergy.Core.Domain.IssueNoteModule;

namespace BiharEnergy.Persistence.Repository
{
    public class IssueNoteRepository:RepositoryBase<IssueNote>,IIssueNoteRepository
    {
        private readonly ApplicationDbContext _context;

        public IssueNoteRepository(ApplicationDbContext context) : base((DbContext) context)
        {
            _context = context;
        }

        public override Task<IssueNote> GetAsync(object id, string accountingUnitId)
        {
            return _context.IssueNotes.IncludeItems()
                .SingleOrDefaultAsync(nt => nt.Id == (int)id && nt.AccountingUnitId == accountingUnitId);
        }


        public Task<int> GetLastInvoiceNumber(string accountingUnitId)
        {
            return _context.IssueNotes.Where(inv => inv.AccountingUnitId == accountingUnitId).MaxAsync(inv => inv.IssueNoteNumber);
        }

        public async Task<bool> IsInvoiceNumberUnique(string accountingUnitId, int issueNoteNumber)
        {
            var issueNote =
                await _context.IssueNotes.SingleOrDefaultAsync(inv => inv.IssueNoteNumber == issueNoteNumber && inv.AccountingUnitId == accountingUnitId);

            if (issueNote == null)
                return true;


            return false;
        }

        public Task<int> GetLastIssueNoteNumber(string accountingUnitId)
        {
            return _context.IssueNotes.Where(inv => inv.AccountingUnitId == accountingUnitId).MaxAsync(inv => inv.IssueNoteNumber);

        }

        public async Task<bool> IsIssueNoteNumberUnique(string accountingUnitId, int issueNoteNumber)
        {
            var issueNote =
                await _context.IssueNotes.SingleOrDefaultAsync(inv => inv.IssueNoteNumber == issueNoteNumber && inv.AccountingUnitId == accountingUnitId);

            if (issueNote == null)
                return true;


            return false;
        }

        public override Task<IssueNote> GetAllAsync(object id)
        {
            throw new NotImplementedException();
        }
    }
}