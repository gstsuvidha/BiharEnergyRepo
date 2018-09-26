using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BiharEnergy.Core.Domain.Accounting;
using BiharEnergy.Core.Domain.CustomerModule;
using Microsoft.EntityFrameworkCore;

namespace BiharEnergy.Persistence.Repository
{
    public class AccountingUnitRepository : RepositoryBase<AccountingUnit>, IAccountingUnitRepository
    {
        private readonly ApplicationDbContext _context;

        public AccountingUnitRepository(ApplicationDbContext context) : base((DbContext)context)
        {
            _context = context;
        }


        //public Task<Gstr1> GetGstr1Data(string tenantId, int month)
        //{
        //    var fromDate = new DateTime(DateTime.Now.Year, month, 1);
        //    var toDate = new DateTime(DateTime.Now.Year, month + 1, 1);


        //    return _context.Tenants

        //                        .Include(c => c.Customers)
        //                            .ThenInclude(si => si.SalesInvoices)
        //                            .ThenInclude(siItem => siItem.SalesInvoiceItems)


        //                           .Include(b2C => b2C.B2CSalesInvoices)
        //                             .ThenInclude(b2CItems => b2CItems.SalesInvoiceItems)



        //                        .Select(t => Gstr1.PopulateGstr1(t.Id,
        //                                                t.LastGrossTurnOver,
        //                                                t.CurrentGrossTurnOver,
        //                                                t.PlaceOfSupply,
        //                                                t.Customers.Where(c => c.SalesInvoices.Any(si => si.Date >= fromDate && si.Date < toDate)),
        //                                                t.B2CSalesInvoices.Where(si => si.Date >= fromDate && si.Date < toDate && si.CustomerId == null)
        //                                                ))
        //                                                 .SingleOrDefaultAsync(t => t.Id == tenantId);


            //return _context.Tenants

            //                .Include(c => c.Customers)
            //                    .ThenInclude(si => si.SalesInvoices)
            //                        .ThenInclude(siItem => siItem.SalesInvoiceItems)

            //                    .Include(b2C => b2C.B2CSalesInvoices)
            //                            .ThenInclude(b2CItems => b2CItems.SalesInvoiceItems)

            //                        .Select(t => new Tenant
            //                        {
            //                            Id = t.Id,
            //                            Gstin = t.Gstin,
            //                            LastGrossTurnOver = t.LastGrossTurnOver,
            //                            CurrentGrossTurnOver = t.CurrentGrossTurnOver,
            //                            Customers = t.Customers.Where(c => c.SalesInvoices.Any(si => si.Date >= fromDate && si.Date < toDate))
            //                            .Select(c => new Customer
            //                            {
            //                                Gstin = c.Gstin,
            //                                SalesInvoices = c.SalesInvoices.Select(si => new SalesInvoice { SalesInvoiceItems = si.SalesInvoiceItems })

            //                            }),
            //                            B2CSalesInvoices = t.B2CSalesInvoices.Where(si => si.Date >= fromDate && si.Date < toDate && si.CustomerId == null)

            //                        })
            //                        .SingleOrDefaultAsync(t => t.Id == tenantId);

        //}

        public async Task<IEnumerable<Customer>> GetB2b(string tenantId, int month)
        {
            var fromDate = new DateTime(DateTime.Now.Year, month, 1);
            var toDate = new DateTime(DateTime.Now.Year, month + 1, 1);


            return await _context.Customers
                                            .Include(si => si.SalesInvoices)
                                            .ThenInclude(siItem => siItem.SalesInvoiceItems)
                                            // .Select(c => new Customer
                                            // {
                                            //    Id = c.Id,
                                            //    TenantId = c.TenantId,
                                            //    Gstin = c.Gstin,
                                            //    SalesInvoices = c.SalesInvoices,


                                            // })
                                            // .Where(c => c.SalesInvoices != null)
                                            .ToListAsync();
        }

        public override Task<AccountingUnit> GetAsync(object id, string accountingUnitId)
        {
            return _context.AccountingUnits.FindAsync(id);
        }
        public Task<AccountingUnit> GetAsync(object id)
        {
            return _context.AccountingUnits.FindAsync(id);
        }

        public override Task<AccountingUnit> GetAllAsync(object id)
        {
            throw new NotImplementedException();
        }

        public Task<AccountingUnit> GetInvoicePrefix(string accountingUnitId)
        {
            var accountingUnit = _context.AccountingUnits.FirstOrDefaultAsync(inv => inv.Id == accountingUnitId);
            return accountingUnit;
        }

        public async  Task<IEnumerable<AccountingUnit>> GetAccountingUnitsByCompanyId(int companyId)
        {
            
              return await _context.AccountingUnits.Where(acc => acc.CompanyId == companyId).ToListAsync();
        
            
           
        }
    }
}