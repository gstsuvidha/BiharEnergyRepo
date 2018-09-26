using AutoMapper;
using BiharEnergy.Core.Domain.Accounting;
using BiharEnergy.Core.Domain.CustomerModule;
using BiharEnergy.Persistence;
using BiharEnergy.WebApp.Api.AccountingUnitApi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BiharEnergy.WebApp.Api.CustomerApi
{
    [Produces("application/json")]
    [Route("api/Customers")]
    public class CustomersController : AccountingUnitResolverController
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IQueryModelDatabase _database;
        private readonly IAccountingUnitRepository _accountingUnitRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public CustomersController(ICustomerRepository customerRepository,
                                    IUnitOfWork unitOfWork, IMapper mapper,
                                    IQueryModelDatabase database, IQueryModelDatabase database1, IAccountingUnitRepository accountingUnitRepository)
                                    : base(database, mapper, unitOfWork, accountingUnitRepository)

        {
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _database = database1;
            _accountingUnitRepository = accountingUnitRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<CustomerResource>> GetCustomers()
        {
            var customers = await _database.
                                     CustomersFor(AccountingUnitId)
                                     .IsActive()
                                    .ToListAsync();

            return _mapper.Map<List<Customer>, List<CustomerResource>>(customers.ToList());
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] SaveCustomerResource model)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var customer = new Customer(model.Name,
                                        model.Gstin,
                                        model.Address,
                                        AccountingUnitId,
                                        model.State,
                                        model.ContactNumber,
                                        model.RegistrationType,
                                        model.Email
                                       );


            _customerRepository.Add(customer);

            await _unitOfWork.CompleteAsync();
            return Ok(_mapper.Map<Customer, CustomerResource>(customer));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, [FromBody] SaveCustomerResource model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var customerFromDb = await FindCustomerById(id);
            if (customerFromDb == null)
            {
                return NotFound();
            }

            customerFromDb.Modify(model.Name, model.Gstin, model.Address, model.State, model.ContactNumber,
                                    model.RegistrationType,model.Email);


            await _unitOfWork.CompleteAsync();
            return Ok(_mapper.Map<Customer, CustomerResource>(customerFromDb));
        }


        [HttpGet("{id}")]
        public async Task<SaveCustomerResource> GetCustomerById(int id)
        {
            var customer = await FindCustomerById(id);

            return _mapper.Map<Customer, SaveCustomerResource>(customer);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            var customerFromDb = await FindCustomerById(id);
            if (customerFromDb == null)
            {
                return NotFound();
            }
            customerFromDb.Delete();
            await _unitOfWork.CompleteAsync();
            return Ok();
        }

        private Task<Customer> FindCustomerById(int id)
        {
            return _customerRepository.GetAsync(id, AccountingUnitId);
        }



    }
}