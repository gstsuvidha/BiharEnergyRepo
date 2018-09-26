

using BiharEnergy.Core.Domain.Accounting;
using BiharEnergy.Core.Domain.AccountModule;
using BiharEnergy.Core.Domain.AdvancedReceivedModule;
using BiharEnergy.Core.Domain.AdvancePaidModule;
using BiharEnergy.Core.Domain.CompanyModule;
using BiharEnergy.Core.Domain.CustomerModule;
using BiharEnergy.Core.Domain.IssueNoteModule;
using BiharEnergy.Core.Domain.ProductModule;
using BiharEnergy.Core.Domain.PurchaseInvoiceModule;
using BiharEnergy.Core.Domain.ReceiptNoteModule;
using BiharEnergy.Core.Domain.SalesInvoiceModule;
using BiharEnergy.Core.Domain.SupplierModule;
using BiharEnergy.Core.Domain.TdsModule;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BiharEnergy.Persistence
{
    public interface IQueryModelDatabase
    {
        DbSet<AccountingUnit> AccountingUnits { get; }
        DbSet<Company> Companies { get; }
        IQueryable<Customer> CustomersFor(string accountingUnitId);
        IQueryable<Tds> TdssFor(string accountingUnitId);
        IQueryable<Tds> TdssForCompany(int companyId);
        IQueryable<AdvanceReceived> AdvanceReceivesFor(string accountingUnitId);
        IQueryable<AdvanceReceived> AdvanceReceivesForCompany(int companyId);

        IQueryable<SalesInvoice> SalesInvoicesFor(string accountingUnitId);
        IQueryable<SalesInvoice> SalesInvoicesForCompany(int companyId);

        IQueryable<AdvancePaid> AdvancePaidsFor(string accountingUnitId);
        IQueryable<AdvancePaid> AdvancePaidsForCompany(int companyId);
        IQueryable<Supplier> SuppliersFor(string accountingUnitId);
        IQueryable<Product> ProductsFor(string accountingUnitId);
        IQueryable<IssueNote> IssueNotesFor(string accountingUnitId);
        IQueryable<IssueNote> IssueNotesForCompany(int companyId);
        IQueryable<PurchaseInvoice> PurchaseInvoicesFor(string accountingUnitId);
        IQueryable<PurchaseInvoice> PurchaseInvoicesForCompany(int companyId);
        IQueryable<ReceiptNote> ReceiptNotesFor(string accountingUnitId);
        IQueryable<ReceiptNote> ReceiptNotesForCompany(int companyId);
        IQueryable<Account> AccountsFor(string accountingUnitId);

    }
    public class QueryModelDatabase : IQueryModelDatabase
    {
        private readonly ApplicationDbContext _context;

        public QueryModelDatabase(ApplicationDbContext context)
        {
            _context = context;
        }
        public DbSet<AccountingUnit> AccountingUnits => _context.AccountingUnits;
        public DbSet<Company> Companies => _context.Companies;

        public IQueryable<Customer> CustomersFor(string accountingUnitId)
        {
            return _context.Customers.ForAccountingUnit(accountingUnitId);
        }
            
        public IQueryable<SalesInvoice> SalesInvoicesFor(string accountingUnitId)
        {
            return _context.SalesInvoices.ForAccountingUnit(accountingUnitId);
        }

        public IQueryable<SalesInvoice> SalesInvoicesForCompany(int companyId)
        {
            return _context.SalesInvoices.Include(si => si.AccountingUnit).Where(si => si.AccountingUnit.CompanyId == companyId);
        }
        public IQueryable<AdvancePaid> AdvancePaidsFor(string accountingUnitId)
        {
            return _context.AdvancePaids.ForAccountingUnit(accountingUnitId);
        }
        public IQueryable<AdvancePaid> AdvancePaidsForCompany(int companyId)
        {
            return _context.AdvancePaids.Include(ap => ap.AccountingUnit).Where(ap => ap.AccountingUnit.CompanyId == companyId);
        }
        public IQueryable<Product> ProductsFor(string accountingUnitId)
        {
            return _context.Products.ForAccountingUnit(accountingUnitId);
        }
        public IQueryable<Supplier> SuppliersFor(string accountingUnitId)
        {
            return _context.Suppliers.ForAccountingUnit(accountingUnitId);
        }
        public IQueryable<AdvanceReceived> AdvanceReceivesFor(string accountingUnitId)
        {
            return _context.AdvanceReceiveds.ForAccountingUnit(accountingUnitId);
        }

        public IQueryable<AdvanceReceived> AdvanceReceivesForCompany(int companyId)
        {
            return _context.AdvanceReceiveds.Include(ar => ar.AccountingUnit).Where(ar => ar.AccountingUnit.CompanyId == companyId);
        }
        public IQueryable<IssueNote> IssueNotesFor(string accountingUnitId)
        {
            return _context.IssueNotes.ForAccountingUnit(accountingUnitId);
        }
        public IQueryable<IssueNote> IssueNotesForCompany(int companyId)
        {
            return _context.IssueNotes.Include(isn => isn.AccountingUnit).Where(isn => isn.AccountingUnit.CompanyId == companyId);
        }
        public IQueryable<PurchaseInvoice> PurchaseInvoicesFor(string accountingUnitId)
        {
            return _context.PurchaseInvoices.ForAccountingUnit(accountingUnitId);
        }
        public IQueryable<PurchaseInvoice> PurchaseInvoicesForCompany(int companyId)
        {
            return _context.PurchaseInvoices.Include(pi => pi.AccountingUnit).Where(pi => pi.AccountingUnit.CompanyId == companyId);
        }
        public IQueryable<ReceiptNote> ReceiptNotesFor(string accountingUnitId)
        {
            return _context.ReceiptNotes.ForAccountingUnit(accountingUnitId);
        }
        public IQueryable<ReceiptNote> ReceiptNotesForCompany(int companyId)
        {
            return _context.ReceiptNotes.Include(pi => pi.AccountingUnit).Where(pi => pi.AccountingUnit.CompanyId == companyId);
        }
        public IQueryable<Account> AccountsFor(string accountingUnitId)
        {
            return _context.Account.ForAccountingUnit(accountingUnitId);
        }

        public IQueryable<Tds> TdssFor(string accountingUnitId)
        {
            return _context.Tdss.ForAccountingUnit(accountingUnitId);
        }

        public IQueryable<Tds> TdssForCompany(int companyId)
        {
            return _context.Tdss.Include(td=> td.AccountingUnit).Where(td=>td.AccountingUnit.CompanyId==companyId);
        }
    }
}