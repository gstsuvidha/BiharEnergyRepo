using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BiharEnergy.Core.Domain.AdjustSupplierAmountModule;

namespace BiharEnergy.Persistence.Repository
{
   public class AdjustSupplierAmountRepository :RepositoryBase<AdjustSupplierAmount> , IAdjustSupplierAmountRepository
    {
        private readonly ApplicationDbContext _context;

        public AdjustSupplierAmountRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public override Task<AdjustSupplierAmount> GetAllAsync(object id)
        {
            throw new NotImplementedException();
        }

        public override Task<AdjustSupplierAmount> GetAsync(object id, string accountingUnitId)
        {
            return _context.AdjustSupplierAmount
                .SingleOrDefaultAsync(nt => nt.Id == (int)id && nt.AccountingUnitId == accountingUnitId);

        }
    }
}
