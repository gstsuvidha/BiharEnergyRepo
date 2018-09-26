using AutoMapper;
using BiharEnergy.Core.Domain.Accounting;
using BiharEnergy.Core.Domain.CompanyModule;
using BiharEnergy.Core.Domain.CustomerModule;
using BiharEnergy.Core.Domain.ProductModule;
using BiharEnergy.Core.Domain.PurchaseInvoiceModule;
using BiharEnergy.Core.Domain.SalesInvoiceModule;
using BiharEnergy.Core.Domain.SupplierModule;
using BiharEnergy.Core.Domain.TdsModule;
using BiharEnergy.Core.Enums;
using BiharEnergy.WebApp.Api.AccountingUnitApi;
using BiharEnergy.WebApp.Api.CustomerApi;
using BiharEnergy.WebApp.Api.ProductApi;
using BiharEnergy.WebApp.Api.PurchaseApi;
using BiharEnergy.WebApp.Api.SaleInvoiceApi;
using BiharEnergy.WebApp.Api.SupplierApi;
using BiharEnergy.WebApp.Api.TdsApi;

namespace BiharEnergy.WebApp.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AccountingUnit, AccountingUnitResource>();
            CreateMap<Company, CompanyResource>();
            
            CreateMap<Customer, CustomerResource>()
            .ForMember(cr => cr.RegistrationType, opt => opt.MapFrom(c => c.RegistrationType.ToString()));
            CreateMap<Customer, SaveCustomerResource>();
            CreateMap<Customer , KeyValuePairResource>(); 
            CreateMap<Product, ProductResource>();
            CreateMap<Product, SaveProductResource>();
            CreateMap<Product, KeyValuePairResource>();
            
            CreateMap<SalesInvoice, SalesInvoiceResource>()
            .AfterMap((si, sR) =>
            {
                if (si.SupplyType == SupplyType.InterState)
                {
                    sR.TotalIgst = si.TotalTaxAmount-si.TotalCessAmount ;
                    sR.TotalCgst = 0;
                    sR.TotalSgst = 0;
                }
                else
                {
                    sR.TotalIgst = 0;
                    sR.TotalCgst = (si.TotalTaxAmount - si.TotalCessAmount) / 2;
                    sR.TotalSgst = (si.TotalTaxAmount - si.TotalCessAmount) / 2;
                }

            });


            CreateMap<SalesInvoice, SaveSalesInvoiceResource>();
            CreateMap<SalesInvoiceItem, SaveSalesInvoiceItemResource>();
            CreateMap<SalesInvoice, PrintSalesInvoice>();
            CreateMap<SalesInvoiceItem, PrintSalesInvoiceItem>();
            CreateMap<Supplier, SupplierResource>();
            CreateMap<Supplier, SaveSupplierResource>();
            CreateMap<Tds, TdsResource>();
            CreateMap<Tds, SaveTdsResource>();
            CreateMap<Supplier, KeyValuePairResource>();

            CreateMap<PurchaseInvoice, PurchaseInvoiceResource>()
             .AfterMap((si, sR) =>
             {
                 if (si.SupplyType == SupplyType.InterState)
                 {
                     sR.TotalIgst = si.TotalTaxAmount-si.TotalCessAmount;
                     sR.TotalCgst = 0;
                     sR.TotalSgst = 0;
                 }
                               else
                  {
                      sR.TotalIgst = 0;
                      sR.TotalCgst = (si.TotalTaxAmount-si.TotalCessAmount) / 2;
                      sR.TotalSgst = (si.TotalTaxAmount-si.TotalCessAmount) / 2;
                  }

              });

              CreateMap<PurchaseInvoice, SavePurchaseInvoiceResource>();
              CreateMap<PurchaseInvoiceItem, SavePurchaseInvoiceItemResource>();








        }
    }
}