using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiharEnergy.Core.Domain.ReceiptNoteModule;
using Microsoft.EntityFrameworkCore;
using BiharEnergy.Core.Enums;

namespace BiharEnergy.Persistence.QueryFilter.Filters
{
    public static class ReceiptNoteFilter
    {
        public static IQueryable<ReceiptNote> IncludeTenant(this IQueryable<ReceiptNote> data)
        {
            return data.Include(dt => dt.AccountingUnit);
        }
        public static IQueryable<ReceiptNote> IncludeItems(this IQueryable<ReceiptNote> data)
        {
            return data.Include(dt => dt.ReceiptNoteItems);
        }
        public static IQueryable<ReceiptNote> IncludeItemsAndProducts(this IQueryable<ReceiptNote> data)
        {
            return data.Include(dt => dt.ReceiptNoteItems).ThenInclude(it => it.Product);
        }
        public static IQueryable<ReceiptNote> IncludeSupplier(this IQueryable<ReceiptNote> data)
        {
            return data.Include(dt => dt.Supplier);

        
        }

          public static IQueryable<ReceiptNote> OfReceiptNoteType(this IQueryable<ReceiptNote> data, NoteType noteType)
        {
            return data.Where(nt => nt.NoteType == noteType);
        }

        public static Task<ReceiptNote> GetForPrint(this IQueryable<ReceiptNote> data, int id)
        {
            return data.IncludeTenant()
                .IncludeSupplier()
                .IncludeItemsAndProducts()
                .SingleOrDefaultAsync(si => si.Id == id);
        }

    }
}
