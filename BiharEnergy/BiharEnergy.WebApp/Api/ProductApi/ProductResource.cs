using BiharEnergy.WebApp.Mappings;
using BiharEnergy.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using BiharEnergy.WebApp.Api.AccountingUnitApi;

namespace BiharEnergy.WebApp.Api.ProductApi
{
    public class ProductResource : KeyValuePairResource
    {
        public string Unit { get; set; }
        public string UnitOthers { get; set; }
        public string Description { get; set; }
        public bool IsTaxIncluded { get; set; }
        public bool IsSaleable { get; set; }
        public bool IsPurchaseable { get; set; }

        public double Rate { get; set; }


        public ProductType ProductType { get; set; }

        public string HsnSacCode { get; set; }


        public bool IsReverseChargeApplicable { get; set; }

        public double PerReverseCharge { get; set; }

        public double Igst { get; set; }

        public double Cess { get; set; }
        public double ItcPercentage { get; set; }
        public string ItcEligibility { get; set; }


        public AccountingUnitResource AccountingUnit { get; set; }


    }
}
