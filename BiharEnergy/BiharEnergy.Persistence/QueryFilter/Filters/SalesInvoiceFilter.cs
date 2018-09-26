using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiharEnergy.Core.Domain.SalesInvoiceModule;
using Microsoft.EntityFrameworkCore;

namespace BiharEnergy.Persistence.QueryFilter.Filters
{
    public static class SalesInvoiceFilter
    {
        public static IQueryable<SalesInvoice> IncludeTenant(this IQueryable<SalesInvoice> data)
        {
            return data.Include(dt => dt.AccountingUnit);
        }
        public static IQueryable<SalesInvoice> IncludeItems(this IQueryable<SalesInvoice> data)
        {
            return data.Include(dt => dt.SalesInvoiceItems);
        }
        public static IQueryable<SalesInvoice> IncludeItemsAndProducts(this IQueryable<SalesInvoice> data)
        {
            return data.Include(dt => dt.SalesInvoiceItems).ThenInclude(it=> it.Product);
        }
        public static IQueryable<SalesInvoice> IncludeCustomer(this IQueryable<SalesInvoice> data)
        {
            return data.Include(dt => dt.Customer);
        }
        public static Task<SalesInvoice> GetForPrint(this IQueryable<SalesInvoice> data,int id)
        {
            return data.IncludeTenant()
                        .IncludeCustomer()
                        .IncludeItemsAndProducts()
                        .SingleOrDefaultAsync(si => si.Id == id);
        }

    }
}
