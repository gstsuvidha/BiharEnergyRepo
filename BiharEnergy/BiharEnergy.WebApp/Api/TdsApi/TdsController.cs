using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BiharEnergy.Core.Domain.Accounting;
using BiharEnergy.Core.Domain.TdsModule;
using BiharEnergy.Persistence;
using BiharEnergy.WebApp.Api.AccountingUnitApi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BiharEnergy.WebApp.Api.TdsApi
{
    [Produces("application/json")]
    [Route("api/Tds")]
    

    public class TdsController : AccountingUnitResolverController
    {
        private readonly IQueryModelDatabase _database;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccountingUnitRepository _accountingUnitRepository;
        private readonly ITdsRepository _tdsRepository;
        public TdsController(IQueryModelDatabase database, IMapper mapper, IUnitOfWork unitOfWork,
        

        IAccountingUnitRepository accountingUnitRepository, ITdsRepository tdsRepository)
                    : base(database, mapper, unitOfWork, accountingUnitRepository)
        {
            _tdsRepository = tdsRepository;
            _accountingUnitRepository = accountingUnitRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _database = database;
        }

        [HttpGet]
        public async Task<IEnumerable<TdsResource>> GetAll()
        {
            var tds = await _database.TdssFor(AccountingUnitId)
                                    .Include(td=>td.Supplier)
                                    .ToListAsync();
             return _mapper.Map<List<Tds>,List<TdsResource>>(tds.Where(td => td.IsActive).ToList());
        }

          [HttpGet("{id}")]
        public async Task<SaveTdsResource> GetTdsById(int id)
        {
            var tds = await FindTdsById(id);

            return _mapper.Map<Tds, SaveTdsResource>(tds);
        }



        [HttpPost]
        public async Task<IActionResult> CreateTds([FromBody] SaveTdsResource model)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);




            var tds = new Tds(model.SupplierId, model.Date, model.PlaceOfSupply, model.AmountPaid,
                             model.CgstAmount, model.SgstAmount,model.IgstAmount, model.TdsAmount, 
                                 model.NetAmount, AccountingUnitId);

            _tdsRepository.Add(tds);

            await _unitOfWork.CompleteAsync();
            return Ok(_mapper.Map<Tds, TdsResource>(tds));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTds(int id, [FromBody] SaveTdsResource model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var tdsFromDb = await FindTdsById(id);
            if (tdsFromDb == null)
            {
                return NotFound();
            }

            tdsFromDb.Modify(model.SupplierId, model.Date, model.PlaceOfSupply, model.AmountPaid,
                             model.CgstAmount, model.SgstAmount,model.IgstAmount, model.TdsAmount, 
                               model.NetAmount,   AccountingUnitId);

            await _unitOfWork.CompleteAsync();
            return Ok(_mapper.Map<Tds, TdsResource>(tdsFromDb));


        }

      
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            var tdsFromDb = await FindTdsById(id);
            if (tdsFromDb == null)
            {
                return NotFound();
            }
            tdsFromDb.Delete();
            await _unitOfWork.CompleteAsync();
            return Ok();
        }


        private Task<Tds> FindTdsById(int id)
        {
            return _tdsRepository.GetAsync(id, AccountingUnitId);
        }



    }
}
