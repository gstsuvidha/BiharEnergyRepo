using System;
using System.Threading.Tasks;
using BiharEnergy.Core.Domain.TdsModule;
using Microsoft.EntityFrameworkCore;

namespace BiharEnergy.Persistence.Repository
{
    public class TdsRepository:RepositoryBase<Tds>,ITdsRepository
    {

        private readonly ApplicationDbContext _context;

        public TdsRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public override Task<Tds> GetAllAsync(object id)
        {
            throw new NotImplementedException();
        }

        public override Task<Tds> GetAsync(object id, string accountingUnitId)
        {
            return _context.Tdss.SingleOrDefaultAsync(c => c.Id == (int)id
                                                                && c.AccountingUnitId == accountingUnitId);
        }
    }
}