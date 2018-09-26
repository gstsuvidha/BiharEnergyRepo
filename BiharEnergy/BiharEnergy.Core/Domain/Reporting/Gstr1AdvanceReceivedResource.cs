using System;
using System.Collections.Generic;
using System.Text;

namespace BiharEnergy.Core.Domain.Reporting
{
    public class Gstr1AdvanceReceivedResources
    {

        public string Pos { get; set; }
        public string SplyTy { get; set; }

        public IEnumerable<AdvanceItem> Itms { get; set; }
    }
    
    
}
