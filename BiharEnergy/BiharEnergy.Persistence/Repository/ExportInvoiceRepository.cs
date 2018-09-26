
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BiharEnergy.Core.Domain.ExportInvoiceModule;

namespace BiharEnergy.Persistence.Repository
{

    public class ExportInvoiceRepository : RepositoryBase<ExportInvoice>, IExportInvoiceRepository
    {
        private readonly ApplicationDbContext _context;

        public ExportInvoiceRepository(ApplicationDbContext context) : base((DbContext)context)
        {
            _context = context;
        }

        public override Task<ExportInvoice> GetAllAsync(object id)
        {
            throw new NotImplementedException();
        }

        public override Task<ExportInvoice> GetAsync(object id, string accountingUnitId)
        {
            return _context.ExportInvoices
                .SingleOrDefaultAsync(nt => nt.Id == (int)id && nt.AccountingUnitId == accountingUnitId);

        }

        public Task<int> GetLastInvoiceNumber(string accountingUnitId)
        {
            return _context.ExportInvoices.Where(inv => inv.AccountingUnitId == accountingUnitId).MaxAsync(inv => inv.InvoiceNumber);
        }

        public async Task<bool> IsExportInvoiceNumberUnique(string accountingUnitId, int invoiceNumber)
        {
            var exportInvoice =
                await _context.ExportInvoices.SingleOrDefaultAsync(inv => inv.InvoiceNumber == invoiceNumber && inv.AccountingUnitId == accountingUnitId);

            if (exportInvoice == null)
                return true;


            return false;
        }        
    }
}