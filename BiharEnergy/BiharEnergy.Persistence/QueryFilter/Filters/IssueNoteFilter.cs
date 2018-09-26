using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiharEnergy.Core.Domain.IssueNoteModule;
using BiharEnergy.Core.Enums;
using Microsoft.EntityFrameworkCore;

namespace BiharEnergy.Persistence.QueryFilter.Filters
{
    public static class IssueNoteFilter
    {
        public static IQueryable<IssueNote> IncludeTenant(this IQueryable<IssueNote> data)
        {
            return data.Include(dt => dt.AccountingUnit);
        }
        public static IQueryable<IssueNote> IncludeItems(this IQueryable<IssueNote> data)
        {
            return data.Include(dt => dt.IssueNoteItems);
        }
        public static IQueryable<IssueNote> IncludeItemsAndProducts(this IQueryable<IssueNote> data)
        {
            return data.Include(dt => dt.IssueNoteItems).ThenInclude(it => it.Product);
        }
        public static IQueryable<IssueNote> IncludeCustomer(this IQueryable<IssueNote> data)
        {
            return data.Include(dt => dt.Customer);
        }
         public static IQueryable<IssueNote> OfNoteType(this IQueryable<IssueNote> data, NoteType noteType)
        {
            return data.Where(nt => nt.NoteType == noteType);
        }
        public static Task<IssueNote> GetForPrint(this IQueryable<IssueNote> data, int id)
        {
            return data.IncludeTenant()
                        .IncludeCustomer()
                        .IncludeItemsAndProducts()
                        .SingleOrDefaultAsync(si => si.Id == id);
        }

    }
}
