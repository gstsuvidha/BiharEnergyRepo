using AutoMapper;
using BiharEnergy.Core.Domain.Accounting;
using BiharEnergy.Core.Domain.CompanyModule;
using BiharEnergy.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BiharEnergy.WebApp.Api.AccountingUnitApi
{
    [Produces("application/json")]
    [Route("api/AccountingUnitResolver")]
    [Authorize]
    public class AccountingUnitResolverController : Controller
    {
        private readonly IQueryModelDatabase _database;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccountingUnitRepository _accountingUnitRepository;

        protected string AccountingUnitId => GetCurrentAccountingUnitId();



        public AccountingUnitResolverController(IQueryModelDatabase database, IMapper mapper, IUnitOfWork unitOfWork, IAccountingUnitRepository accountingUnitRepository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _accountingUnitRepository = accountingUnitRepository;
            _database = database;
        }

        private string GetCurrentAccountingUnitId()
        {
            var accountingUnitId = _database.AccountingUnits.SingleAsync(t => t.Subject == User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value).Result;

            return accountingUnitId.Id;
        }


        protected async Task<string> GetAccountingUnitPlaceOfSupply()
        {
            var accountingUnit = await _database.AccountingUnits.SingleOrDefaultAsync(
                u => u.Subject == User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);

            return accountingUnit.PlaceOfSupply;
        }

        [HttpGet("AccountingUnits")]
        public async Task<IEnumerable<AccountingUnitResource>> GetAccountingUnits()
        {
            var accountingUnits = await _database.AccountingUnits
                                    .ToListAsync();

            return _mapper.Map<List<AccountingUnit>, List<AccountingUnitResource>>(accountingUnits.ToList());
        }
        [HttpGet("AccountingUnitsForCompany")]
        public async Task<IEnumerable<AccountingUnitResource>> GetAccountingUnitsForCompanyId(int companyId)
        {
            var accountingUnitByCompanyId = await _accountingUnitRepository.GetAccountingUnitsByCompanyId(companyId);
            
            return _mapper.Map<List<AccountingUnit>,List<AccountingUnitResource>>(accountingUnitByCompanyId.ToList());
        }

        [HttpGet("Companies")]
        public async Task<IEnumerable<CompanyResource>> GetCompanies()
        {
            var companies = await _database.Companies
                                    .ToListAsync();

            return _mapper.Map<List<Company>, List<CompanyResource>>(companies.ToList());
        }


        [HttpGet("Profile")]
        public async Task<AccountingUnitResource> GetAccountingUnitProfielById()
        {
            //Todo uncomment
            var accountingUnit = await _database.AccountingUnits.SingleOrDefaultAsync
                          (u => u.Subject == User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);

            return _mapper.Map<AccountingUnit, AccountingUnitResource>(accountingUnit);
        }

        [HttpPut("Profile")]
        public async Task<IActionResult> UpdateAccountingUnit([FromBody] SaveAccountingUnitResource model)
        {
            // var accountingUnitFromDb = await _database.AccountingUnits.SingleOrDefaultAsync();
             var accountingUnitFromDb = await _database.AccountingUnits.SingleOrDefaultAsync(t => t.Subject == User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            if (accountingUnitFromDb == null)
            {
                return NotFound();
            }

            accountingUnitFromDb.Modify(model.BusinessName, model.PlaceOfSupply, model.Gstin, model.TdsGstin, model.Email, model.Address, model.ContactNumber, model.TurnOver,
                                model.BankAccountName, model.BankAccountNumber, model.IfscCode, model.RegistrationType, model.TermsAndCondition,
                                model.InventorySelection, model.CurrentGrossTurnOver, model.SelectedYear, model.InvoicePrefix,model.CompanyId,model.Pan);

            await _unitOfWork.CompleteAsync();
            return Ok(model);
        }
        [HttpPost("Register")]
        public async Task<IActionResult> CreateAccountingUnit([FromBody] SaveAccountingUnitResource model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);



            var accountingUnit = new AccountingUnit(model.BusinessName, model.PlaceOfSupply, model.Gstin, model.TdsGstin, model.Email, model.Address, model.ContactNumber, model.TurnOver,
                               model.BankAccountName, model.BankAccountNumber, model.IfscCode, model.RegistrationType, model.TermsAndCondition,
                               model.InventorySelection, model.CurrentGrossTurnOver, model.SelectedYear, model.InvoicePrefix,model.Pan);

            _accountingUnitRepository.Add(accountingUnit);
            await _unitOfWork.CompleteAsync();
            return Ok(model);
        }
        [HttpGet("invoicePrefix")]
        public async Task<string> GetInvoicePrefix()
        {
            var invoicePrefix = await _accountingUnitRepository.GetInvoicePrefix(AccountingUnitId);
            return invoicePrefix.InvoicePrefix;
        }

    }
}