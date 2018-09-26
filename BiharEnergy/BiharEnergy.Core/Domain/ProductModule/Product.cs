using System;
using System.Collections.Generic;
using System.Text;
using BiharEnergy.Core.Domain.Accounting;
using BiharEnergy.Core.Domain.Inferface;
using BiharEnergy.Core.Domain.InventoryModule;
using BiharEnergy.Core.Domain.IssueNoteModule;
using BiharEnergy.Core.Domain.PurchaseInvoiceModule;
using BiharEnergy.Core.Domain.ReceiptNoteModule;
using BiharEnergy.Core.Domain.SalesInvoiceModule;
using BiharEnergy.Core.Enums;

namespace BiharEnergy.Core.Domain.ProductModule
{
    public class Product : IHaveAccountingUnit, IHaveActiveFilter
    {
        public int Id { get; set; }

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


        public bool IsActive { get; set; }

        //foreign key
        public string AccountingUnitId { get; set; }
        public AccountingUnit AccountingUnit { get; set; }


        public IEnumerable<SalesInvoiceItem> SalesInvoiceItems { get; set; }
        public IEnumerable<PurchaseInvoiceItem> PurchaseInvoiceItems { get; set; }
        public IEnumerable<IssueNoteItem> IssueNoteItems { get; set; }
        public IEnumerable<ReceiptNoteItem> ReceiptNoteItems { get; set; }
        public IEnumerable<Inventory> Inventory { get; set; }


        protected Product()
        {
            //Ef
        }

        public Product(string name, string unit, string unitOthers, string description,
         bool isTaxIncluded, bool isSaleable, bool isPurchaseable,
          double rate, string hsnSacCode, ProductType productType,
         bool isReverseChargeApplicable, double perReverseCharge,
         double igst, double cess, double itcPercentage, string itcEligibility, string accountingUnitId)
        {
            Name = name;
            Unit = unit;
            UnitOthers = unitOthers;
            Description = description;
            IsTaxIncluded = isTaxIncluded;
            IsSaleable = isSaleable;
            IsPurchaseable = isPurchaseable;
            Rate = rate;
            HsnSacCode = hsnSacCode;
            ProductType = productType;
            IsReverseChargeApplicable = isReverseChargeApplicable;
            PerReverseCharge = perReverseCharge;
            Igst = igst;
            Cess = cess;
            ItcPercentage = itcPercentage;
            ItcEligibility = itcEligibility;
            AccountingUnitId = accountingUnitId;
            IsActive = true;
        }

        public void Modify(string name, string unit, string unitOthers, string description, bool isTaxIncluded,
                            bool isSaleable, bool isPurchaseable, double rate,
                             string hsnSacCode, ProductType productType,
                            bool isReverseChargeApplicable, double perReverseCharge,
                            double igst, double cess, double itcPercentage, string itcEligibility)
        {
            Name = name;
            Unit = unit;
            UnitOthers = unitOthers;
            Description = description;
            IsTaxIncluded = isTaxIncluded;
            IsSaleable = isSaleable;
            IsPurchaseable = isPurchaseable;
            Rate = rate;
            HsnSacCode = hsnSacCode;
            ProductType = productType;
            IsReverseChargeApplicable = isReverseChargeApplicable;
            PerReverseCharge = perReverseCharge;
            Igst = igst;
            Cess = cess;
            ItcPercentage = itcPercentage;
            ItcEligibility = itcEligibility;

            IsActive = true;
        }

        public void Delete()
        {
            IsActive = false;
        }
    }
}
