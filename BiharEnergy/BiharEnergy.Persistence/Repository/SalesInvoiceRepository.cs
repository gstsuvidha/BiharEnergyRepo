using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BiharEnergy.Core.Domain.SalesInvoiceModule;
using BiharEnergy.Persistence.Repository;
using Microsoft.EntityFrameworkCore;

namespace BiharEnergy.Persistence.Repository
{

    public class SalesInvoiceRepository:RepositoryBase<SalesInvoice>,ISalesInvoiceRepository
    {
        private readonly ApplicationDbContext _context;

        public SalesInvoiceRepository(ApplicationDbContext context) : base((DbContext) context)
        {
            _context = context;
        }

        public void AddLog(SalesInvoiceLog log)
        {
            _context.SalesInvoiceLogs.Add(log);
        }

        public override Task<SalesInvoice> GetAllAsync(object id)
        {
            throw new NotImplementedException();
        }

        public override Task<SalesInvoice> GetAsync(object id,string accountingUnitId)
        {
            return _context.SalesInvoices.Include(si => si.SalesInvoiceItems).SingleOrDefaultAsync(si => si.Id == (int)id && si.AccountingUnitId == accountingUnitId);
         
        }

       

        public Task<int> GetLastInvoiceNumber(string accountingUnitId)
        {
            return _context.SalesInvoices.Where(inv => inv.AccountingUnitId == accountingUnitId).DefaultIfEmpty().MaxAsync(inv => inv.InvoiceNumber);
        }

        public async Task<bool> IsInvoiceNumberUnique(string accountingUnitId, int invoiceNumber)
        {
            var salesInvoice =
                await _context.SalesInvoices.SingleOrDefaultAsync(inv => inv.InvoiceNumber == invoiceNumber && inv.AccountingUnitId == accountingUnitId);

            if (salesInvoice == null)
                return true;


            return false;
        }
    }
}