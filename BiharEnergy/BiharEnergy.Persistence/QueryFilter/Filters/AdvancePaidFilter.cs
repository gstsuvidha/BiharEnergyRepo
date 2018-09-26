using BiharEnergy.Core.Domain.AdvancePaidModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BiharEnergy.Persistence.QueryFilter.Filters
{
    public static class AdvancePaidFilter
    {
        public static IQueryable<AdvancePaid> IncludeTenant(this IQueryable<AdvancePaid> data)
        {
            return data.Include(dt => dt.AccountingUnit);
        }
        public static IQueryable<AdvancePaid> IncludeItems(this IQueryable<AdvancePaid> data)
        {
            return data.Include(dt => dt.AdvancePaidItems);
        }
        public static IQueryable<AdvancePaid> IncludeItemsAndProducts(this IQueryable<AdvancePaid> data)
        {
            return data.Include(dt => dt.AdvancePaidItems).ThenInclude(it => it.Product);
        }
        public static IQueryable<AdvancePaid> IncludeSupplier(this IQueryable<AdvancePaid> data)
        {
            return data.Include(dt => dt.Supplier);
        }
        public static Task<AdvancePaid> GetForPrint(this IQueryable<AdvancePaid> data, int id)
        {
                return data.IncludeTenant()
                    .IncludeSupplier()
                    .IncludeItemsAndProducts()
                    .SingleOrDefaultAsync(si => si.Id == id);
        }


    }
}
