using System;
using BiharEnergy.Core.Enums;

namespace BiharEnergy.WebApp.Api.ProductApi
{
    public class SaveProductResource
    {
        public string Name { get; set; }

        public string Unit { get; set; }
        public string UnitOthers { get; set; }

        public string Description { get; set; }

        public bool IsTaxIncluded { get; set; }
        public bool IsSaleable { get; set; }
        public bool IsPurchaseable { get; set; }

        public double Rate { get; set; }

       
        public string HsnSacCode { get; set; }

        public ProductType ProductType { get; set; }

        public bool IsReverseChargeApplicable { get; set; }

        public double PerReverseCharge { get; set; }

        public double Igst { get; set; }

        public double Cess { get; set; }
        public double ItcPercentage { get; set; }
        public string ItcEligibility { get; set; }



    }
}