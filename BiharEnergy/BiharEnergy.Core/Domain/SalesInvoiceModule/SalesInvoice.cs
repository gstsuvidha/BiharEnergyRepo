using BiharEnergy.Core.Domain.Accounting;
using BiharEnergy.Core.Domain.CustomerModule;
using BiharEnergy.Core.Domain.Inferface;
using BiharEnergy.Core.Enums;
using System;
using System.Collections.Generic;

namespace BiharEnergy.Core.Domain.SalesInvoiceModule
{
    public class SalesInvoice : IHaveDateFilter, IHaveAccountingUnit, IHaveInvoiceCategory
    {

        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string InvoicePrefix { get; set; }

        public int InvoiceNumber { get; set; } //TenantId and Invoice Number will be Unique

        public string AccountingUnitId { get; set; }

        public bool IsReverseChargeApplicable { get; set; }

        public int? CustomerId { get; set; }

        public string CustomerName { get; set; }

        public string BillingAddress { get; set; }

        public string Etin { get; set; }

        public bool IsRegisteredSales { get; set; }



        public InvoiceCategory InvoiceCategory { get; set; }//B2b,B2c etc

        public InvoiceType InvoiceType { get; set; }

        public bool IsPlaceOfSupplyDifferent { get; set; }

        public string PlaceOfSupply { get; set; }

        public SupplyType SupplyType { get; set; }




        public string ShippingAddress { get; set; }


        public string Reference { get; set; }


        public double TotalTaxableValue { get; set; }

        public double TotalTaxAmount { get; set; }
        public double TotalCessAmount { get; set; }

        public double Freight { get; set; }
        public double Insurance { get; set; }
        public double PackingFwdChrg { get; set; }


        public double TotalInvoiceValue { get; set; }


        public Customer Customer { get; set; }

        public AccountingUnit AccountingUnit { get; set; }

        public bool IsEdited { get; set; }

        public DateTime CreatedAt { get; set; }

        public int NoOfTimesEdited { get; set; }

        public DateTime LastUpdated { get; set; }


        public ICollection<SalesInvoiceItem> SalesInvoiceItems { get; set; }

        public SalesInvoice()
        {
            SalesInvoiceItems = new List<SalesInvoiceItem>();
        }

        public SalesInvoice(DateTime date, string invoicePrefix, int invoiceNumber,
                             bool isReverseChargeApplicable, string customerName, string billingAddress,
                                string etin, bool isPlaceOfSupplyDifferent, string givenPlaceOfSupply,
                                string shippingAddress,
                                double totalTaxableValue, double totalTaxAmount, double totalCessAmount, double freight,
                                 double insurance, string reference,
                                double packingFwdChrg, List<SalesInvoiceItem> salesInvoiceItems, Customer customer, AccountingUnit accountingUnit)
        {

            Customer = customer;
            AccountingUnit = accountingUnit;

            Date = date;

            InvoicePrefix = invoicePrefix;
            InvoiceNumber = invoiceNumber;

            AccountingUnitId = AccountingUnit.Id;

            IsReverseChargeApplicable = isReverseChargeApplicable;

            CustomerName = customerName;

            BillingAddress = billingAddress;
            ShippingAddress = shippingAddress;

            Etin = etin;

            IsPlaceOfSupplyDifferent = isPlaceOfSupplyDifferent;
            TotalTaxableValue = totalTaxableValue;
            TotalTaxAmount = totalTaxAmount;
            TotalCessAmount = totalCessAmount;
            Freight = freight;
            Insurance = insurance;
            PackingFwdChrg = packingFwdChrg;
            TotalInvoiceValue = TotalTaxableValue + TotalTaxAmount + Freight + Insurance + packingFwdChrg;

            Reference = reference;

            SalesInvoiceItems = salesInvoiceItems;

            CalculatePlaceOfSupply(givenPlaceOfSupply);

            IsEdited = false;
            CreatedAt = DateTime.Now;
            LastUpdated = DateTime.Now;
            NoOfTimesEdited = 0;

        }

        public void Modify(DateTime date, string invoicePrefix,
                        bool isReverseChargeApplicable, string customerName,
                        string billingAddress, string etin,
                        bool isPlaceOfSupplyDifferent, string givenPlaceOfSupply,
                        string shippingAddress, double totalTaxableValue,
                        double totalTaxAmount, double totalCessAmount, double freight,
                        double insurance,
                        string reference, double packingFwdChrg,
                        List<SalesInvoiceItem> salesInvoiceItems,
                        Customer customer, AccountingUnit accountingUnit)
        {
            Customer = customer;
            AccountingUnit = accountingUnit;
            Date = date;
            InvoicePrefix = invoicePrefix;
            IsReverseChargeApplicable = isReverseChargeApplicable;
            CustomerName = customerName;
            BillingAddress = billingAddress;
            Etin = etin;
            IsPlaceOfSupplyDifferent = isPlaceOfSupplyDifferent;
            ShippingAddress = shippingAddress;
            TotalTaxableValue = totalTaxableValue;
            TotalTaxAmount = totalTaxAmount;
            TotalCessAmount = totalCessAmount;
            Freight = freight;
            Insurance = insurance;
            Reference = reference;
            PackingFwdChrg = packingFwdChrg;
            TotalInvoiceValue = TotalTaxableValue + TotalTaxAmount + Freight + Insurance + PackingFwdChrg;

            SalesInvoiceItems.Clear();
            SalesInvoiceItems = salesInvoiceItems;

            CalculatePlaceOfSupply(givenPlaceOfSupply);

            IsEdited = true;
            LastUpdated = DateTime.Now;
            NoOfTimesEdited++;
        }


        private void CalculatePlaceOfSupply(string givenPlaceOfSupply)
        {
            CustomerId = Customer?.Id;


            InvoiceType = InvoiceType.R;

            PlaceOfSupply = IsPlaceOfSupplyDifferent
                                        ? givenPlaceOfSupply
                                        : Customer == null
                                            ? AccountingUnit.PlaceOfSupply
                                            : Customer.State;

            SupplyType = PlaceOfSupply == AccountingUnit.PlaceOfSupply
                ? SupplyType.IntraState
                : SupplyType.InterState;


            InvoiceCategory = Customer != null
                              && (Customer.RegistrationType == RegistrationType.Registered || Customer.RegistrationType == RegistrationType.CompositeDealer)
                                  ? InvoiceCategory.B2B
                                  : TotalInvoiceValue > 250000
                                    && SupplyType == SupplyType.InterState
                                      ? InvoiceCategory.B2CL
                                      : InvoiceCategory.B2C;

            IsRegisteredSales = InvoiceCategory == InvoiceCategory.B2B;

            UpdateTaxCalculation();
        }

        private void UpdateTaxCalculation()
        {
            foreach (var item in SalesInvoiceItems)
            {
                item.CalculateTax(SupplyType);
            }

        }




    }
}
