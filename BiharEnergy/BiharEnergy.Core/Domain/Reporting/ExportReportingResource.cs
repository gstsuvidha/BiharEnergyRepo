using System;
using System.Collections.Generic;
using System.Text;

namespace BiharEnergy.Core.Domain.Reporting
{
    public class ExportReportingResource
    {
        public string ExpTyp { get; set; }
        public ExportReport Inv { get; set; }
    }



    public class ExportReport
    {
    public string Inum { get; set; }
    public string Idt { get; set; }
    public double Val { get; set; }
    public string Sbpcode { get; set; }
    public string Sbnum { get; set; }
    public string Sbdt { get; set; }

    public IEnumerable<Item> Itms { get; set; }

    }
}