using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BiharEnergy.Core.Domain.ProductModule;
using Microsoft.EntityFrameworkCore;

namespace BiharEnergy.Persistence.Repository
{
    public class ProductRepository:RepositoryBase<Product>,IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public override Task<Product> GetAllAsync(object id)
        {
            throw new NotImplementedException();
        }

        public override Task<Product> GetAsync(object id, string accountingUnitId)
        {
            return _context.Products.SingleOrDefaultAsync(pr => pr.Id == (int) id 
                                                             && pr.AccountingUnitId == accountingUnitId);
        }
    }
}
