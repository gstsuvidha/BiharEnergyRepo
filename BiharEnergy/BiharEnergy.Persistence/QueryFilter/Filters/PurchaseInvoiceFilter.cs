using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiharEnergy.Core.Domain.PurchaseInvoiceModule;
using Microsoft.EntityFrameworkCore;

namespace BiharEnergy.Persistence.QueryFilter.Filters
{
    public static class PurchaseInvoiceFilter
    {
        public static IQueryable<PurchaseInvoice> IncludeTenant(this IQueryable<PurchaseInvoice> data)
        {
            return data.Include(dt => dt.AccountingUnit);
        }
        public static IQueryable<PurchaseInvoice> IncludeSupplier(this IQueryable<PurchaseInvoice> data)
        {
            return data.Include(dt => dt.Supplier);
        }
        public static IQueryable<PurchaseInvoice> IncludeItems(this IQueryable<PurchaseInvoice> data)
        {
            return data.Include(dt => dt.PurchaseInvoiceItems);
        }
        public static IQueryable<PurchaseInvoice> IncludeItemsAndProducts(this IQueryable<PurchaseInvoice> data)
        {
            return data.Include(dt => dt.PurchaseInvoiceItems).ThenInclude(it=> it.Product);
        }
        public static IQueryable<PurchaseInvoice> IncludePurchase(this IQueryable<PurchaseInvoice> data)
        {
            return data.Include(dt => dt.PurchaseInvoiceItems);
        }
        public static Task<PurchaseInvoice> GetForPrint(this IQueryable<PurchaseInvoice> data,int id)
        {
            return data.IncludeTenant()
                        .IncludePurchase()
                        .IncludeItemsAndProducts()
                        .SingleOrDefaultAsync(si => si.Id == id);
        }

    }
}
