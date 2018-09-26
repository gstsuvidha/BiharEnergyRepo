using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BiharEnergy.WebApp.Api.AccountingUnitApi;
using BiharEnergy.Persistence;
using BiharEnergy.Core.Domain.PurchaseInvoiceModule;
using BiharEnergy.Persistence.QueryFilter.Filters;
using BiharEnergy.Core.Domain.Accounting;

namespace BiharEnergy.WebApp.Api.PurchaseApi
{
    [Produces("application/json")]
    [Route("api/PurchaseInvoices")]
    public class PurchaseInvoiceController : AccountingUnitResolverController
    {
        private readonly IPurchaseInvoiceRepository _purchaseInvoiceRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IQueryModelDatabase _database;
        public PurchaseInvoiceController(IPurchaseInvoiceRepository purchaseInvoiceRepository,
                                        IUnitOfWork unitOfWork, IMapper mapper, IQueryModelDatabase database,IAccountingUnitRepository accountingUnitRepository)
                                        : base(database, mapper, unitOfWork,accountingUnitRepository)
        {
            _purchaseInvoiceRepository = purchaseInvoiceRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _database = database;
        }

        [HttpGet]
        public async Task<IEnumerable<PurchaseInvoiceResource>> GetAll(int searchMonth, int year)
        {
            var fromDate = new DateTime(year, searchMonth, 1);
            var toDate = fromDate.AddMonths(1).AddDays(-1);

            var purchaseInvoices = await _database.
                                    PurchaseInvoicesFor(AccountingUnitId)
                                    .ForDateRange(fromDate, toDate)
                                    .IncludeSupplier()
                                    .ToListAsync();

            return _mapper.Map<List<PurchaseInvoice>, List<PurchaseInvoiceResource>>(purchaseInvoices.ToList());
        }


        [HttpGet("{id}")]
        public async Task<SavePurchaseInvoiceResource> GetById(int id)
        {

            var purchaseInvoice = await FindById(id);

            return _mapper.Map<PurchaseInvoice, SavePurchaseInvoiceResource>(purchaseInvoice);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]SavePurchaseInvoiceResource model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var supplier = await _database.SuppliersFor(AccountingUnitId).SingleOrDefaultAsync(supp => supp.Id == model.SupplierId);
            var accountingUnit = await _database.AccountingUnits.FindAsync(AccountingUnitId);


            
            var purchaseInvoice = new PurchaseInvoice(model.Date, model.PostingDate, 
                                                        model.InvoiceNumber, AccountingUnitId,
                                                        model.IsReverseChargeApplicable, model.SupplierId,
                                                        model.Etin,
                                                        model.PurchaseInvoiceType,
                                                        model.PlaceOfSupply, model.ShippingAddress,
                                                        model.TotalTaxableValue,
                                                        model.TotalTaxAmount, model.TotalCessAmount,  model.Reference,
                                                        model.BilledPassed,
                                                        PurchaseInvoiceItems(model),
                                                        supplier, accountingUnit);


            _purchaseInvoiceRepository.Add(purchaseInvoice);

            await _unitOfWork.CompleteAsync();
            return Ok(_mapper.Map<PurchaseInvoice, PurchaseInvoiceResource>(purchaseInvoice));
        }

        private static List<PurchaseInvoiceItem> PurchaseInvoiceItems(SavePurchaseInvoiceResource model)
        {
            var purchaseInvoiceItems = model.PurchaseInvoiceItems
                                        .Select(item => new PurchaseInvoiceItem(item.ProductId,
                                            item.Quantity, item.UnitPrice, item.Discount, item.TaxRate,
                                            item.TaxableValue, item.TaxAmount, item.IgstAmount, item.CgstAmount, item.SgstAmount,
                                            item.CessAmount, item.Total, item.TaxAmount + item.TaxableValue))
                                        .ToList();
            return purchaseInvoiceItems;
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody]SavePurchaseInvoiceResource model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var purchaseInvoiceFromDb = await FindById(id);
            if (purchaseInvoiceFromDb == null)
            {
                return NotFound();
            }

            var supplier = await _database.SuppliersFor(AccountingUnitId).SingleOrDefaultAsync(supp => supp.Id == model.SupplierId);
            var accountingUnit = await _database.AccountingUnits.FindAsync(AccountingUnitId);

         

            purchaseInvoiceFromDb.Modify(model.Date, model.PostingDate,
                                            model.IsReverseChargeApplicable, model.SupplierId, model.Etin,
                                            model.PurchaseInvoiceType,
                                            model.PlaceOfSupply, model.ShippingAddress, model.TotalTaxableValue,
                                            model.TotalTaxAmount, model.TotalCessAmount, 
                                            model.Reference,model.BilledPassed, PurchaseInvoiceItems(model),
                                            supplier, accountingUnit);

            await _unitOfWork.CompleteAsync();
            return Ok(_mapper.Map<PurchaseInvoice, PurchaseInvoiceResource>(purchaseInvoiceFromDb));


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var purchaseInvoiceFromDb = await FindById(id);
            if (purchaseInvoiceFromDb == null)
            {
                return NotFound();
            }

            _purchaseInvoiceRepository.Remove(purchaseInvoiceFromDb);
            await _unitOfWork.CompleteAsync();
            return Ok();
        }


        [HttpGet("{originalInvoiceNumber}")]
        public async Task<PurchaseInvoiceResource> GetInfoByInvoiceNumber(string invoiceNumber)
        {
            var purchaseInvoice = await _database.PurchaseInvoicesFor(AccountingUnitId).SingleOrDefaultAsync(p => p.InvoiceNumber == invoiceNumber);

            return _mapper.Map<PurchaseInvoice, PurchaseInvoiceResource>(purchaseInvoice);
        }




        //[HttpGet("isInvoiceNumberUnique/{invoiceNumber}")]

        //public async Task<bool> IsInvoiceNumberUnique(string invoiceNumber)
        //{
        //    var result = await _purchaseInvoiceRepository.IsInvoiceNumberUnique(GetCurrentTenantId(), invoiceNumber);

        //    return result;
        //}

        private Task<PurchaseInvoice> FindById(int id)
        {
            return _purchaseInvoiceRepository.GetAsync(id, AccountingUnitId);
        }

    }
}


