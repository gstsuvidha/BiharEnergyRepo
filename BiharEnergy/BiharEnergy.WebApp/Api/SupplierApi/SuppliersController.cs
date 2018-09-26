using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BiharEnergy.WebApp.Api.AccountingUnitApi;
using BiharEnergy.Persistence;
using BiharEnergy.Core.Domain.SupplierModule;
using BiharEnergy.Core.Domain.Accounting;
using BiharEnergy.Core.Enums;

namespace BiharEnergy.WebApp.Api.SupplierApi
{
    [Produces("application/json")]
    [Route("api/Suppliers")]
    public class SuppliersController : AccountingUnitResolverController
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IQueryModelDatabase _database;
        private readonly IAccountingUnitRepository _accountingUnitRepository;

        public SuppliersController(ISupplierRepository supplierRepository,
                                    IUnitOfWork unitOfWork,
                                    IMapper mapper,
                                    IQueryModelDatabase database, IQueryModelDatabase database1,
                                    IAccountingUnitRepository accountingUnitRepository)
                                : base(database, mapper, unitOfWork,accountingUnitRepository)
        {
            _supplierRepository = supplierRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _database = database1;
            _accountingUnitRepository = accountingUnitRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<SupplierResource>> GetSuppliers()
        {
            var suppliers = await _database
                                    .SuppliersFor(AccountingUnitId)
                                    .IsActive()
                                    .ToListAsync();

            return _mapper.Map<List<Supplier>, List<SupplierResource>>(suppliers.ToList());
        }

                [HttpGet("RegisteredSuppliers")]
        public async Task<IEnumerable<SupplierResource>> GetRegisteredSuppliers()
        {
            var suppliers = await _database
                                    .SuppliersFor(AccountingUnitId)
                                    .Where(su=>su.RegistrationType==RegistrationType.Registered || 
                                           su.RegistrationType==RegistrationType.CompositeDealer)
                                    .IsActive()
                                    .ToListAsync();

            return _mapper.Map<List<Supplier>, List<SupplierResource>>(suppliers.ToList());
        }

        [HttpGet("{id}")]
        public async Task<SaveSupplierResource> GetSupplierById(int id)
        {
            var supplier = await FindSupplierById(id);

            return _mapper.Map<Supplier, SaveSupplierResource>(supplier);
        }



        [HttpPost]
        public async Task<IActionResult> CreateSupplier([FromBody] SaveSupplierResource model)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);




            var supplier = new Supplier(model.Name, model.Gstin, model.Address, model.State, model.ContactNumber,
                                          AccountingUnitId, model.RegistrationType,model.Email);

            _supplierRepository.Add(supplier);

            await _unitOfWork.CompleteAsync();
            return Ok(_mapper.Map<Supplier, SupplierResource>(supplier));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSupplier(int id, [FromBody] Supplier model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var supplierFromDb = await FindSupplierById(id);
            if (supplierFromDb == null)
            {
                return NotFound();
            }

            supplierFromDb.Modify(model.Name, model.Gstin, model.Address, model.State, model.ContactNumber, 
                                  model.RegistrationType,model.Email);

            await _unitOfWork.CompleteAsync();
            return Ok(_mapper.Map<Supplier, SupplierResource>(supplierFromDb));


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            var supplierFromDb = await FindSupplierById(id);
            if (supplierFromDb == null)
            {
                return NotFound();
            }
            supplierFromDb.Delete();
            await _unitOfWork.CompleteAsync();
            return Ok();
        }


        private Task<Supplier> FindSupplierById(int id)
        {
            return _supplierRepository.GetAsync(id, AccountingUnitId);
        }



    }
}


