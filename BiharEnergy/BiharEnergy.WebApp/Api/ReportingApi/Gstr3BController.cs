using AutoMapper;
using BiharEnergy.Core.Domain.SalesInvoiceModule;
using BiharEnergy.Core.Enums;
using BiharEnergy.Persistence;
using BiharEnergy.Persistence.QueryFilter.Filters;
using BiharEnergy.Core.Domain.Reporting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using BiharEnergy.WebApp.Api.AccountingUnitApi;
using BiharEnergy.Core.Domain.Accounting;

namespace BiharEnergy.WebApp.Api.ReportingApi
{
    [Produces("application/json")]
    [Route("api/Gstr3b")]
    public class Gstr3BController : AccountingUnitResolverController
    {
        private readonly IQueryModelDatabase _database;
        private readonly IAccountingUnitRepository _accountingUnitRepository;


        public Gstr3BController(
            IQueryModelDatabase database, IMapper mapper, IAccountingUnitRepository accountingUnitRepository, IUnitOfWork uow) : base(database, mapper, uow,accountingUnitRepository)
        {
            _database = database;
            _accountingUnitRepository = accountingUnitRepository;

        }

        [HttpGet("Gstr3BDetails")]
        public IActionResult Index(int searchMonth,int year,int companyId)
        {
            
            var frmDate = new DateTime(year, searchMonth, 1);
            var toDate = frmDate.AddMonths(1).AddDays(-1);
            var fp = searchMonth.ToString("00") + year.ToString();

            var accountingUnit = _accountingUnitRepository.GetAsync(AccountingUnitId).Result;
            var userprofile = _accountingUnitRepository.GetAsync(AccountingUnitId).Result;
            var company = _database.Companies.SingleOrDefault(cp => cp.Id == companyId);

            return Ok(new Gstr3B
            {
                Gstin = accountingUnit.Gstin,
                LegalNameOfTheRegisteredPerson = accountingUnit.BusinessName,
                Fp = fp,
                OutwardSupplyOtherThanExempted = OutwardSupplyOtherThanExempted(frmDate, toDate, companyId),
                OtherOutwardSupplyNilRatedExempted = OtherOutwardSupplyNilRatedExempted(frmDate, toDate, companyId),
                Unregistered = Unregistered(frmDate, toDate, companyId),
                Composite = Composite(frmDate, toDate,companyId),
                AllOtherItc = AllOtherItc(frmDate, toDate, companyId),

            });
        }


        private SupplyDetail3B OutwardSupplyOtherThanExempted(DateTime frmDate, DateTime toDate, int companyId)

        {
            
            var outwardSales = _database.SalesInvoicesForCompany(companyId)
                                .ForDateRange(frmDate, toDate)
                                .GroupBy(si => 1)
                                .Select(si => new SupplyDetail3B
                                {
                                    TotalTaxableValue = si.Sum(inv => inv.SalesInvoiceItems.Where(itm => itm.TaxRate > 0).Sum(itm => itm.TaxableValue)),
                                    Igst = si.Sum(inv => inv.SalesInvoiceItems.Where(itm => itm.TaxRate > 0).Sum(itm => itm.IgstAmount)),
                                    Cgst = si.Sum(inv => inv.SalesInvoiceItems.Where(itm => itm.TaxRate > 0).Sum(itm => itm.CgstAmount)),
                                    Sgst = si.Sum(inv => inv.SalesInvoiceItems.Where(itm => itm.TaxRate > 0).Sum(itm => itm.SgstAmount)),

                                }).SingleOrDefault();

            var outwardAdvanceReceived = _database.AdvanceReceivesForCompany(companyId)
                                                .ForDateRange(frmDate, toDate)
                                                .GroupBy(si => 1)
                                                .Select(si => new SupplyDetail3B
                                                {
                                                    TotalTaxableValue = si.Sum(inv => inv.AdvanceReceivedItems.Where(itm => itm.TaxRate > 0).Sum(itm => itm.TaxableValue)),
                                                    Igst = si.Sum(inv => inv.AdvanceReceivedItems.Where(itm => itm.TaxRate > 0).Sum(itm => itm.IgstAmount)),
                                                    Cgst = si.Sum(inv => inv.AdvanceReceivedItems.Where(itm => itm.TaxRate > 0).Sum(itm => itm.CgstAmount)),
                                                    Sgst = si.Sum(inv => inv.AdvanceReceivedItems.Where(itm => itm.TaxRate > 0).Sum(itm => itm.SgstAmount)),

                                                }).SingleOrDefault();

            var outwardIssueNoteCredit = _database.IssueNotesForCompany(companyId)
                                                .OfNoteType(NoteType.Credit)
                                                .ForDateRange(frmDate, toDate)
                                                .GroupBy(si => 1)
                                                .Select(si => new SupplyDetail3B
                                                {
                                                    TotalTaxableValue = si.Sum(inv => inv.IssueNoteItems.Where(itm => itm.TaxRate > 0).Sum(itm => itm.TaxableValue)),
                                                    Igst = si.Sum(inv => inv.IssueNoteItems.Where(itm => itm.TaxRate > 0).Sum(itm => itm.IgstAmount)),
                                                    Cgst = si.Sum(inv => inv.IssueNoteItems.Where(itm => itm.TaxRate > 0).Sum(itm => itm.CgstAmount)),
                                                    Sgst = si.Sum(inv => inv.IssueNoteItems.Where(itm => itm.TaxRate > 0).Sum(itm => itm.SgstAmount)),

                                                }).SingleOrDefault();

            var outwardIssueNoteDebit = _database.IssueNotesForCompany(companyId)
                                                .OfNoteType(NoteType.Debit)
                                                .ForDateRange(frmDate, toDate)
                                                .GroupBy(si => 1)
                                                .Select(si => new SupplyDetail3B
                                                {
                                                    TotalTaxableValue = si.Sum(inv => inv.IssueNoteItems.Where(itm => itm.TaxRate > 0).Sum(itm => itm.TaxableValue)),
                                                    Igst = si.Sum(inv => inv.IssueNoteItems.Where(itm => itm.TaxRate > 0).Sum(itm => itm.IgstAmount)),
                                                    Cgst = si.Sum(inv => inv.IssueNoteItems.Where(itm => itm.TaxRate > 0).Sum(itm => itm.CgstAmount)),
                                                    Sgst = si.Sum(inv => inv.IssueNoteItems.Where(itm => itm.TaxRate > 0).Sum(itm => itm.SgstAmount)),

                                                }).SingleOrDefault();
            var outwardSupplyOtherThanExempted = new SupplyDetail3B();

            outwardSupplyOtherThanExempted.Add(outwardSales);
            outwardSupplyOtherThanExempted.Add(outwardAdvanceReceived);
            outwardSupplyOtherThanExempted.Substract(outwardIssueNoteCredit);
            outwardSupplyOtherThanExempted.Add(outwardIssueNoteDebit);

            return outwardSupplyOtherThanExempted;
        }
        private SupplyDetail3B OtherOutwardSupplyNilRatedExempted(DateTime frmDate, DateTime toDate, int companyId)
        {
            var outwardSales = _database.SalesInvoicesForCompany(companyId)
                                         .ForDateRange(frmDate, toDate)
                                         .GroupBy(si => 1)
                                         .Select(si => new SupplyDetail3B
                                         {
                                             TotalTaxableValue = si.Sum(inv => inv.SalesInvoiceItems.Where(itm => itm.TaxRate == 0).Sum(itm => itm.TaxableValue)),
                                             Igst = si.Sum(inv => inv.SalesInvoiceItems.Where(itm => itm.TaxRate == 0).Sum(itm => itm.IgstAmount)),
                                             Cgst = si.Sum(inv => inv.SalesInvoiceItems.Where(itm => itm.TaxRate == 0).Sum(itm => itm.CgstAmount)),
                                             Sgst = si.Sum(inv => inv.SalesInvoiceItems.Where(itm => itm.TaxRate == 0).Sum(itm => itm.SgstAmount)),
                                             Cess = si.Sum(inv => inv.SalesInvoiceItems.Where(itm => itm.TaxRate == 0).Sum(itm => itm.Cess))

                                         }).SingleOrDefault();

            var outwardAdvanceReceived = _database.AdvanceReceivesForCompany(companyId)
                                                .ForDateRange(frmDate, toDate)
                                                .GroupBy(si => 1)
                                                .Select(si => new SupplyDetail3B
                                                {
                                                    TotalTaxableValue = si.Sum(inv => inv.AdvanceReceivedItems.Where(itm => itm.TaxRate == 0).Sum(itm => itm.TaxableValue)),
                                                    Igst = si.Sum(inv => inv.AdvanceReceivedItems.Where(itm => itm.TaxRate == 0).Sum(itm => itm.IgstAmount)),
                                                    Cgst = si.Sum(inv => inv.AdvanceReceivedItems.Where(itm => itm.TaxRate == 0).Sum(itm => itm.CgstAmount)),
                                                    Sgst = si.Sum(inv => inv.AdvanceReceivedItems.Where(itm => itm.TaxRate == 0).Sum(itm => itm.SgstAmount)),
                                                    Cess = si.Sum(inv => inv.AdvanceReceivedItems.Where(itm => itm.TaxRate == 0).Sum(itm => itm.CessAmount))

                                                }).SingleOrDefault();

            var outwardIssueNoteCredit = _database.IssueNotesForCompany(companyId)
                                                .OfNoteType(NoteType.Credit)
                                                .ForDateRange(frmDate, toDate)
                                                .GroupBy(si => 1)
                                                .Select(si => new SupplyDetail3B
                                                {
                                                    TotalTaxableValue = si.Sum(inv => inv.IssueNoteItems.Where(itm => itm.TaxRate == 0).Sum(itm => itm.TaxableValue)),
                                                    Igst = si.Sum(inv => inv.IssueNoteItems.Where(itm => itm.TaxRate == 0).Sum(itm => itm.IgstAmount)),
                                                    Cgst = si.Sum(inv => inv.IssueNoteItems.Where(itm => itm.TaxRate == 0).Sum(itm => itm.CgstAmount)),
                                                    Sgst = si.Sum(inv => inv.IssueNoteItems.Where(itm => itm.TaxRate == 0).Sum(itm => itm.SgstAmount)),
                                                    Cess = si.Sum(inv => inv.IssueNoteItems.Where(itm => itm.TaxRate == 0).Sum(itm => itm.CessAmount))

                                                }).SingleOrDefault();

            var outwardIssueNoteDebit = _database.IssueNotesForCompany(companyId)
                                                .OfNoteType(NoteType.Debit)
                                                .ForDateRange(frmDate, toDate)
                                                .GroupBy(si => 1)
                                                .Select(si => new SupplyDetail3B
                                                {
                                                    TotalTaxableValue = si.Sum(inv => inv.IssueNoteItems.Where(itm => itm.TaxRate == 0).Sum(itm => itm.TaxableValue)),
                                                    Igst = si.Sum(inv => inv.IssueNoteItems.Where(itm => itm.TaxRate == 0).Sum(itm => itm.IgstAmount)),
                                                    Cgst = si.Sum(inv => inv.IssueNoteItems.Where(itm => itm.TaxRate == 0).Sum(itm => itm.CgstAmount)),
                                                    Sgst = si.Sum(inv => inv.IssueNoteItems.Where(itm => itm.TaxRate == 0).Sum(itm => itm.SgstAmount)),
                                                    Cess = si.Sum(inv => inv.IssueNoteItems.Where(itm => itm.TaxRate == 0).Sum(itm => itm.CessAmount))

                                                }).SingleOrDefault();


            var outwardSupplyOtherThanExempted = new SupplyDetail3B();

            outwardSupplyOtherThanExempted.Add(outwardSales);
            outwardSupplyOtherThanExempted.Add(outwardAdvanceReceived);
            outwardSupplyOtherThanExempted.Substract(outwardIssueNoteCredit);
            outwardSupplyOtherThanExempted.Add(outwardIssueNoteDebit);

            return outwardSupplyOtherThanExempted;

        }

        private IEnumerable<InterStateSupply> Unregistered(DateTime frmDate, DateTime toDate, int companyId)
        {
            var sales = _database.SalesInvoicesForCompany(companyId)
                                                .Where(si => si.SupplyType == SupplyType.InterState)
                                                .Where(si => si.CustomerId == null
                                                           || (si.Customer != null && si.Customer.RegistrationType == RegistrationType.Unregistered))
                                                .ForDateRange(frmDate, toDate)
                                                .GroupBy(si => si.PlaceOfSupply)
                                                .Select(si => new InterStateSupply
                                                {
                                                    PlaceOfSupply = si.Key,
                                                    TotalTaxableValue = si.Sum(inv => inv.TotalTaxableValue),
                                                    Igst = si.Sum(inv => inv.SalesInvoiceItems.Sum(itm => itm.IgstAmount)),
                                                }).ToList();

            var adRec = _database.AdvanceReceivesForCompany(companyId)
                                                                .Where(si => si.SupplyType == SupplyType.InterState)
                                                                .Where(si => si.CustomerId == null
                                                                           || (si.Customer != null && si.Customer.RegistrationType == RegistrationType.Unregistered))
                                                                .ForDateRange(frmDate, toDate)
                                                                .GroupBy(si => si.PlaceOfSupply)
                                                                .Select(si => new InterStateSupply
                                                                {
                                                                    PlaceOfSupply = si.Key,
                                                                    TotalTaxableValue = si.Sum(inv => inv.TotalTaxableValue),
                                                                    Igst = si.Sum(inv => inv.AdvanceReceivedItems.Sum(itm => itm.IgstAmount)),
                                                                }).ToList();
            var credit = _database.IssueNotesForCompany(companyId)
                                                        .OfNoteType(NoteType.Credit)
                                                                .Where(si => si.SupplyType == SupplyType.InterState)
                                                                .Where(si => si.CustomerId == null
                                                                           || (si.Customer != null && si.Customer.RegistrationType == RegistrationType.Unregistered))
                                                                .ForDateRange(frmDate, toDate)
                                                                .GroupBy(si => si.PlaceOfSupply)
                                                                .Select(si => new InterStateSupply
                                                                {
                                                                    PlaceOfSupply = si.Key,
                                                                    TotalTaxableValue = -si.Sum(inv => inv.TotalTaxableValue),
                                                                    Igst = -si.Sum(inv => inv.IssueNoteItems.Sum(itm => itm.IgstAmount)),
                                                                }).ToList();

            var debit = _database.IssueNotesForCompany(companyId)
                                                        .OfNoteType(NoteType.Debit)
                                                        .Where(si => si.SupplyType == SupplyType.InterState)
                                                        .Where(si => si.CustomerId == null
                                                                     || (si.Customer != null && si.Customer.RegistrationType == RegistrationType.Unregistered))
                                                        .ForDateRange(frmDate, toDate)
                                                        .GroupBy(si => si.PlaceOfSupply)
                                                        .Select(si => new InterStateSupply
                                                        {
                                                            PlaceOfSupply = si.Key,
                                                            TotalTaxableValue = si.Sum(inv => inv.TotalTaxableValue),
                                                            Igst = si.Sum(inv => inv.IssueNoteItems.Sum(itm => itm.IgstAmount)),
                                                        }).ToList();


            var unregisteresAggregate = new List<InterStateSupply>();
            unregisteresAggregate.AddRange(sales);
            unregisteresAggregate.AddRange(adRec);
            unregisteresAggregate.AddRange(credit);
            unregisteresAggregate.AddRange(debit);



            return unregisteresAggregate.GroupBy(ur => ur.PlaceOfSupply).Select(ur => new InterStateSupply
            {
                PlaceOfSupply = ur.Key,
                TotalTaxableValue = ur.Sum(inv => inv.TotalTaxableValue),
                Igst = ur.Sum(inv => inv.Igst)
            }).ToList();
        }

        private IEnumerable<InterStateSupply> Composite(DateTime frmDate, DateTime toDate, int companyId)
        {
            var compositeSales = _database.SalesInvoicesForCompany(companyId)
                                                .Where(si => si.SupplyType == SupplyType.InterState)
                                                .Where(si => si.Customer.RegistrationType == RegistrationType.CompositeDealer)
                                                .ForDateRange(frmDate, toDate)
                                                .GroupBy(si => si.PlaceOfSupply)
                                                .Select(si => new InterStateSupply
                                                {
                                                    PlaceOfSupply = si.Key,
                                                    TotalTaxableValue = si.Sum(inv => inv.TotalTaxableValue),
                                                    Igst = si.Sum(inv => inv.SalesInvoiceItems.Sum(itm => itm.IgstAmount)),
                                                }).ToList();

            var compositeAdRec = _database.AdvanceReceivesForCompany(companyId)
                                                .Where(si => si.SupplyType == SupplyType.InterState)
                                                .Where(si => si.Customer.RegistrationType == RegistrationType.CompositeDealer)
                                                .ForDateRange(frmDate, toDate)
                                                .GroupBy(si => si.PlaceOfSupply)
                                                .Select(si => new InterStateSupply
                                                {
                                                    PlaceOfSupply = si.Key,
                                                    TotalTaxableValue = si.Sum(inv => inv.TotalTaxableValue),
                                                    Igst = si.Sum(inv => inv.AdvanceReceivedItems.Sum(itm => itm.IgstAmount)),
                                                }).ToList();

            var compositeCredit = _database.IssueNotesForCompany(companyId)
                                                        .OfNoteType(NoteType.Credit)
                                                                .Where(si => si.SupplyType == SupplyType.InterState)
                                                                .Where(si => si.Customer.RegistrationType == RegistrationType.CompositeDealer)
                                                                .ForDateRange(frmDate, toDate)
                                                                .GroupBy(si => si.PlaceOfSupply)
                                                                .Select(si => new InterStateSupply
                                                                {
                                                                    PlaceOfSupply = si.Key,
                                                                    TotalTaxableValue = -si.Sum(inv => inv.TotalTaxableValue),
                                                                    Igst = -si.Sum(inv => inv.IssueNoteItems.Sum(itm => itm.IgstAmount)),

                                                                }).ToList();

            var compositeDebit = _database.IssueNotesForCompany(companyId)

                                                        .OfNoteType(NoteType.Debit)
                                                                .Where(si => si.SupplyType == SupplyType.InterState)
                                                                .Where(si => si.Customer.RegistrationType == RegistrationType.CompositeDealer)
                                                                .ForDateRange(frmDate, toDate)
                                                                .GroupBy(si => si.PlaceOfSupply)
                                                                .Select(si => new InterStateSupply
                                                                {
                                                                    PlaceOfSupply = si.Key,
                                                                    TotalTaxableValue = -si.Sum(inv => inv.TotalTaxableValue),
                                                                    Igst = -si.Sum(inv => inv.IssueNoteItems.Sum(itm => itm.IgstAmount)),
                                                                }).ToList();


            var compositeAggregate = new List<InterStateSupply>();
            compositeAggregate.AddRange(compositeSales);
            compositeAggregate.AddRange(compositeAdRec);
            compositeAggregate.AddRange(compositeCredit);
            compositeAggregate.AddRange(compositeDebit);



            return compositeAggregate.GroupBy(ur => ur.PlaceOfSupply).Select(ur => new InterStateSupply
            {
                PlaceOfSupply = ur.Key,
                TotalTaxableValue = ur.Sum(inv => inv.TotalTaxableValue),
                Igst = ur.Sum(inv => inv.Igst)
            }).ToList();
        }

        private SupplyDetail3B AllOtherItc(DateTime frmDate, DateTime toDate, int companyId)

        {
            var purchase = _database.PurchaseInvoicesForCompany(companyId)
                                .ForDateRange(frmDate, toDate)

                                .Where(si => si.Supplier.RegistrationType == RegistrationType.Registered ||  si.Supplier.RegistrationType == RegistrationType.CompositeDealer )
                                .GroupBy(si => 1)
                                .Select(si => new SupplyDetail3B
                                {
                                    TotalTaxableValue = si.Sum(itm => itm.TotalTaxableValue),
                                    Igst = si.Sum(inv => inv.PurchaseInvoiceItems.Where(itm => itm.TaxRate >= 0).Sum(itm => itm.IgstAmount * itm.Product.ItcPercentage/100)),
                                    Cgst = si.Sum(inv => inv.PurchaseInvoiceItems.Where(itm => itm.TaxRate >= 0).Sum(itm => itm.CgstAmount * itm.Product.ItcPercentage/100)),
                                    Sgst = si.Sum(inv => inv.PurchaseInvoiceItems.Where(itm => itm.TaxRate >= 0).Sum(itm => itm.SgstAmount * itm.Product.ItcPercentage/100)),
                                    Cess = si.Sum(inv => inv.PurchaseInvoiceItems.Where(itm => itm.TaxRate >= 0).Sum(itm => itm.CessAmount * itm.Product.ItcPercentage/100)),
                                    
                                }).SingleOrDefault();

            var advancePaid = _database.AdvancePaidsForCompany(companyId)
                                                .ForDateRange(frmDate, toDate)
                                                .GroupBy(si => 1)
                                                .Select(si => new SupplyDetail3B
                                                {
                                                    TotalTaxableValue = si.Sum(itm => itm.TotalTaxableValue),
                                                    Igst = si.Sum(inv => inv.AdvancePaidItems.Where(itm => itm.TaxRate >= 0).Sum(itm => itm.IgstAmount)),
                                                    Cgst = si.Sum(inv => inv.AdvancePaidItems.Where(itm => itm.TaxRate >= 0).Sum(itm => itm.CgstAmount)),
                                                    Sgst = si.Sum(inv => inv.AdvancePaidItems.Where(itm => itm.TaxRate >= 0).Sum(itm => itm.SgstAmount)),
                                                    Cess = si.Sum(inv => inv.AdvancePaidItems.Where(itm => itm.TaxRate >= 0).Sum(itm => itm.CessAmount)),

                                                }).SingleOrDefault();

            var receiptNoteCredit = _database.ReceiptNotesForCompany(companyId)
                                                .OfReceiptNoteType(NoteType.Credit)
                                                .ForDateRange(frmDate, toDate)
                                                .GroupBy(si => 1)
                                                .Select(si => new SupplyDetail3B
                                                {
                                                    TotalTaxableValue = si.Sum(itm => itm.TotalTaxableValue),
                                                    Igst = si.Sum(inv => inv.ReceiptNoteItems.Where(itm => itm.TaxRate >= 0).Sum(itm => itm.IgstAmount)),
                                                    Cgst = si.Sum(inv => inv.ReceiptNoteItems.Where(itm => itm.TaxRate >= 0).Sum(itm => itm.CgstAmount)),
                                                    Sgst = si.Sum(inv => inv.ReceiptNoteItems.Where(itm => itm.TaxRate >= 0).Sum(itm => itm.SgstAmount)),
                                                    Cess = si.Sum(inv => inv.ReceiptNoteItems.Where(itm => itm.TaxRate >= 0).Sum(itm => itm.CessAmount)),

                                                }).SingleOrDefault();

            var receiptNoteDebit = _database.ReceiptNotesForCompany(companyId)
                                                .OfReceiptNoteType(NoteType.Debit)
                                                .ForDateRange(frmDate, toDate)
                                                .GroupBy(si => 1)
                                                .Select(si => new SupplyDetail3B
                                                {
                                                    TotalTaxableValue = si.Sum(itm => itm.TotalTaxableValue),
                                                    Igst = si.Sum(inv => inv.ReceiptNoteItems.Where(itm => itm.TaxRate >= 0).Sum(itm => itm.IgstAmount)),
                                                    Cgst = si.Sum(inv => inv.ReceiptNoteItems.Where(itm => itm.TaxRate >= 0).Sum(itm => itm.CgstAmount)),
                                                    Sgst = si.Sum(inv => inv.ReceiptNoteItems.Where(itm => itm.TaxRate >= 0).Sum(itm => itm.SgstAmount)),
                                                    Cess = si.Sum(inv => inv.ReceiptNoteItems.Where(itm => itm.TaxRate >= 0).Sum(itm => itm.CessAmount)),

                                                }).SingleOrDefault();
            var outwardSupplyOtherThanExempted = new SupplyDetail3B();

            outwardSupplyOtherThanExempted.Add(purchase);
            outwardSupplyOtherThanExempted.Add(advancePaid);
            outwardSupplyOtherThanExempted.Substract(receiptNoteDebit);
            outwardSupplyOtherThanExempted.Add(receiptNoteCredit);

            return outwardSupplyOtherThanExempted;
        }

    }
}