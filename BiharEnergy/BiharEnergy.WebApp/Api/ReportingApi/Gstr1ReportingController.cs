using AutoMapper;
using BiharEnergy.Core.Domain;
using BiharEnergy.Core.Domain.Accounting;
using BiharEnergy.Core.Domain.Reporting;
using BiharEnergy.Persistence;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;

using System.Linq;
using System.Threading.Tasks;


namespace BiharEnergy.WebApp.Api.ReportingApi
{
    [Produces("application/json")]
    [Route("api/Gstr1Reporting")]
    public class Gstr1ReportingController : Controller
    {
        private readonly IGstr1Repository _gstr1ReportingRepository;
        private readonly IQueryModelDatabase _database;
        private readonly IAccountingUnitRepository _accountingUnitRepository;

        public Gstr1ReportingController(IGstr1Repository gstr1ReportingRepository,
                                        IUnitOfWork unitOfWork, IMapper mapper,
                                        IQueryModelDatabase database,
                                        IAccountingUnitRepository accountingUnitRepository)

        {
            _database = database;
            _accountingUnitRepository = accountingUnitRepository;
            _gstr1ReportingRepository = gstr1ReportingRepository;

        }



        // gstr1

        //        [HttpGet("gstr1")]
        //        public IActionResult GetGstr1(int searchMonth)
        //        {
        //            //var fromDate = new DateTime(DateTime.Now.Year, searchMonth, 1);
        //            // var toDate = fromDate.AddMonths(1).AddDays(-1);
        //
        //            var data = JsonConvert.DeserializeObject<Gstr1Report>(System.IO.File.ReadAllText("seed" + Path.DirectorySeparatorChar + "gstr1.json"));
        //
        //           // return ConvertoSnakeCase(data);
        //           return Ok(data);
        //
        //        }
        // B2B

        [HttpGet("B2B")]
        public IActionResult GetB2B(int searchMonth, int year, int companyId)
        {
            var fromDate = new DateTime(year, searchMonth, 1);
            var toDate = fromDate.AddMonths(1).AddDays(-1);


            var customersB2B = _gstr1ReportingRepository.GetAllB2BInvoices(fromDate, toDate, companyId);

            return Ok(customersB2B);

        }

        //getting customerName at b2b
        [HttpGet("CustomerName")]
        public async Task<IActionResult> GetPlaceOfSupply(int searchMonth, int year, int companyId)

        {
            var fromDate = new DateTime(year, searchMonth, 1);
            var toDate = fromDate.AddMonths(1).AddDays(-1);
            var cusName = await _gstr1ReportingRepository.GetCustomerNameAtB2B(fromDate, toDate, companyId);
            var customerName = cusName.Select(c =>
             {

                 return new GetCustomerName
                 {
                     CustomerName = c.CustomerName,
                     Gstin = c.Gstin,
                 };
             }).ToList();

            return Ok(customerName);
        }

        // B2CL

        [HttpGet("B2CL")]
        public IActionResult GetB2Cl(int searchMonth, int year, int companyId)
        {
            var fromDate = new DateTime(year, searchMonth, 1);
            var toDate = fromDate.AddMonths(1).AddDays(-1);

            var posB2Cl = _gstr1ReportingRepository.GetB2ClInvoices(fromDate, toDate, companyId);



            return Ok(posB2Cl);
        }


        [HttpGet("B2C")]
        public IActionResult GetB2C(int searchMonth, int year, int companyId)
        {
            var fromDate = new DateTime(year, searchMonth, 1);
            var toDate = fromDate.AddMonths(1).AddDays(-1);
            var b2CInvoices = _gstr1ReportingRepository.GetAllB2CInvoices(fromDate, toDate, companyId);


            return Ok(b2CInvoices);
        }


        //credit debit note register reporting controller

        [HttpGet("CDNR")]
        public IActionResult GetCreditDebitNoteRegister(int searchMonth, int year, string companyId)
        {
            var fromDate = new DateTime(year, searchMonth, 1);
            var toDate = fromDate.AddMonths(1).AddDays(-1);

            var creditDebitNoteRegistered = _gstr1ReportingRepository.GetAllCdnRInvoices(fromDate, toDate, companyId);



            return Ok(creditDebitNoteRegistered);

        }
        //getting placeOfSupply at cdnr
        [HttpGet("PlaceOfSupplyCdnr")]
        public IActionResult GetPlaceOfSupplyAtCdnR(int searchMonth, int year, string companyId)
        {
            var fromDate = new DateTime(year, searchMonth, 1);
            var toDate = fromDate.AddMonths(1).AddDays(-1);
            var pos = _gstr1ReportingRepository.GetPlaceOfSupplyAtCdnr(fromDate, toDate, companyId);
            return Ok(pos);

        }


        // credit debit note unregistred

        [HttpGet("CDNUR")]
        public IActionResult GetCreditDebitNoteUnregister(int searchMonth, int year, string companyId)
        {
            var year1 = year;
            var fromDate = new DateTime(year1, searchMonth, 1);
            var toDate = fromDate.AddMonths(1).AddDays(-1);

            var creditDebitNoteUnregister = _gstr1ReportingRepository.GetAllCdnUrInvoices(fromDate, toDate, companyId);
            return Ok(creditDebitNoteUnregister);


        }

        //getting placeOfSupply at cdnur
        [HttpGet("PlaceOfSupplyCdnur")]
        public IActionResult GetPlaceOfSupplyAtCdnUr(int searchMonth, int year, string companyId)
        {
            var fromDate = new DateTime(year, searchMonth, 1);
            var toDate = fromDate.AddMonths(1).AddDays(-1);
            var pos = _gstr1ReportingRepository.GetPlaceOfSupplyAtCdnur(fromDate, toDate, companyId);
            return Ok(pos);

        }

        // advance receive reporting

        [HttpGet("AdvanceReceive")]
        public IActionResult GetAdvanceReceive(int searchMonth, int year, string companyId)
        {
            var year1 = year;
            var fromDate = new DateTime(year1, searchMonth, 1);
            var toDate = fromDate.AddMonths(1).AddDays(-1);
            var advanceReceives = _gstr1ReportingRepository.GetAllAdvanceReceives(fromDate, toDate, companyId);


            return Ok(advanceReceives);

        }


        // ad reporting

        [HttpGet("Hsn")]
        public async Task<IActionResult> GetHsnReporting(int searchMonth, int year, int companyId)
        {
            var year1 = DateTime.Now.Year;
            var fromDate = new DateTime(year1, searchMonth, 1);
            var toDate = fromDate.AddMonths(1).AddDays(-1);
            var customersHsnReport = await _gstr1ReportingRepository.GetAllHsnReport(fromDate, toDate, companyId);

            var indexList = customersHsnReport.Select((hsn, index) => new HsnReporting
            {




                Data = new HsnItemReport
                {
                    Num = index + 1,
                    HsnSc = hsn.Data.HsnSc,
                    Desc = hsn.Data.Desc,
                    Uqc = hsn.Data.Uqc,
                    Qty = hsn.Data.Qty,
                    Iamt = hsn.Data.Iamt,
                    Txval = hsn.Data.Txval,
                    Val = hsn.Data.Val,
                    Csamt = hsn.Data.Csamt,

                }



            });


            return Ok(indexList);

        }
        //document detail

        [HttpGet("DocumentDetail")]
        public async Task<IActionResult> GetDocumentDetail(int searchMonth, int year, string companyId)
        {

            var fromDate = new DateTime(year, searchMonth, 1);
            var toDate = fromDate.AddMonths(1).AddDays(-1);
            var customersDocumentDetail = await _gstr1ReportingRepository.GetDocumentDetail(fromDate, toDate, companyId);

            var indexList = customersDocumentDetail.Select((doc, index) => new Gstr1DocumentDetail
            {

                DocDet = new DocumentDet
                {
                    DocNum = index + 1,

                    Docs = new DocumentDetail
                    {

                        Num = index + 1,
                        From = doc.DocDet.Docs.From,
                        To = doc.DocDet.Docs.To,
                        NetIssue = doc.DocDet.Docs.NetIssue,
                        Totnum = doc.DocDet.Docs.Totnum,
                        Cancel = doc.DocDet.Docs.Cancel,   //To do check logic , may change

                    }

                }

            }).ToList();


            return Ok(indexList);
        }
        //export details

        [HttpGet("ExportInvoice")]

        public async Task<IActionResult> GetExportInvoice(int searchMonth, int year, string companyId)
        {
            var year1 = year;
            var fromDate = new DateTime(year1, searchMonth, 1);
            var toDate = fromDate.AddMonths(1).AddDays(-1);
            var customersExportInvoice = await _gstr1ReportingRepository.GetAllExportInvoices(fromDate, toDate, companyId);
            var indexList = customersExportInvoice.Select(exp =>
            {

                return new ExportReportingResource
                {
                    ExpTyp = exp.ExpTyp,

                    Inv = new ExportReport
                    {
                        Idt = exp.Inv.Idt,
                        Inum = exp.Inv.Inum,
                        Sbdt = exp.Inv.Sbdt,
                        Sbnum = exp.Inv.Sbnum,
                        Sbpcode = exp.Inv.Sbpcode,
                        Val = exp.Inv.Val,
                        Itms = exp.Inv.Itms.Select((item, index) => new Item
                        {
                            Num = index + 1,
                            ItmDet = new ItemDetail
                            {
                                Rt = item.ItmDet.Rt,
                                Txval = item.ItmDet.Txval,
                                Iamt = item.ItmDet.Iamt,

                            }

                        })
                    }
                };
            }).ToList();

            return Ok(indexList);

        }


        //Extra info of tenant for final reporting
        // [HttpGet("gstr1AccountingUnitInfo")]
        // public async Task<string> GetAccountingUnitInGstr1(string AccountingUnitId = "1")
        // {

        //     var accountingUnit = await _accountingUnitRepository.GetAsync(AccountingUnitId, AccountingUnitId);

        //     var gstr1AccountingUnit = new AccountingUnitInformationInFinalReporting
        //     {
        //         LegalNameOFCompany = accountingUnit.BusinessName,
        //         AuthorisedPerson = accountingUnit.AuthorizedRepresentativeName,
        //     };
        //     return ConvertoSnakeCase(gstr1AccountingUnit);
        // }

        //        GSTR1
        [HttpGet("gstr1")]
        public async Task<Gstr1Report> GetGstr1(int companyId, int searchMonth, int year)
        {
            var fromDate = new DateTime(year, searchMonth, 1);
            var toDate = fromDate.AddMonths(1).AddDays(-1);
            var fp = searchMonth.ToString() + year.ToString();

            // var tenant = await _accountingUnitRepository.GetAsync(AccountingUnitId);

            var b2Bs = _gstr1ReportingRepository.GetAllB2BInvoices(fromDate, toDate, companyId);
            var b2Cl = _gstr1ReportingRepository.GetB2ClInvoices(fromDate, toDate, companyId);
            var b2ClWithIndex = b2Cl.Select(pos => new Gstr1B2Cl
            {
                Pos = pos.Pos,
                Inv = pos.Inv.Select(si => new B2ClInvoice
                {
                    Inum = si.Inum,
                    Idt = si.Idt,
                    Val = si.Val,
                    Itms = si.Itms.Select((item, index) => new Item
                    {
                        Num = index + 1,
                        ItmDet = new ItemDetail
                        {
                            Rt = item.ItmDet.Rt,
                            Txval = item.ItmDet.Txval,
                            Iamt = item.ItmDet.Iamt,
                            Camt = item.ItmDet.Camt,
                            Samt = item.ItmDet.Samt,
                            Csamt = item.ItmDet.Csamt,
                        }
                    })
                })
            }).ToList();

            var cdnrs = _gstr1ReportingRepository.GetAllCdnRInvoices(fromDate, toDate, companyId.ToString());

            var cdnur = _gstr1ReportingRepository.GetAllCdnUrInvoices(fromDate, toDate, companyId.ToString());

            var advanceReceived = _gstr1ReportingRepository.GetAllAdvanceReceives(fromDate, toDate, companyId.ToString());


            var hsns = await _gstr1ReportingRepository.GetAllHsnReport(fromDate, toDate, companyId);
            var hsnsWithIndex = hsns.Select((hsn, index) => new HsnReporting
            {

                Data = new HsnItemReport
                {
                    Num = index + 1,
                    HsnSc = hsn.Data.HsnSc,
                    Desc = hsn.Data.Desc,
                    Uqc = hsn.Data.Uqc,
                    Qty = hsn.Data.Qty,
                    Iamt = hsn.Data.Iamt,
                    Txval = hsn.Data.Txval,
                    Val = hsn.Data.Val,
                    Csamt = hsn.Data.Csamt,

                }


            }).ToList();

            var exports = await _gstr1ReportingRepository.GetAllExportInvoices(fromDate, toDate, companyId.ToString());
            var exportWithIndex = exports.Select(exp =>
            {

                return new ExportReportingResource
                {
                    ExpTyp = exp.ExpTyp,
                    Inv = new ExportReport
                    {
                        Idt = exp.Inv.Idt,
                        Inum = exp.Inv.Inum,
                        Sbdt = exp.Inv.Sbdt,
                        Sbnum = exp.Inv.Sbnum,
                        Sbpcode = exp.Inv.Sbpcode,
                        Val = exp.Inv.Val,
                        Itms = exp.Inv.Itms.Select((item, index) => new Item
                        {
                            Num = index + 1,
                            ItmDet = new ItemDetail
                            {
                                Rt = item.ItmDet.Rt,
                                Txval = item.ItmDet.Txval,
                                Iamt = item.ItmDet.Iamt,

                            }

                        })
                    }
                };
            }).ToList();

            var gstr1 = new Gstr1Report
            {
                // Gstin = tenant.Gstin,
                Fp = fp,
                CurGt = 0,// double.Parse(tenant.CurrentGrossTurnOver),
                          //  Gt = tenant.TurnOver,//double.Parse(tenant.CurrentGrossTurnOver),

                B2b = b2Bs,
                B2cl = b2ClWithIndex,
                B2cs = _gstr1ReportingRepository.GetAllB2CInvoices(fromDate, toDate, companyId),
                Cdnr = cdnrs,
                Cdnur = cdnur,
                At = advanceReceived,
                Exp = exportWithIndex,
                Hsn = hsnsWithIndex,

            };

            return gstr1;
            //            return ConvertoSnakeCase(gstr1);

        }




        //snake case conversion from camel case

        private static string ConvertoSnakeCase(object obj)
        {
            var contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            };
            var json = JsonConvert.SerializeObject(obj, new JsonSerializerSettings
            {
                ContractResolver = contractResolver,
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore,
            });


            return json;
        }


    }
}


//for flattening nested query
//var res = customersB2B.SelectMany(b => b.Inv.SelectMany(c => c.Itms
//                                                 .Select(p => b.Ctin + ", " + c.Idt + ", " + p.Num)));

