using System;
using System.Collections.Generic;
using System.Text;

namespace BiharEnergy.Core.Domain.Reporting
{
  public  class TaxSummary
    {

        
        
            public double TotalTaxableValue { get; set; }
            public double TotalTaxAmount { get; set; }
        
    }

    public class Gstr1ReportingDashboard
    {



        public TaxSummary B2B { get; set; }
        public TaxSummary B2C { get; set; }
        public TaxSummary B2CL { get; set; }
        public TaxSummary CreditDebitNoteRegister { get; set; }
        public TaxSummary CreditDebitNoteUnregister { get; set; }

    }
}
