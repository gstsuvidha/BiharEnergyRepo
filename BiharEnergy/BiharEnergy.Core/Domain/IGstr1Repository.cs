using BiharEnergy.Core.Domain.Reporting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BiharEnergy.Core.Domain
{
    public interface IGstr1Repository
    {
        IEnumerable<Gstr1B2B> GetAllB2BInvoices(DateTime frmDate, DateTime toDate, int companyId);
        IEnumerable<Gstr1B2Cl> GetB2ClInvoices(DateTime frmDate, DateTime toDate, int companyId);
        IEnumerable<Gstr1B2C> GetAllB2CInvoices(DateTime frmDate, DateTime toDate, int companyId);



        IEnumerable<Gstr1CreditDebitNoteRegistered> GetAllCdnRInvoices(DateTime frmDate, DateTime toDate, string companyId);
        IEnumerable<Gstr1CreditDebitNoteUnregistered> GetAllCdnUrInvoices(DateTime frmDate, DateTime toDate, string companyId);


        IEnumerable<Gstr1AdvanceReceivedResources> GetAllAdvanceReceives(DateTime frmDate, DateTime toDate, string companyId);


        Task<IEnumerable<HsnReporting>> GetAllHsnReport(DateTime frmDate, DateTime toDate, int companyId);
        Task<IEnumerable<Gstr1DocumentDetail>> GetDocumentDetail(DateTime frmDate, DateTime toDate, string companyId);
        Task<TaxSummary> GetB2BTotalDashboardSummary(DateTime frmDate, DateTime toDate, string companyId);
        Task<IEnumerable<ExportReportingResource>> GetAllExportInvoices(DateTime frmDate, DateTime toDate, string companyId);


        IEnumerable<PlaceOfSupplyCdnr> GetPlaceOfSupplyAtCdnr(DateTime frmDate, DateTime toDate, string companyId);
        IEnumerable<PlaceOfSupplyCdnur> GetPlaceOfSupplyAtCdnur(DateTime frmDate, DateTime toDate, string companyId);
        Task<IEnumerable<GetCustomerName>> GetCustomerNameAtB2B(DateTime frmDate, DateTime toDate, int companyId);


    }
}




//Repository of total calculation of dashboard


//Task<IEnumerable<TaxSummary>> GetB2BTotalDashboardSummary (DateTime frmDate, DateTime toDate, string tenantId);
//Task<IEnumerable<TaxSummary>> GetB2CTotalDashboardSummary(DateTime frmDate, DateTime toDate, string tenantId);
//Task<IEnumerable<TaxSummary>> GetB2CLTotalDashboardSummary(DateTime frmDate, DateTime toDate, string tenantId);
//Task<IEnumerable<TaxSummary>> GetRegisterNoteTotalDashboardSummary(DateTime frmDate, DateTime toDate, string tenantId);
//Task<IEnumerable<TaxSummary>> GetUnregisterNoteTotalDashboardSummary(DateTime frmDate, DateTime toDate, string tenantId);