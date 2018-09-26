using System;
using System.Collections.Generic;
using System.Text;

namespace BiharEnergy.Core.Domain.Reporting
{
  public class Gstr1DocumentDetail
    {

        public DocumentDet DocDet { get; set; }

    }
    public class DocumentDetail
    {
    //    public int Invoice { get; set; }
        public int Num { get; set; }
        public int From { get; set; }
        public int To { get; set; }
        public int Totnum { get; set; }
        public int Cancel { get; set; }
        public int NetIssue { get; set; }
       
    }
    public class DocumentDet
    {
        public int DocNum { get; set; }
        public DocumentDetail Docs { get; set; }

    }
}
