using BiharEnergy.Core.Domain.Reporting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BiharEnergy.Core.Domain
{
    public interface IGstr1RepositoryForAccountingUnit
    {
        IEnumerable<Gstr1B2B> GetAllB2BInvoices(DateTime frmDate, DateTime toDate, string accountingUnitId);
        IEnumerable<Gstr1B2Cl> GetB2ClInvoices(DateTime frmDate, DateTime toDate, string accountingUnitId);
        IEnumerable<Gstr1B2C> GetAllB2CInvoices(DateTime frmDate, DateTime toDate, string accountingUnitId);



        IEnumerable<Gstr1CreditDebitNoteRegistered> GetAllCdnRInvoices(DateTime frmDate, DateTime toDate, string accountingUnitId);
        IEnumerable<Gstr1CreditDebitNoteUnregistered> GetAllCdnUrInvoices(DateTime frmDate, DateTime toDate, string accountingUnitId);


        IEnumerable<Gstr1AdvanceReceivedResources> GetAllAdvanceReceives(DateTime frmDate, DateTime toDate, string accountingUnitId);


        Task<IEnumerable<HsnReporting>> GetAllHsnReport(DateTime frmDate, DateTime toDate, string accountingUnitId);
        Task<IEnumerable<Gstr1DocumentDetail>> GetDocumentDetail(DateTime frmDate, DateTime toDate, string accountingUnitId);
        Task<TaxSummary> GetB2BTotalDashboardSummary(DateTime frmDate, DateTime toDate, string accountingUnitId);
        Task<IEnumerable<ExportReportingResource>> GetAllExportInvoices(DateTime frmDate, DateTime toDate, string accountingUnitId);


        IEnumerable<PlaceOfSupplyCdnr> GetPlaceOfSupplyAtCdnr(DateTime frmDate, DateTime toDate, string accountingUnitId);
        IEnumerable<PlaceOfSupplyCdnur> GetPlaceOfSupplyAtCdnur(DateTime frmDate, DateTime toDate, string accountingUnitId);
        Task<IEnumerable<GetCustomerName>> GetCustomerNameAtB2B(DateTime frmDate, DateTime toDate, string accountingUnitId);


    }
}




//Repository of total calculation of dashboard


//Task<IEnumerable<TaxSummary>> GetB2BTotalDashboardSummary (DateTime frmDate, DateTime toDate, string tenantId);
//Task<IEnumerable<TaxSummary>> GetB2CTotalDashboardSummary(DateTime frmDate, DateTime toDate, string tenantId);
//Task<IEnumerable<TaxSummary>> GetB2CLTotalDashboardSummary(DateTime frmDate, DateTime toDate, string tenantId);
//Task<IEnumerable<TaxSummary>> GetRegisterNoteTotalDashboardSummary(DateTime frmDate, DateTime toDate, string tenantId);
//Task<IEnumerable<TaxSummary>> GetUnregisterNoteTotalDashboardSummary(DateTime frmDate, DateTime toDate, string tenantId);