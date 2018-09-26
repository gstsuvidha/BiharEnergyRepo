using System.Collections.Generic;

namespace BiharEnergy.Core.Domain.Reporting
{
    public class Gstr1Report
    {

        public string Gstin { get; set; }
        public string Fp { get; set; }
        public string Gt { get; set; }
        public double CurGt { get; set; }

        public IEnumerable<Gstr1B2B> B2b { get; set; }

        public IEnumerable<Gstr1B2Cl> B2cl { get; set; }

        public IEnumerable<Gstr1B2C> B2cs { get; set; }

        public IEnumerable<ExportReportingResource> Exp { get; set; }

        public IEnumerable<Gstr1AdvanceReceivedResources> At { get; set; }

        public IEnumerable<Gstr1CreditDebitNoteRegistered> Cdnr { get; set; }

        public IEnumerable<Gstr1CreditDebitNoteUnregistered> Cdnur { get; set; }

        public IEnumerable<HsnReporting> Hsn { get; set; }

        //public IEnumerable<Gstr1DocumentDetail> DocumentDetail { get; set; }

    }

    public class AccountingUnitInformationInFinalReporting
    {

        public string LegalNameOFCompany { get; set; }
        public string AuthorisedPerson { get; set; }
        public string TradeName { get; set; }
    }
}