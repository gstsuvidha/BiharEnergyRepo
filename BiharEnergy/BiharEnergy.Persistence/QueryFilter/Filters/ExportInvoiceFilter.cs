using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiharEnergy.Core.Domain.ExportInvoiceModule;
using Microsoft.EntityFrameworkCore;

namespace BiharEnergy.Persistence.QueryFilter.Filters
{
    public static class ExportInvoiceFilter
    {
        public static IQueryable<ExportInvoice> IncludeTenant(this IQueryable<ExportInvoice> data)
        {
            return data.Include(dt => dt.AccountingUnit);
        }
        public static IQueryable<ExportInvoice> IncludeItems(this IQueryable<ExportInvoice> data)
        {
            return data.Include(dt => dt.ExportInvoiceItems);
        }
        public static IQueryable<ExportInvoice> IncludeItemsAndProducts(this IQueryable<ExportInvoice> data)
        {
            return data.Include(dt => dt.ExportInvoiceItems).ThenInclude(it => it.Product);
        }
        public static Task<ExportInvoice> GetForPrint(this IQueryable<ExportInvoice> data, int id)
        {
            return data.IncludeTenant()
                .IncludeItemsAndProducts()
                .SingleOrDefaultAsync(si => si.Id == id);
        }

    }
}
