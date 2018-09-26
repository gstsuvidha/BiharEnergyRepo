using System;
using System.Threading.Tasks;
using BiharEnergy.Core.Domain.SupplierModule;

using BiharEnergy.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace BiharEnergy.Persistence.Repository
{
    public class SupplierRepository:RepositoryBase<Supplier>,ISupplierRepository
    {

        private readonly ApplicationDbContext _context;

        public SupplierRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public override Task<Supplier> GetAllAsync(object id)
        {
            throw new NotImplementedException();
        }

        public override Task<Supplier> GetAsync(object id, string accountingUnitId)
        {
            return _context.Suppliers.SingleOrDefaultAsync(c => c.Id == (int)id
                                                                && c.AccountingUnitId == accountingUnitId);
        }
    }
}