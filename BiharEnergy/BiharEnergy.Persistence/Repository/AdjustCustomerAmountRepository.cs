using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BiharEnergy.Core.Domain.AdjustCustomerAmountModule;

namespace BiharEnergy.Persistence.Repository
{
    public class AdjustCustomerAmountRepository : RepositoryBase<AdjustCustomerAmount>, IAdjustCustomerAmountRepository
    {
        private readonly ApplicationDbContext _context;
        public AdjustCustomerAmountRepository(ApplicationDbContext context) : base((DbContext)context)
        {
            _context = context;
        }

        public override Task<AdjustCustomerAmount> GetAllAsync(object id)
        {
            throw new NotImplementedException();
        }

        public override Task<AdjustCustomerAmount> GetAsync(object id, string accountingUnitId)
        {
            return _context.AdjustCustomerAmount
                .SingleOrDefaultAsync(nt => nt.Id == (int)id && nt.AccountingUnitId == accountingUnitId);

        }
    }
}
