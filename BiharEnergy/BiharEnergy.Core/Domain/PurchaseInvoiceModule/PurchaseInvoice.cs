using BiharEnergy.Core.Domain.Accounting;
using BiharEnergy.Core.Domain.AccountModule;
using BiharEnergy.Core.Domain.Inferface;
using BiharEnergy.Core.Domain.SupplierModule;
using BiharEnergy.Core.Enums;
using System;
using System.Collections.Generic;

namespace BiharEnergy.Core.Domain.PurchaseInvoiceModule
{
    public class PurchaseInvoice : IHaveAccountingUnit, IHaveDateFilter, IHaveInvoiceCategory
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }
        public DateTime PostingDate { get; set; }
        

        public string InvoiceNumber { get; set; } //TenantId and Invoice Number will be Unique

        public string AccountingUnitId { get; set; }

        public bool IsReverseChargeApplicable { get; set; }

        public int? SupplierId { get; set; }
    
        public string Etin { get; set; }

        public string PurchaseInvoiceType { get; set; }

        public InvoiceCategory InvoiceCategory { get; private set; }//B2b,B2c etc

        public InvoiceType InvoiceType { get; private set; }

        public bool IsRegisteredPurchase { get; private set; }

        public string PlaceOfSupply { get; private set; }

        public SupplyType SupplyType { get; private set; }

        public string Reference { get; set; }


        public string ShippingAddress { get; set; }


        public double TotalTaxableValue { get; set; }
        public double TotalTaxAmount { get; set; }
        public double TotalCessAmount { get; set; }
        public double TotalInvoiceValue { get; set; }
        public bool BilledPassed { get; set; }

        public Account Account { get; set; }
        public Supplier Supplier { get; set; }
        public AccountingUnit AccountingUnit { get; set; }

        public ICollection<PurchaseInvoiceItem> PurchaseInvoiceItems { get; set; }

        public PurchaseInvoice()
        {
            PurchaseInvoiceItems = new List<PurchaseInvoiceItem>();
        }

        public PurchaseInvoice(DateTime date, DateTime postingDate, string invoiceNumber,
                                string accountingUnitId, bool isReverseChargeApplicable, int? supplierId,
                                string etin, string purchaseInvoiceType,
                                string placeOfSupply, string shippingAddress,
                                double totalTaxableValue, double totalTaxAmount, double totalCessAmount, 
                                 string reference,bool billedPassed,
                                List<PurchaseInvoiceItem> purchaseInvoiceItems,
                                Supplier supplier, AccountingUnit accountingUnit)
        {
            Supplier = supplier;
            AccountingUnit = accountingUnit;

            Date = date;
            PostingDate = postingDate;
            InvoiceNumber = invoiceNumber;
            AccountingUnitId = accountingUnitId;
            IsReverseChargeApplicable = isReverseChargeApplicable;
            SupplierId = supplierId;
            Etin = etin;
            PurchaseInvoiceType = purchaseInvoiceType;
            Reference = reference;
            ShippingAddress = shippingAddress;
            TotalTaxableValue = totalTaxableValue;
            TotalTaxAmount = totalTaxAmount;
            TotalCessAmount = totalCessAmount;
            TotalInvoiceValue = TotalTaxableValue + TotalTaxAmount;
            PurchaseInvoiceItems = purchaseInvoiceItems;
            BilledPassed = billedPassed;
            PlaceOfSupply = placeOfSupply;

            CalculatePlaceOfSupply();
        }

        private void CalculatePlaceOfSupply()
        {

            InvoiceType = InvoiceType.R;
            if(Supplier != null)
            {
                SupplyType = PlaceOfSupply == Supplier.State

                 ? SupplyType.IntraState
                 : SupplyType.InterState;
               
                 }
                 else
                 {
                    SupplyType = PlaceOfSupply == AccountingUnit.PlaceOfSupply
                    
                    ? SupplyType.IntraState
                    : SupplyType.InterState;
                 }



            // SupplyType = Supplier != null && Supplier.State == PlaceOfSupply
            //                         ? SupplyType.IntraState
            //                         : Tenant.PlaceOfSupply == PlaceOfSupply ? SupplyType.InterState : SupplyType.IntraState; //TODO: login check


            //InvoiceCategory =
            //                  Tenant.RegistrationType == "Registered" // TODO: convert to enum
            //    ? InvoiceCategory.B2B
            //    : TotalInvoiceValue > 250000
            //      && SupplyType == SupplyType.InterState
            //        ? InvoiceCategory.B2CL
            //        : InvoiceCategory.B2C;

            //IsRegisteredPurchase = InvoiceCategory == InvoiceCategory.B2B;

            InvoiceCategory = Supplier != null
                             && Supplier.RegistrationType != RegistrationType.Unregistered
                             

               ? InvoiceCategory.B2B
               : TotalInvoiceValue > 250000
                 && SupplyType == SupplyType.InterState
                   ? InvoiceCategory.B2CL
                   : InvoiceCategory.B2C;

            IsRegisteredPurchase = InvoiceCategory == InvoiceCategory.B2B;



        }

        public void Modify(DateTime date, DateTime postingDate,
                            bool isReverseChargeApplicable, int? supplierId,
                            string etin, string purchaseInvoiceType,
                            string placeOfSupply, string shippingAddress,
                            double totalTaxableValue, double totalTaxAmount, double totalCessAmount, 
                             string reference,bool billedPassed, 
                            List<PurchaseInvoiceItem> purchaseInvoiceItems,
                            Supplier supplier, AccountingUnit accountingUnit)
        {
            Supplier = supplier;
            AccountingUnit = accountingUnit;

            Date = date;
            PostingDate = postingDate;
            IsReverseChargeApplicable = isReverseChargeApplicable;
            SupplierId = supplierId;
            Etin = etin;
            PurchaseInvoiceType = purchaseInvoiceType;
            ShippingAddress = shippingAddress;
            TotalTaxableValue = totalTaxableValue;
            Reference = reference;
            TotalTaxAmount = totalTaxAmount;
            TotalCessAmount = totalCessAmount;
            TotalInvoiceValue = TotalTaxableValue + TotalTaxAmount;
            BilledPassed = billedPassed;
            PlaceOfSupply = placeOfSupply;


            PurchaseInvoiceItems.Clear();
            PurchaseInvoiceItems = purchaseInvoiceItems;

            CalculatePlaceOfSupply();

        }




    }
}
