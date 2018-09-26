using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BiharEnergy.Core.Domain.AdvancedReceivedModule;

namespace BiharEnergy.Persistence.QueryFilter.Filters
{
    public static class AdvanceReceiveFilter
    {
        public static IQueryable<AdvanceReceived> IncludeTenant(this IQueryable<AdvanceReceived> data)
        {
            return data.Include(dt => dt.AccountingUnit);
        }
        public static IQueryable<AdvanceReceived> IncludeItems(this IQueryable<AdvanceReceived> data)
        {
            return data.Include(dt => dt.AdvanceReceivedItems);
        }
        public static IQueryable<AdvanceReceived> IncludeItemsAndProducts(this IQueryable<AdvanceReceived> data)
        {
            return data.Include(dt => dt.AdvanceReceivedItems).ThenInclude(it => it.Product);
        }
        public static IQueryable<AdvanceReceived> IncludeCustomer(this IQueryable<AdvanceReceived> data)
        {
            return data.Include(dt => dt.Customer);
        }
        public static Task<AdvanceReceived> GetForPrint(this IQueryable<AdvanceReceived> data, int id)
        {
                return data.IncludeTenant()
                    .IncludeCustomer()
                    .IncludeItemsAndProducts()
                    .SingleOrDefaultAsync(si => si.Id == id);
        }


    }
}
