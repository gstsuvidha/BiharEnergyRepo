using System;
using System.Collections.Generic;
using System.Text;

using System.Linq;

namespace BiharEnergy.Core.Domain.Reporting
{


    public class Gstr1B2C
    {


        public string SplyTy { get; set; }
        public string Pos { get; set; }
        public double Txval { get; set; } //taxable value
        public double Rt { get; set; }
        public double? Iamt { get; set; }
        public double? Camt { get; set; }
        public double? Samt { get; set; }
        public double? Csamt { get; set; }
    


    }
    
    
}
