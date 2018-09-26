using System;
using System.Collections;
using System.Linq;
using AutoMapper;
using BiharEnergy.Core.Domain.Accounting;
using BiharEnergy.Persistence;
using BiharEnergy.WebApp.Api.AccountingUnitApi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BiharEnergy.WebApp.Api.ReportingApi
{
    [Produces("application/json")]
    [Route("api/Gstr7")]
    public class Gstr7COntroller : AccountingUnitResolverController
    {
        private readonly IQueryModelDatabase _database;
        public Gstr7COntroller(IQueryModelDatabase database, IMapper mapper, 
        IUnitOfWork unitOfWork,  IAccountingUnitRepository accountingUnitRepository) 
         : base(database, mapper, unitOfWork, accountingUnitRepository)
        {
            _database = database;
        }

        [HttpGet] 

        public IEnumerable  GetGstr7(int searchMonth,int year, string accountingUnitId)
        {
                var frmDate = new DateTime(year, searchMonth, 1);
                var toDate = frmDate.AddMonths(1).AddDays(-1);
                var fp = searchMonth.ToString("00") + year.ToString();
                    var accountingUnit = _database.AccountingUnits.SingleOrDefault(ai => ai.Id == accountingUnitId);
               
                var gstr7 = _database.TdssFor(accountingUnit.Id).Where(co => co.IsActive == true)
                         .ForDateRange(frmDate,toDate)
                        .GroupBy(a=>a.Supplier.Gstin)
                        .Select(a=> new Gstr7{
                             Gstin = a.Key,
                             
                            IgstAmount = a.Sum(b=>b.IgstAmount),
                            CgstAmount = a.Sum(b=>b.CgstAmount),
                            SgstAmount = a.Sum(b=>b.SgstAmount),
                            TdsAmount =  a.Sum(b=>b.TdsAmount),
                            
                            
                        }).ToList();
                return gstr7;
                  }
    
  
  
    [HttpGet ("details")]

    public IActionResult GetDetails (string accountingUnitId)
    {
  

  var accountingUnit = _database.AccountingUnits.SingleOrDefault(a=>a.Id==accountingUnitId);
                                         


                    return Ok(new Gstr7Details
                    {
                        TdsGstin = accountingUnit.TdsGstin,
                        LegalName = accountingUnit.AuthorizedRepresentativeName,
                        TradeName = accountingUnit.BusinessName
                    


                    });
                    
    }
    
    }



    public class Gstr7 
    {
            public string Gstin { get; set; }
            public double CgstAmount { get; set; }
            public double SgstAmount { get; set; }
            public double IgstAmount { get; set; }
            public double TdsAmount { get; set; }
    
    }

    public class Gstr7Details
    {
        public string TdsGstin {get; set; }
        public string LegalName {get; set; }
        public string TradeName {get; set; }
    }
}