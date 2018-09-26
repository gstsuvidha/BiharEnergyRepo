using System.Collections.Generic;

namespace BiharEnergy.Core.Domain.Reporting
{
    public class Gstr1CreditDebitNoteRegistered
    {
        public string Ctin { get; set; }
        
        public IEnumerable<CreditDebitRegisteredNote> Nt { get; set; }
    }

    public class CreditDebitRegisteredNote
    {
        public string NtNum { get; set; }
        public string NtDt { get; set; }
        public string Ntty { get; set; }
        public string Rsn { get; set; }
        public string PGst { get; set; }
        public string Inum { get; set; }
        public string Idt { get; set; }
        public double Val { get; set; }

        public IEnumerable<Item> Itms { get; set; }

    }

    public class PlaceOfSupplyCdnr{
        public string Pos { get; set; }
        public string Inum { get; set; }
    } 
}