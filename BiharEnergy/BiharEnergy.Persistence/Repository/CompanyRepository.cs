using BiharEnergy.Core.Domain.CompanyModule;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace BiharEnergy.Persistence.Repository
{


    public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
    {
        private readonly ApplicationDbContext _context;

        public CompanyRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public override Task<Company> GetAllAsync(object id)
        {
            throw new NotImplementedException();
        }

        public override Task<Company> GetAsync(object id, string accountingUnitId)
        {
            return _context.Companies.SingleOrDefaultAsync(c => c.Id == (int)id);
        }
    }
}