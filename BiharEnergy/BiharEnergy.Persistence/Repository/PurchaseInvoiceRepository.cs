using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BiharEnergy.Core.Domain.PurchaseInvoiceModule;

using Microsoft.EntityFrameworkCore;

namespace BiharEnergy.Persistence.Repository
{
    public class PurchaseInvoiceRepository : RepositoryBase<PurchaseInvoice>, IPurchaseInvoiceRepository
    {
        private readonly ApplicationDbContext _context;

        public PurchaseInvoiceRepository(ApplicationDbContext context) : base((DbContext)context)
        {
            _context = context;
        }

        public override Task<PurchaseInvoice> GetAllAsync(object id)
        {
            throw new NotImplementedException();
        }

        public override Task<PurchaseInvoice> GetAsync(object id, string accountingUnitId)
        {
            return _context.PurchaseInvoices
                .Include(si => si.PurchaseInvoiceItems)
                .SingleOrDefaultAsync(nt => nt.Id == (int)id && nt.AccountingUnitId == accountingUnitId);
        }


        public async Task<bool> IsInvoiceNumberUnique(string accountingUnitId, string invoiceNumber)
        {
            var purchaseInvoice =
                await _context.PurchaseInvoices.SingleOrDefaultAsync(
                    inv => inv.InvoiceNumber == invoiceNumber && inv.AccountingUnitId == accountingUnitId);

            if (purchaseInvoice == null)
                return true;


            return false;
        }


    }
}