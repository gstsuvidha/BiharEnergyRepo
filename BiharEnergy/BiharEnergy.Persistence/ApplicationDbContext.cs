using BiharEnergy.Core.Domain.Accounting;
using BiharEnergy.Core.Domain.AccountModule;
using BiharEnergy.Core.Domain.AdjustCustomerAmountModule;
using BiharEnergy.Core.Domain.AdjustSupplierAmountModule;
using BiharEnergy.Core.Domain.AdvancedReceivedModule;
using BiharEnergy.Core.Domain.AdvancePaidModule;
using BiharEnergy.Core.Domain.CompanyModule;
using BiharEnergy.Core.Domain.CustomerModule;
using BiharEnergy.Core.Domain.ExportInvoiceModule;
using BiharEnergy.Core.Domain.InventoryModule;
using BiharEnergy.Core.Domain.IssueNoteModule;
using BiharEnergy.Core.Domain.ProductModule;
using BiharEnergy.Core.Domain.PurchaseInvoiceModule;
using BiharEnergy.Core.Domain.ReceiptNoteModule;
using BiharEnergy.Core.Domain.SalesInvoiceModule;
using BiharEnergy.Core.Domain.SupplierModule;
using BiharEnergy.Core.Domain.TdsModule;
using Microsoft.EntityFrameworkCore;

namespace BiharEnergy.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base((DbContextOptions)options)
        {

        }
         public DbSet<Customer> Customers { get; set; }
         public DbSet<Company> Companies { get; set; }
         public DbSet<Tds> Tdss { get; set; }
        public DbSet<AccountingUnit> AccountingUnits { get; set; }
         public DbSet<SalesInvoice> SalesInvoices { get; set; }
         public DbSet<SalesInvoiceItem> SalesInvoiceItems { get; set; }
         public DbSet<SalesInvoiceLog> SalesInvoiceLogs { get; set; }
         public DbSet<AdvancePaid> AdvancePaids { get; set; }
         public DbSet<AdvancePaidItem> AdvancePaidItems { get; set; }
         
        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<AdvanceReceived> AdvanceReceiveds { get; set; }
        public DbSet<AdvanceReceivedItem> AdvanceReceivedItems { get; set; }
        public DbSet<IssueNote> IssueNotes { get; set; }
        public DbSet<IssueNoteItem> IssueNoteItems { get; set; }
        public DbSet<PurchaseInvoice> PurchaseInvoices { get; set; }
        public DbSet<PurchaseInvoiceItem> PurchaseInvoiceItems { get; set; }
        public DbSet<ExportInvoice> ExportInvoices { get; set; }
        public DbSet<ExportInvoiceItem> ExportInvoiceItems { get; set; }
        public DbSet<ReceiptNote> ReceiptNotes { get; set; }
        public DbSet<ReceiptNoteItem> ReceiptNoteItems { get; set; }
        public DbSet<AdjustSupplierAmount> AdjustSupplierAmount { get; set; }//TODO: rename to plural
        public DbSet<AdjustCustomerAmount> AdjustCustomerAmount { get; set; }
        public DbSet<Inventory> Inventory { get; set; }
        public DbSet<Account> Account { get; set; }



        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //SalesInvoiceItem
            modelBuilder.Entity<SalesInvoiceItem>()
                .HasOne(fk => fk.Product)
                .WithMany(p => p.SalesInvoiceItems)
                .OnDelete(DeleteBehavior.Restrict);

             //SalesInvoiceNumberUnique
            modelBuilder.Entity<SalesInvoice>()
                .HasIndex(u => new { u.InvoiceNumber, u.AccountingUnitId })
                .IsUnique();

            //IssueNoteItem
            modelBuilder.Entity<IssueNoteItem>()
                .HasOne(fk => fk.Product)
                .WithMany(p => p.IssueNoteItems)
                .OnDelete(DeleteBehavior.Restrict);


            //IssueNoteNumberUnique
            modelBuilder.Entity<IssueNote>()
                .HasIndex(u => new { u.IssueNoteNumber, u.AccountingUnitId })
                .IsUnique();



            //AdvanceReceivedItem
            modelBuilder.Entity<AdvanceReceivedItem>()
                .HasOne(fk => fk.Product)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            //AdvanceReceiptNumberUnique
            modelBuilder.Entity<AdvanceReceived>()
                .HasIndex(u => new { u.ReceiptNumber, u.AccountingUnitId })
                .IsUnique();

            modelBuilder.Entity<AccountingUnit>()
                .HasMany(s => s.Customers)
                .WithOne(t => t.AccountingUnit)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Customer>()
                .HasMany(s => s.SalesInvoices)
                .WithOne(t => t.Customer)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Customer>()
             .HasMany(s => s.IssueNotes)
             .WithOne(t => t.Customer)
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Customer>()
            .HasMany(s => s.AdvanceReceiveds)
            .WithOne(t => t.Customer)
            .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Customer>()
                .HasMany(s => s.AdjustCustomerAmount)
                .WithOne(t => t.Customer)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Supplier>()
         .HasMany(s => s.PurchaseInvoices)
         .WithOne(t => t.Supplier)
         .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Supplier>()
             .HasMany(s => s.AdvancePaids)
             .WithOne(t => t.Supplier)
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Supplier>()
             .HasMany(s => s.ReceiptNotes)
             .WithOne(t => t.Supplier)
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Supplier>()
                .HasMany(s => s.AdjustSupplierAmount)
                .WithOne(t => t.Supplier)
                .OnDelete(DeleteBehavior.Restrict);



            //PurchaseInvoiceItem
            modelBuilder.Entity<PurchaseInvoiceItem>()
                .HasOne(fk => fk.Product)
                .WithMany(p => p.PurchaseInvoiceItems)
                .OnDelete(DeleteBehavior.Restrict);

            //AdvanceReceivedItem
            modelBuilder.Entity<AdvancePaidItem>()
                .HasOne(fk => fk.Product)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            //ReceiptNoteItem
            modelBuilder.Entity<ReceiptNoteItem>()
                .HasOne(fk => fk.Product)
                .WithMany(p => p.ReceiptNoteItems)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}