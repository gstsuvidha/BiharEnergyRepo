using System;
using System.Collections.Generic;
using System.Text;

namespace BiharEnergy.Core.Domain.Reporting
{
   public class Gstr1FinalReportResource
    {
        public string gstin { get; set; }
        public string fp { get; set; }
        public string gt { get; set; }
        public string cur_gt { get; set; }
        public string version = "GST1..1";
        public string hash = "hash";


        public IEnumerable<Item> Itms { get; set; }

        public IEnumerable<Gstr1B2B> b2b { get; set; }

        public IEnumerable<Gstr1B2C> b2cs { get; set; }

        public IEnumerable<Gstr1B2Cl> b2cl { get; set; }

        public string exp { get; set; }

        public IEnumerable<Gstr1AdvanceReceivedResources> at { get; set; }

        public IEnumerable<Gstr1CreditDebitNoteRegistered> cdnr { get; set; }

        public IEnumerable<Gstr1CreditDebitNoteUnregistered> cdnur { get; set; }

        public IEnumerable<HsnReporting> hsn { get; set; }

   
        public string txpd { get; set; }


    }
}
