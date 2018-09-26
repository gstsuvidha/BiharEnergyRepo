using System.Collections.Generic;

namespace BiharEnergy.Core.Domain.Reporting
{
    public class Gstr3B
    {
        public string Gstin { get; set; }
        public string LegalNameOfTheRegisteredPerson { get; set; }
        public string Fp { get; set; }
        public string Gt { get; set; }
        public double CurGt { get; set; }

        public SupplyDetail3B OutwardSupplyOtherThanExempted { get; set; }
        public SupplyDetail3B OutwardTaxableSupplyZeroRated { get; set; }
        public SupplyDetail3B OtherOutwardSupplyNilRatedExempted { get; set; }

        public SupplyDetail3B InwardSupplyLiablToReverseCharge { get; set; }
        public SupplyDetail3B NonGstOutwardSupply { get; set; }
        public SupplyDetail3B AllOtherItc { get; set; }

        public IEnumerable<InterStateSupply> Unregistered { get; set; }
        public IEnumerable<InterStateSupply> Composite { get; set; }
        public IEnumerable<InterStateSupply> UinHolder { get; set; }



    }

    public class SupplyDetail3B
    {
        public double TotalTaxableValue { get; set; }
        public double Igst { get; set; }
        public double Cgst { get; set; }
        public double Sgst { get; set; }
        public double Cess { get; set; }

        public void Add(SupplyDetail3B detail)
        {
            if (detail == null)
                return;


            TotalTaxableValue += detail.TotalTaxableValue;
            Igst += detail.Igst;
            Cgst += detail.Cgst;
            Sgst += detail.Sgst;
            Cess += detail.Cess;
        }
        public void Substract(SupplyDetail3B detail)
        {
            if (detail == null)
                return;


            TotalTaxableValue -= detail.TotalTaxableValue;
            Igst -= detail.Igst;
            Cgst -= detail.Cgst;
            Sgst -= detail.Sgst;
            Cess -= detail.Cess;
        }
    }

    public class InterStateSupply
    {
        public string PlaceOfSupply { get; set; }
        public double TotalTaxableValue { get; set; }
        public double Igst { get; set; }
    }

}
