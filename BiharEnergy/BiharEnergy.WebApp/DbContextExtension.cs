using BiharEnergy.Core.Domain.Accounting;
using BiharEnergy.Core.Domain.CompanyModule;
using BiharEnergy.Core.Domain.CustomerModule;
using BiharEnergy.Core.Domain.ProductModule;
using BiharEnergy.Core.Domain.PurchaseInvoiceModule;
using BiharEnergy.Core.Domain.SalesInvoiceModule;
using BiharEnergy.Core.Domain.SupplierModule;
using BiharEnergy.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BiharEnergy.WebApp
{
    public static class DbContextExtension
    {
        public static bool AllMigrationsApplied(this DbContext context)
        {

            var applied = context.GetService<IHistoryRepository>()
                .GetAppliedMigrations()
                .Select(m => m.MigrationId);

            var total = context.GetService<IMigrationsAssembly>()
                .Migrations
                .Select(m => m.Key);

            return !total.Except(applied).Any();
        }

        public static void EnsureSeeded(this ApplicationDbContext context)
        {
            if (!context.Companies.Any())
            {
                var data = JsonConvert.DeserializeObject<List<Company>>(File.ReadAllText("seed" + Path.DirectorySeparatorChar + "company.json"));
                context.AddRange(data);
                context.SaveChanges();
            }

            if (!context.Customers.Any())
            {
                var data = JsonConvert.DeserializeObject<List<Customer>>(File.ReadAllText("seed" + Path.DirectorySeparatorChar + "customers.json"));
                context.AddRange(data);
                context.SaveChanges();
            }
            
            if (!context.Products.Any())
            {
                var data = JsonConvert.DeserializeObject<List<Product>>(File.ReadAllText("seed" + Path.DirectorySeparatorChar + "products.json"));
                context.AddRange(data);
                context.SaveChanges();
            }
            if (!context.Suppliers.Any())
            {
                var data = JsonConvert.DeserializeObject<List<Supplier>>(File.ReadAllText("seed" + Path.DirectorySeparatorChar + "suplliers.json"));
                context.AddRange(data);
                context.SaveChanges();
            }
            if (!context.SalesInvoices.Any())
            {
                var data = JsonConvert.DeserializeObject<List<SalesInvoice>>(File.ReadAllText("seed" + Path.DirectorySeparatorChar + "salesInvoices.json"));
                context.AddRange(data);
                context.SaveChanges();
            }
            if (!context.PurchaseInvoices.Any())
            {
                var data = JsonConvert.DeserializeObject<List<PurchaseInvoice>>(File.ReadAllText("seed" + Path.DirectorySeparatorChar + "purchaseInvoice.json"));
                context.AddRange(data);
                context.SaveChanges();
            }
            if(!context.AccountingUnits.Any())
            {
                var data = JsonConvert.DeserializeObject<List<AccountingUnit>>(File.ReadAllText("seed" + Path.DirectorySeparatorChar + "accountingUnit.json"));
                context.AddRange(data);
                context.SaveChanges();

            }
        }

    }
}