using System;
using System.Threading.Tasks;
using BiharEnergy.Core.Domain.InventoryModule;
using Microsoft.EntityFrameworkCore;

namespace BiharEnergy.Persistence.Repository
{
     
    public class InventoryRepository : RepositoryBase<Inventory>, IInventoryRepository
    {
        private readonly ApplicationDbContext _context;

        public InventoryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public override Task<Inventory> GetAllAsync(object id)
        {
            throw new NotImplementedException();
        }

        public override Task<Inventory> GetAsync(object id, string accountingUnitId)
        {
            return _context.Inventory
                .SingleOrDefaultAsync(nt => nt.Id == (int)id && nt.AccountingUnitId == accountingUnitId);

    }
}
}
