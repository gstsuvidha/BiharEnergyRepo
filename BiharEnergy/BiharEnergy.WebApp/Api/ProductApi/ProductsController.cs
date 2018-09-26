using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BiharEnergy.WebApp.Api.AccountingUnitApi;
using BiharEnergy.Persistence;
using BiharEnergy.Core.Domain.ProductModule;
using BiharEnergy.Core.Domain.Accounting;

namespace BiharEnergy.WebApp.Api.ProductApi
{
    //TODO: uncomment [Authorize]
    [Produces("application/json")]
    [Route("api/Products")]
    public class ProductsController : AccountingUnitResolverController
    {
        private readonly IQueryModelDatabase _database;
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductsController(IProductRepository productRepository,
                                    IUnitOfWork unitOfWork, IMapper mapper,
                                    IQueryModelDatabase database,IAccountingUnitRepository accountingUnitRepository)
                                : base(database, mapper, unitOfWork,accountingUnitRepository)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _database = database;
        }

        [HttpGet]
        public async Task<IEnumerable<ProductResource>> GetProducts()
        {
            var products = await _database
                                    .ProductsFor(AccountingUnitId)
                                    .IsActive()
                                    .ToListAsync();

            return _mapper.Map<List<Product>, List<ProductResource>>(products.ToList());
        }

        [HttpGet("{id}")]
        public async Task<ProductResource> GetProductById(int id)
        {


            var product = await FindProductById(id);

            return _mapper.Map<Product, ProductResource>(product);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody]SaveProductResource model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);




            var product = new Product(model.Name, model.Unit, model.UnitOthers, model.Description, model.IsTaxIncluded,
                                      model.IsSaleable, model.IsPurchaseable, model.Rate, 
                                      model.HsnSacCode, model.ProductType,
                                      model.IsReverseChargeApplicable, model.PerReverseCharge,
                                      model.Igst, model.Cess, model.ItcPercentage, model.ItcEligibility,
                                      AccountingUnitId);

            _productRepository.Add(product);

            await _unitOfWork.CompleteAsync();
            return Ok(_mapper.Map<Product, ProductResource>(product));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody]SaveProductResource model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var productFromDb = await FindProductById(id);
            if (productFromDb == null)
            {
                return NotFound();
            }
            productFromDb.Modify(model.Name, model.Unit, model.UnitOthers, model.Description, model.IsTaxIncluded, 
                                    model.IsSaleable, model.IsPurchaseable, model.Rate,
                                    model.HsnSacCode, model.ProductType,
                                    model.IsReverseChargeApplicable, model.PerReverseCharge,
                                    model.Igst, model.Cess, model.ItcPercentage, model.ItcEligibility);

            await _unitOfWork.CompleteAsync();
            return Ok(_mapper.Map<Product, ProductResource>(productFromDb));


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            var productFromDb = await FindProductById(id);
            if (productFromDb == null)
            {
                return NotFound();
            }

            productFromDb.Delete();
            await _unitOfWork.CompleteAsync();
            return Ok();
        }

        private Task<Product> FindProductById(int id)
        {
            return _productRepository.GetAsync(id, AccountingUnitId);
        }


    }


}