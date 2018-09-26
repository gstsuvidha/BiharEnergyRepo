using BiharEnergy.Core.Domain;
using BiharEnergy.Core.Domain.Inferface;
using BiharEnergy.Core.Domain.Reporting;
using BiharEnergy.Core.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BiharEnergy.Persistence.Repository
{
    public class Gstr1Repository : IGstr1Repository
    {
        private readonly ApplicationDbContext _context;
        private readonly IQueryModelDatabase _database;

        public Gstr1Repository(ApplicationDbContext context, IQueryModelDatabase database)
        {
            _context = context;
            _database = database;
        }

        public IEnumerable<Gstr1B2B> GetAllB2BInvoices(DateTime frmDate, DateTime toDate, int companyId)
        {

            return _database.SalesInvoicesForCompany(Convert.ToInt32(companyId))
                                    .ForDateRange(frmDate, toDate)
                                    .ForInvoiceCategory(InvoiceCategory.B2B)
                                    .GroupBy(si => si.Customer)
                                    .Select(customerGrp => new Gstr1B2B
                                    {
                                        Ctin = customerGrp.Key.Gstin,
                                        Inv = customerGrp.Select(si => new B2BInvoice
                                        {
                                            Inum = si.InvoiceNumber.ToString(),
                                            Idt = si.Date.ToString("dd-MMM-yyyy", CultureInfo.InvariantCulture),
                                            Val = Math.Round(si.TotalInvoiceValue, 2),
                                            Pos = si.PlaceOfSupply,

                                            Itms = si.SalesInvoiceItems.GroupBy(i => i.TaxRate)
                                                        .Select((grouped, index) => new Item
                                                        {
                                                            Num = index + 1,
                                                            ItmDet = new ItemDetail
                                                            {
                                                                Rt = grouped.Key,
                                                                Txval = grouped.Sum(i => i.TaxableValue),
                                                                Iamt = Math.Round(grouped.Sum(i => i.IgstAmount), 2),
                                                                Camt = Math.Round(grouped.Sum(i => i.CgstAmount), 2),
                                                                Samt = Math.Round(grouped.Sum(i => i.SgstAmount), 2),
                                                                Csamt = Math.Round(grouped.Sum(i => i.Cess), 2)
                                                            }
                                                        })
                                        })


                                    }).ToList();
        }




        public async Task<IEnumerable<GetCustomerName>> GetCustomerNameAtB2B(DateTime frmDate, DateTime toDate, int companyId)
        {
            var query = _context.SalesInvoices.Include(c => c.Customer).Include(si => si.AccountingUnit);

            return await query.Where(cus => cus.AccountingUnit.CompanyId == companyId && cus.Date >= frmDate && cus.Date <= toDate).Select(cu => new GetCustomerName
            {
                CustomerName = cu.Customer.Name,
                Gstin = cu.Customer.Gstin
            }).ToListAsync();

        }


        public IEnumerable<Gstr1B2Cl> GetB2ClInvoices(DateTime frmDate, DateTime toDate, int companyId)
        {
            return _database.SalesInvoicesForCompany(companyId)
                                .ForDateRange(frmDate, toDate)
                                .ForInvoiceCategory(InvoiceCategory.B2CL)
                                .GroupBy(si => si.PlaceOfSupply)
                                .Select(pos => new Gstr1B2Cl
                                {

                                    Pos = pos.Key,
                                    Inv = pos.Select(si => new B2ClInvoice
                                    {
                                        Inum = si.InvoiceNumber,
                                        Idt = si.Date.ToString("dd-MMM-yyyy", CultureInfo.InvariantCulture),
                                        Val = Math.Round(si.TotalInvoiceValue, 2),
                                        Itms = si.SalesInvoiceItems.GroupBy(i => i.TaxRate).Select((grouped, index) => new Item
                                        {
                                            Num = index + 1,
                                            ItmDet = new ItemDetail
                                            {
                                                Rt = grouped.Key,
                                                Txval = grouped.Sum(i => i.TaxableValue),
                                                Iamt = Math.Round(grouped.Sum(i => i.IgstAmount), 2),
                                                Camt = Math.Round(grouped.Sum(i => i.CgstAmount), 2),
                                                Samt = Math.Round(grouped.Sum(i => i.SgstAmount), 2),
                                                Csamt = Math.Round(grouped.Sum(i => i.Cess), 2)
                                            }

                                        })
                                    })
                                }).ToList();
        }



        //B2C Repository

        public IEnumerable<Gstr1B2C> GetAllB2CInvoices(DateTime frmDate, DateTime toDate, int companyId)
        {

            return _database.SalesInvoicesForCompany(companyId)
                                .ForDateRange(frmDate, toDate)
                                .ForInvoiceCategory(InvoiceCategory.B2C)
                                .SelectMany(si => si.SalesInvoiceItems)
                                .GroupBy(si => new { si.SalesInvoice.SupplyType, si.SalesInvoice.PlaceOfSupply, si.TaxRate })
                                .Select(g => new Gstr1B2C
                                {
                                    SplyTy = g.Key.SupplyType.ToString(),
                                    Pos = g.Key.PlaceOfSupply,
                                    Rt = g.Key.TaxRate,
                                    Txval = g.Sum(i => i.TaxableValue),
                                    Iamt = Math.Round(g.Sum(item => item.IgstAmount), 2),
                                    Camt = Math.Round(g.Sum(item => item.CgstAmount), 2),
                                    Samt = Math.Round(g.Sum(item => item.SgstAmount), 2),
                                    Csamt = g.Sum(i => i.Cess)
                                }).ToList();
        }



        public IEnumerable<Gstr1CreditDebitNoteRegistered> GetAllCdnRInvoices(DateTime frmDate, DateTime toDate, string companyId)
        {
            return _database.IssueNotesFor(companyId)
                                    .ForDateRange(frmDate, toDate)
                                    .ForInvoiceCategory(InvoiceCategory.B2B)
                                    .GroupBy(nt => nt.Customer)
                                    .Select(customerGrouped => new Gstr1CreditDebitNoteRegistered
                                    {
                                        Ctin = customerGrouped.Key.Gstin,


                                        Nt = customerGrouped.Select(cdnR => new CreditDebitRegisteredNote
                                        {

                                            Inum = cdnR.OriginalInvoiceNumber.ToString(),
                                            Idt = cdnR.OriginalInvoiceDate.ToString("dd-MMM-yyyy", CultureInfo.InvariantCulture),
                                            Val = Math.Round(cdnR.OriginalInvoiceValue, 2),
                                            NtNum = cdnR.IssueNoteNumber.ToString(),
                                            NtDt = cdnR.Date.ToString("dd-MMM-yyyy", CultureInfo.InvariantCulture),
                                            Ntty = cdnR.NoteType.ToString(),
                                            Rsn = cdnR.Reason,
                                            PGst = cdnR.Pgst,
                                            Itms = cdnR.IssueNoteItems.GroupBy(i => i.TaxRate).Select((grouped, index) => new Item
                                            {
                                                Num = index + 1,
                                                ItmDet = new ItemDetail
                                                {
                                                    Rt = grouped.Key,
                                                    Txval = grouped.Sum(i => i.TaxableValue),
                                                    Iamt = Math.Round(grouped.Sum(i => i.IgstAmount), 2),
                                                    Camt = Math.Round(grouped.Sum(i => i.CgstAmount), 2),
                                                    Samt = Math.Round(grouped.Sum(i => i.SgstAmount), 2),
                                                    Csamt = Math.Round(grouped.Sum(i => i.CessAmount), 2),
                                                }
                                            })
                                        })


                                    }).ToList();
        }

        private static Expression<Func<T, bool>> ForInvoiceCategory<T>(InvoiceCategory invoiceCategory) where T : IHaveInvoiceCategory
        {
            return cat => cat.InvoiceCategory == invoiceCategory;
            //                .Where(ForInvoiceCategory<IssueNote>(InvoiceCategory.B2CL)
            //                .Or(ForInvoiceCategory<IssueNote>(InvoiceCategory.B2C)))

        }


        public IEnumerable<PlaceOfSupplyCdnr> GetPlaceOfSupplyAtCdnr(DateTime frmDate, DateTime toDate, string companyId)
        {

            return _database.IssueNotesFor(companyId)
                                    .ForDateRange(frmDate, toDate)
                                    .ForInvoiceCategories(cdnr => cdnr.InvoiceCategory == InvoiceCategory.B2B)
                .Select(cdnr => new PlaceOfSupplyCdnr
                {
                    Pos = cdnr.Customer.State,
                    Inum = cdnr.OriginalInvoiceNumber.ToString(),

                }).ToList();
        }




        public IEnumerable<Gstr1CreditDebitNoteUnregistered> GetAllCdnUrInvoices(DateTime frmDate, DateTime toDate, string companyId)
        {

            return _database.IssueNotesFor(companyId)
                                    .ForDateRange(frmDate, toDate)
                                    .ForInvoiceCategories(cdnUr => cdnUr.InvoiceCategory == InvoiceCategory.B2C
                                                                || cdnUr.InvoiceCategory == InvoiceCategory.B2CL)
                .Select(cdnur => new Gstr1CreditDebitNoteUnregistered
                {
                    NtNum = cdnur.IssueNoteNumber.ToString(),

                    NtDt = cdnur.Date.ToString("dd-MMM-yyyy", CultureInfo.InvariantCulture),
                    Ntty = cdnur.NoteType.ToString(),
                    Rsn = cdnur.Reason,
                    PGst = cdnur.Pgst.ToString(),
                    Typ = cdnur.InvoiceCategory.ToString(),

                    Inum = cdnur.OriginalInvoiceNumber.ToString(),
                    Idt = cdnur.OriginalInvoiceDate.ToString("dd-MMM-yyyy", CultureInfo.InvariantCulture),
                    Val = Math.Round(cdnur.OriginalInvoiceValue, 2),


                    Itms = cdnur.IssueNoteItems.GroupBy(i => i.TaxRate)
                                                .Select(grouped => new Item
                                                //.Select((grouped, index) => new Item
                                                {
                                                    // Num = index + 1,
                                                    ItmDet = new ItemDetail
                                                    {
                                                        Rt = grouped.Key,
                                                        Txval = grouped.Sum(i => i.TaxableValue),
                                                        Iamt = Math.Round(grouped.Sum(i => i.IgstAmount), 2),
                                                        Camt = Math.Round(grouped.Sum(i => i.CgstAmount), 2),
                                                        Samt = Math.Round(grouped.Sum(i => i.SgstAmount), 2),
                                                        Csamt = Math.Round(grouped.Sum(i => i.CessAmount), 2),
                                                    }
                                                })



                }).ToList();
        }

        public IEnumerable<PlaceOfSupplyCdnur> GetPlaceOfSupplyAtCdnur(DateTime frmDate, DateTime toDate, string companyId)
        {

            return _database.IssueNotesFor(companyId)
                                    .ForDateRange(frmDate, toDate)
                                    .ForInvoiceCategories(cdnUr => cdnUr.InvoiceCategory == InvoiceCategory.B2C
                                                                || cdnUr.InvoiceCategory == InvoiceCategory.B2CL)
                .Select(cdnur => new PlaceOfSupplyCdnur
                {
                    Pos = cdnur.Customer.State,
                    Inum = cdnur.OriginalInvoiceNumber.ToString(),

                }).ToList();
        }





        public IEnumerable<Gstr1AdvanceReceivedResources> GetAllAdvanceReceives(DateTime frmDate, DateTime toDate, string companyId)
        {

            return _database.AdvanceReceivesFor(companyId)
                                    .ForDateRange(frmDate, toDate)
                                    .GroupBy(adb => new { adb.SupplyType, adb.PlaceOfSupply })
                                    .Select(ad => new Gstr1AdvanceReceivedResources
                                    {
                                        Pos = ad.Key.PlaceOfSupply,
                                        SplyTy = ad.Key.SupplyType.ToString(),

                                        Itms = ad.Select(ar => ar)
                                                    .SelectMany(ar => ar.AdvanceReceivedItems)
                                                    .GroupBy(i => i.TaxRate)
                                                    .Select((grouped, index) => new AdvanceItem
                                                    {
                                                        Num = index + 1,
                                                        ItmDet = new AdvanceItemDetail
                                                        {
                                                            Rt = grouped.Key,
                                                            AdAmt = grouped.Sum(i => i.TaxableValue),
                                                            Iamt = Math.Round(grouped.Sum(i => i.IgstAmount), 2),
                                                            Camt = Math.Round(grouped.Sum(i => i.CgstAmount), 2),
                                                            Samt = Math.Round(grouped.Sum(i => i.SgstAmount), 2),
                                                            Csamt = Math.Round(grouped.Sum(i => i.CessAmount), 2),
                                                        }
                                                    })

                                    }).ToList();
        }






        public async Task<TaxSummary> GetB2BTotalDashboardSummary(DateTime frmDate, DateTime toDate, string companyId)
        {

            return await _database.SalesInvoicesFor(companyId)
                                    .ForDateRange(frmDate, toDate)
                                    .ForInvoiceCategory(InvoiceCategory.B2B)
                                    .GroupBy(si => 1)
                                    .Select(si => new TaxSummary
                                    {
                                        TotalTaxableValue = si.Sum(tv => tv.TotalTaxableValue),
                                        TotalTaxAmount = si.Sum(ta => ta.TotalTaxAmount)


                                    }).SingleOrDefaultAsync();



        }



        public async Task<IEnumerable<HsnReporting>> GetAllHsnReport(DateTime frmDate, DateTime toDate, int companyId)
        {
            var query = _context.SalesInvoiceItems.Include(si => si.SalesInvoice).Include(pro => pro.Product);

            return await query.Where(si => si.SalesInvoice.AccountingUnit.CompanyId == companyId
            && si.SalesInvoice.Date >= frmDate
            && si.SalesInvoice.Date <= toDate)


                .GroupBy(grouped => new

                {
                    grouped.Product.HsnSacCode,
                    //grouped.Product.Name,
                    grouped.Product.Unit


                }



                ).

                Select(hsn => new HsnReporting
                {
                    Data = new HsnItemReport
                    {
                        Iamt = hsn.Sum(hsnsac => Math.Round(hsnsac.TaxAmount, 2)),

                        Csamt = hsn.Sum(hsnsac => Math.Round(hsnsac.Cess, 2)),
                        HsnSc = hsn.Key.HsnSacCode,
                        //Desc=hsn.Key.Name,
                        Uqc = hsn.Key.Unit,
                        Txval = hsn.Sum(hsnsac => Math.Round(hsnsac.TaxableValue, 2)),
                        Qty = hsn.Sum(hsnsac => hsnsac.Quantity),
                        Val = hsn.Sum(hsnsac => Math.Round(hsnsac.Amount, 2)),


                    }

                }).ToListAsync();

        }




        public async Task<IEnumerable<Gstr1DocumentDetail>> GetDocumentDetail(DateTime frmDate, DateTime toDate, string companyId)
        {
            var query = _context.SalesInvoices.GroupBy(si => 1);

            var maximumInvoiceNumber = _context.SalesInvoices.Where(si => si.AccountingUnitId == companyId
                                           && si.Date >= frmDate
                                           && si.Date <= toDate).Max(inv => inv.InvoiceNumber);


            var minimumInvoiceNumber = _context.SalesInvoices.Where(si => si.AccountingUnitId == companyId
                                           && si.Date >= frmDate
                                           && si.Date <= toDate).Min(inv => inv.InvoiceNumber);


            var countInvoiceNumber = _context.SalesInvoices.Where(si => si.AccountingUnitId == companyId
                                           && si.Date >= frmDate
                                           && si.Date <= toDate).Count();


            var maximumInvoiceNumberWithoutDate = _context.SalesInvoices.Select(inv => inv.InvoiceNumber).Max();


            //var maximumCountInvoiceNumberWithoutDate = _context.SalesInvoices.Select(inv => inv.InvoiceNumber).Count();




            return await query.Select(doc => new Gstr1DocumentDetail
            {
                DocDet = new DocumentDet
                {
                    Docs = new DocumentDetail
                    {
                        From = minimumInvoiceNumber,
                        To = maximumInvoiceNumber,
                        NetIssue = maximumInvoiceNumberWithoutDate,
                        Totnum = countInvoiceNumber,
                        Cancel = ((maximumInvoiceNumber - minimumInvoiceNumber) + 1) - countInvoiceNumber



                    }

                }


            }).ToListAsync();

        }

        //Export Invoice repository


        public async Task<IEnumerable<ExportReportingResource>> GetAllExportInvoices(DateTime frmDate, DateTime toDate, string companyId)
        {

            var query = _context.ExportInvoices.Include(i => i.ExportInvoiceItems);

            return await query.Where(exp => exp.AccountingUnitId == companyId && exp.Date >= frmDate && exp.Date <= toDate).Select(expt => new ExportReportingResource
            {
                ExpTyp = expt.ExportType.ToString(),

                Inv = new ExportReport
                {
                    Inum = expt.InvoiceNumber.ToString(),
                    Idt = expt.Date.ToString("dd-MMM-yyyy", CultureInfo.InvariantCulture),
                    Val = Math.Round(expt.TotalInvoiceValue, 2),
                    Sbpcode = expt.PortCode.ToString(),
                    Sbdt = expt.ShippingDate.ToString("dd-MMM-yyyy", CultureInfo.InvariantCulture),
                    Sbnum = expt.ShippingBillNumber.ToString(),





                    Itms = expt.ExportInvoiceItems.GroupBy(i => i.TaxRate).Select(grouped => new Item
                    {
                        ItmDet = new ItemDetail
                        {
                            Rt = grouped.Key,
                            Txval = grouped.Sum(i => i.TaxableValue),
                            Iamt = Math.Round(grouped.Sum(i => i.TaxAmount), 2),
                        }
                    })
                }


            }).ToListAsync();
        }




    }
}
