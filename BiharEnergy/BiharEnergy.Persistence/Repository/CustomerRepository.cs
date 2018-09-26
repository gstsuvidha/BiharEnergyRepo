using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BiharEnergy.Core.Domain;
using BiharEnergy.Core.Domain.CustomerModule;
using Microsoft.EntityFrameworkCore;

namespace BiharEnergy.Persistence.Repository
{
    public class CustomerRepository:RepositoryBase<Customer>,ICustomerRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public override Task<Customer> GetAllAsync(object id)
        {
            throw new NotImplementedException();
        }

        public override Task<Customer> GetAsync(object id, string accountingUnitId)
        {
            return _context.Customers.SingleOrDefaultAsync(c => c.Id == (int)id 
                                                                && c.AccountingUnitId == accountingUnitId);
        }
     }
}