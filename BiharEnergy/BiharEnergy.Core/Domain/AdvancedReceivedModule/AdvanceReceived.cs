using System;
using System.Collections.Generic;
using System.Text;
using BiharEnergy.Core.Domain.Accounting;
using BiharEnergy.Core.Domain.AccountModule;
using BiharEnergy.Core.Domain.CustomerModule;
using BiharEnergy.Core.Domain.Inferface;
using BiharEnergy.Core.Enums;

namespace BiharEnergy.Core.Domain.AdvancedReceivedModule
{
    //TODO: change name to AdvanceReceive
    public class AdvanceReceived : IHaveAccountingUnit, IHaveDateFilter, IHaveInvoiceCategory
    {
        public int Id { get; set; }

        public int ReceiptNumber { get; set; } //TODO: rename to InvoiceNumber

        public DateTime Date { get; set; }

        public int? CustomerId { get; set; }
        public int? AccountId { get; set; }

        public string CustomerName { get; set; }

        public string BillingAddress { get; set; }

        public InvoiceCategory InvoiceCategory { get; set; }


        public bool IsPlaceOfSupplyDifferent { get; set; }

        public string PlaceOfSupply { get; set; }

        public SupplyType SupplyType { get; set; }

        public double TotalTaxableValue { get; set; }
        public double TotalTaxAmount { get; set; }
        public double TotalCessAmount { get; set; }
        public double TotalAdvanceReceive { get; set; }

        public string ModeOfPayment { get; set; }
        public string Reference { get; set; }

        public ICollection<AdvanceReceivedItem> AdvanceReceivedItems { get; set; }

        public string AccountingUnitId { get; set; }
        public AccountingUnit AccountingUnit { get; set; }
        public Account Account { get; set; }

        public Customer Customer { get; set; }

        public AdvanceReceived()
        {
            AdvanceReceivedItems=new List<AdvanceReceivedItem>();
        }
        public AdvanceReceived(int receiptNumber, DateTime date, int? customerId,
                                    string customerName, string billingAddress,
                                    bool isPlaceOfSupplyDifferent, string givenPlaceOfSupply,
                                    double totalTaxableValue, double totalTaxAmount, double totalCessAmount,
                                    double totalAdvanceReceive, string modeOfPayment, int? accountId,
                                    string reference,
                                    List<AdvanceReceivedItem> advanceReceivedItems, Customer customer, AccountingUnit accountingUnit)
        {
            Customer = customer;
            AccountingUnit = accountingUnit;
            AccountingUnitId = AccountingUnit.Id;
            CustomerId = customerId;



            ReceiptNumber = receiptNumber;
            Date = date;
            IsPlaceOfSupplyDifferent = isPlaceOfSupplyDifferent;
            TotalTaxableValue = totalTaxableValue;
            TotalTaxAmount = totalTaxAmount;
            TotalCessAmount = totalCessAmount;
            TotalAdvanceReceive = totalAdvanceReceive;
            CustomerName = customerName;
            BillingAddress = billingAddress;
            ModeOfPayment = modeOfPayment;
            AccountId = accountId;
            Reference = reference;

            AdvanceReceivedItems = advanceReceivedItems;


            CalculatePlaceOfSupply(givenPlaceOfSupply);

        }



        public void Modify(DateTime date, int? customerId, string customerName, string billingAddress, bool isPlaceOfSupplyDifferent, string givenPlaceOfSupply,
                            double totalTaxableValue, double totalTaxAmount, double totalCessAmount, double totalAdvanceReceive, string modeOfPayment, int? accountId, string reference,
                            List<AdvanceReceivedItem> advanceReceivedItems, Customer customer, AccountingUnit accountingUnit)
        {
            Customer = customer;
            AccountingUnit = accountingUnit;

            Date = date;
            CustomerId = customerId;
            IsPlaceOfSupplyDifferent = isPlaceOfSupplyDifferent;
            TotalTaxableValue = totalTaxableValue;

            TotalTaxAmount = totalTaxAmount;
            TotalCessAmount = totalCessAmount;
            TotalAdvanceReceive = totalAdvanceReceive;
            ModeOfPayment = modeOfPayment;
            AccountId = accountId;
            Reference = reference;

            AdvanceReceivedItems.Clear();
            AdvanceReceivedItems = advanceReceivedItems;

            CalculatePlaceOfSupply(givenPlaceOfSupply);

        }


        private void CalculatePlaceOfSupply(string givenPlaceOfSupply)
        {
            PlaceOfSupply = IsPlaceOfSupplyDifferent
                ? givenPlaceOfSupply
                : Customer == null
                    ? AccountingUnit.PlaceOfSupply
                    : Customer.State;



            SupplyType = PlaceOfSupply == AccountingUnit.PlaceOfSupply ? SupplyType.IntraState : SupplyType.InterState;

            UpdateTaxCalculation();

        }

        private void UpdateTaxCalculation()
        {
            foreach (var item in AdvanceReceivedItems)
            {
                item.CalculateTax(SupplyType);
            }

        }

    }
}
