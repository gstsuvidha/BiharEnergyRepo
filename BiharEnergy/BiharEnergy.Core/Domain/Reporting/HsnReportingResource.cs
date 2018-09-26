using System;
using System.Collections.Generic;
using System.Text;

namespace BiharEnergy.Core.Domain.Reporting
{

    public class HsnReporting
    {
        public HsnItemReport Data { get; set; }

    }
    
    
        public class HsnItemReport
        {
        public int Num { get; set; }

        public string HsnSc { get; set; }
        public string Desc { get; set; }
        public string Uqc { get; set; }
        public double Qty { get; set; }
        public double Txval { get; set; }
        public double Val { get; set; }
        public double Iamt { get; set; }
        public double Csamt { get; set; }

        }
    
    
       
     
}
