using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BiharEnergy.WebApp.Api.AccountingUnitApi;
using BiharEnergy.Persistence;
using BiharEnergy.Core.Domain.SalesInvoiceModule;
using BiharEnergy.Persistence.QueryFilter.Filters;
using BiharEnergy.Core.Domain.Accounting;

namespace BiharEnergy.WebApp.Api.SaleInvoiceApi
{
    [Produces("application/json")]
    [Route("api/SalesInvoices")]
    public class SalesInvoiceController : AccountingUnitResolverController
    {
        private readonly ISalesInvoiceRepository _salesInvoiceRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IQueryModelDatabase _database;
        public SalesInvoiceController(ISalesInvoiceRepository salesInvoiceRepository,
                                        IUnitOfWork unitOfWork,
                                        IMapper mapper,
                                        IQueryModelDatabase database, IQueryModelDatabase database1,
                                        IAccountingUnitRepository accountingUnitRepository)
                                    : base(database, mapper, unitOfWork,accountingUnitRepository)
        {
            _salesInvoiceRepository = salesInvoiceRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _database = database1;
        }

        [HttpGet]
        public async Task<IEnumerable<SalesInvoiceResource>> GetAll(int searchMonth,int year)
        {
            // if(searchMonth == 1 || searchMonth == 2 || searchMonth == 3)
            //     year++;
            // Thread.Sleep(1000);
            var fromDate = new DateTime(year, searchMonth, 1);
            var toDate = fromDate.AddMonths(1).AddDays(-1);


            var salesInvoices = await _database
                                        .SalesInvoicesFor(AccountingUnitId)
                                        .ForDateRange(fromDate, toDate)
                                        .IncludeCustomer()
                                        .OrderByDescending(si=>si.InvoiceNumber)
                                        .ToListAsync();



            return _mapper.Map<List<SalesInvoice>, List<SalesInvoiceResource>>(salesInvoices.ToList());
        }


        [HttpGet("{id}")]
        public async Task<SaveSalesInvoiceResource> GetById(int id)
        {

            var salesInvoice = await FindSalesInvoiceById(id);

            return _mapper.Map<SalesInvoice, SaveSalesInvoiceResource>(salesInvoice);
        }
        [HttpGet("{id}/detail")]
        public async Task<PrintSalesInvoice> GetDetailsById(int id)
        {

            var salesInvoice = await _database.SalesInvoicesFor(AccountingUnitId).GetForPrint(id);

            return _mapper.Map<SalesInvoice, PrintSalesInvoice>(salesInvoice);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]SaveSalesInvoiceResource model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var customer = await _database
                                    .CustomersFor(AccountingUnitId)
                                    .FindById(model.CustomerId);

             var accountingUnit = await _database.AccountingUnits.FindAsync(AccountingUnitId);
            


            var salesInvoice = new SalesInvoice(model.Date,accountingUnit.InvoicePrefix, model.InvoiceNumber,
                                                model.IsReverseChargeApplicable, model.CustomerName,
                                                model.BillingAddress, model.Etin,
                                                 model.IsPlaceOfSupplyDifferent, model.PlaceOfSupply,
                                                 model.ShippingAddress, model.TotalTaxableValue,
                                                 model.TotalTaxAmount, model.TotalCessAmount,
                                                  model.Freight, model.Insurance, model.Reference,
                                                 model.PackingFwdChrg, 
                                                 SalesInvoiceItems(model),
                                                 customer,
                                                 accountingUnit);


            _salesInvoiceRepository.Add(salesInvoice);

            await _unitOfWork.CompleteAsync();
            return Ok(_mapper.Map<SalesInvoice, SalesInvoiceResource>(salesInvoice));
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody]SaveSalesInvoiceResource model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var salesInvoiceFromDb = await FindSalesInvoiceById(id);
            if (salesInvoiceFromDb == null)
            {
                return NotFound();
            }


            var customer = await _database
                .CustomersFor(AccountingUnitId)
                .FindById(model.CustomerId);

            var accountingUnit = await _database.AccountingUnits.FindAsync(AccountingUnitId);
           




            salesInvoiceFromDb.Modify(model.Date, accountingUnit.InvoicePrefix,
                                        model.IsReverseChargeApplicable, model.CustomerName,
                                        model.BillingAddress, model.Etin,
                                        model.IsPlaceOfSupplyDifferent, model.PlaceOfSupply,
                                        model.ShippingAddress, 
                                       model.TotalTaxableValue,
                                        model.TotalTaxAmount, model.TotalCessAmount, model.Freight,
                                        model.Insurance, model.Reference, model.PackingFwdChrg,
                                        SalesInvoiceItems(model), customer, accountingUnit);

            await _unitOfWork.CompleteAsync();
            return Ok(_mapper.Map<SalesInvoice, SalesInvoiceResource>(salesInvoiceFromDb));


        }

        private static List<SalesInvoiceItem> SalesInvoiceItems(SaveSalesInvoiceResource model)
        {
            return model.SalesInvoiceItems.Select(item => SalesInvoiceItem.Add(item.ProductId,
                    item.Quantity, item.UnitPrice,
                    item.Discount, item.TaxRate,
                    item.TaxableValue, item.TaxAmount,
                    item.Cess, item.Total))
                .ToList();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var salesInvoiceFromDb = await FindSalesInvoiceById(id);
            if (salesInvoiceFromDb == null)
            {
                return NotFound();
            }

            _salesInvoiceRepository.Remove(salesInvoiceFromDb);

            var log = new SalesInvoiceLog(salesInvoiceFromDb.InvoiceNumber,salesInvoiceFromDb.TotalInvoiceValue,salesInvoiceFromDb.AccountingUnitId);

            _salesInvoiceRepository.AddLog(log);
            await _unitOfWork.CompleteAsync();
            return Ok();
        }
       
        [HttpGet("invoiceNumber")]
        public async Task<int> GetInvoiceNumber()
        {
            var invoiceNumber = await _salesInvoiceRepository.GetLastInvoiceNumber(AccountingUnitId);


            return invoiceNumber + 1;
        }
        [HttpGet("isInvoiceNumberUnique/{invoiceNumber}")]

        public async Task<bool> IsInvoiceNumberUnique(int invoiceNumber)
        {
            var result = await _salesInvoiceRepository.IsInvoiceNumberUnique(AccountingUnitId, invoiceNumber);

            //if (invoiceNumber == 12)
            //    return false;

            return result;
        }

        private Task<SalesInvoice> FindSalesInvoiceById(int id)
        {
            return _salesInvoiceRepository.GetAsync(id, AccountingUnitId);
        }

    }

}