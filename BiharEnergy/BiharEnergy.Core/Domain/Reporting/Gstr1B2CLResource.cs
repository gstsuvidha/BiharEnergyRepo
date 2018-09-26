using System;
using System.Collections.Generic;
using System.Text;

namespace BiharEnergy.Core.Domain.Reporting
{
    public class Gstr1B2Cl
    {

        public string Pos { get; set; }
        public IEnumerable<B2ClInvoice> Inv { get; set; }
    }
    public class B2ClInvoice
    {

        public string Etin { get; set; }
        public int Inum { get; set; }
        public string Idt { get; set; }
        public double Val { get; set; }
        public IEnumerable<Item> Itms { get; set; }
    }
}
