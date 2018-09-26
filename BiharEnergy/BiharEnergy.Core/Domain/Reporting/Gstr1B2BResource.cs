using System.Collections.Generic;

namespace BiharEnergy.Core.Domain.Reporting
{
    public class Gstr1B2B
    {

        public string Ctin { get; set; }
        public IEnumerable<B2BInvoice> Inv { get; set; }
    }

    public class B2BInvoice
    {

        public string Inum { get; set; }
        public string Idt { get; set; }
        public double Val { get; set; }
        public string Pos { get; set; }
        public string Rchrg { get; set; }
        public string InvType { get; set; }
        public IEnumerable<Item> Itms { get; set; }

    }
    public class GetCustomerName {
        public string CustomerName { get; set; }
        public string Gstin { get; set; }
    }
}
