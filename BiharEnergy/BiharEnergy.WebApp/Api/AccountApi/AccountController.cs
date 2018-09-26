using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BiharEnergy.WebApp.Api.AccountingUnitApi;
using BiharEnergy.Core.Domain.AccountModule;
using BiharEnergy.Persistence;
using BiharEnergy.Core.Domain.Accounting;

namespace BiharEnergy.WebApp.Api.AccountApi
{

    [Produces("application/json")]
    [Route("api/Accounts")]
    public class AccountController : AccountingUnitResolverController
    {
        private readonly IAccountsRepository _accountRepository;
        private readonly IQueryModelDatabase _database;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AccountController(IAccountsRepository accountRepository, IMapper mapper, IUnitOfWork unitOfWork, IQueryModelDatabase database,IAccountingUnitRepository accountingUnitRepository) : base(database, mapper, unitOfWork,accountingUnitRepository)
        {
            _accountRepository = accountRepository;
            _unitOfWork = unitOfWork;
            _database = database;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<AccountsResource>> GetAllTenantAccount()
        {
            var account = await _database.AccountsFor(AccountingUnitId).IsActive().ToListAsync();

            return _mapper.Map<List<Account>, List<AccountsResource>>(account.ToList());
        }

        [HttpGet("{id}")]
        public async Task<AccountsResource> GetAccountById(int id)
        {
            var account = await FindAccountById(id);

            return _mapper.Map<Account, AccountsResource>(account);
        }



        [HttpPost]
        public async Task<IActionResult> CreateAccount([FromBody] SaveAccountResource model)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var account = new Account(model.AccountName, model.Description, model.Nature, model.OpeningBalance, AccountingUnitId);

            _accountRepository.Add(account);

            await _unitOfWork.CompleteAsync();
            return Ok(_mapper.Map<Account, AccountsResource>(account));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAccount(int id, [FromBody] SaveAccountResource model)
        {
            var accountFromDb = await FindAccountById(id);
            if (accountFromDb == null)
                return NotFound();


            accountFromDb.Modify(model.AccountName, model.Description, model.Nature, model.OpeningBalance);

            await _unitOfWork.CompleteAsync();
            return Ok(model);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            var accountFromDb = await FindAccountById(id);

            if (accountFromDb == null)
                return NotFound();

            accountFromDb.Delete();
            await _unitOfWork.CompleteAsync();
            return Ok();
        }

        private Task<Account> FindAccountById(int id)
        {
            return _accountRepository.GetAsync(id, AccountingUnitId);
        }


    }
}
