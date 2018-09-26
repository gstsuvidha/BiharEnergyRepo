using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiharEnergy.Core.Domain.CustomerModule;
using BiharEnergy.Core.Domain.Accounting;
using Microsoft.EntityFrameworkCore;

namespace BiharEnergy.Persistence.QueryFilter.Filters
{
    public static class CustomerFilter
    {
        public static IQueryable<Customer> IncludeTenant(this IQueryable<Customer> customers)
        {
            return customers.Include(dt => dt.AccountingUnit);
        }
        public static Task<Customer> FindById(this IQueryable<Customer> customers,int? id)
        {
            return customers.SingleOrDefaultAsync(dt => dt.Id == id);
        }


    }
}
