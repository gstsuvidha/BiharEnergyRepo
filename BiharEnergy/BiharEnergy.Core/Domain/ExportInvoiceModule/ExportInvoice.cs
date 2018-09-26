
using BiharEnergy.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using BiharEnergy.Core.Domain.Inferface;
using BiharEnergy.Core.Domain.Accounting;

namespace BiharEnergy.Core.Domain.ExportInvoiceModule
{
   public class ExportInvoice:IHaveAccountingUnit,IHaveDateFilter
    {
        public int Id { get; set; }
        public int InvoiceNumber { get; set; }
        public DateTime Date { get; set; }
        public string CustomerName { get; set; }
        public string CustomerBilledAddress { get; set; }
        public double Currency { get; set; }
        public double ConversionRate { get; set; }
        public DateTime ShippingDate { get; set; }
        public string ShippingAddress { get; set; } //TODO 
        public int ShippingBillNumber { get; set; }
        public string GstPayment { get; set; }
        public string PortCode { get; set; }
        public string CountryOfSupply { get; set; }
        public string ExportType { get; set; }
        public double TotalInvoiceValue { get; set; }
        public double TotalTaxableValue { get; set; }
        public double TotalTaxAmount { get; set; }
        public SupplyType SupplyType { get; set; }

        //foreign key
        public AccountingUnit AccountingUnit { get; set; }
        public string AccountingUnitId { get; set; }


        public ICollection<ExportInvoiceItem> ExportInvoiceItems { get; set; }

        public ExportInvoice()
        {
            ExportInvoiceItems = new List<ExportInvoiceItem>();
        }

        public ExportInvoice(string accountingUnitId,int invoiceNumber , DateTime date, string customerName ,
        string customerBilledAddress , double currency , double conversionRate , DateTime shippingDate, string shippingAddress ,
        int shippingBillNumber ,string gstPayment,  string portCode,string countryOfSupply, double totalInvoiceValue, 
        double totalTaxableValue, double totalTaxAmount, SupplyType supplyType, string exportType,
        List<ExportInvoiceItem> exportInvoiceItems)
        {
            ExportType= exportType;
            AccountingUnitId = accountingUnitId;
             InvoiceNumber = invoiceNumber;
            Date = date;
            CustomerName = customerName;
            CustomerBilledAddress=customerBilledAddress;
            Currency=currency;
            ConversionRate=conversionRate;
            ShippingDate=shippingDate;
            ShippingAddress= shippingAddress;
            ShippingBillNumber= shippingBillNumber;
            GstPayment = gstPayment;
            PortCode= portCode;
            CountryOfSupply = countryOfSupply;
            TotalInvoiceValue = totalInvoiceValue;
            TotalTaxableValue = totalTaxableValue;
            TotalTaxAmount = totalTaxAmount;
            SupplyType=supplyType;
            ExportInvoiceItems = exportInvoiceItems;

        }

        public void Modify(DateTime invoiceDate, string customerName,
        string customerBilledAddress, double currency, double conversionRate, DateTime shippingDate, string shippingAddress,
        int shippingBillNumber, string gstPayment, string portCode, string countryOfSupply, double totalInvoiceValue,
        double totalTaxableValue, double totalTaxAmount, SupplyType supplyType, string exportType,
        List<ExportInvoiceItem> exportInvoiceItems)
        {
            ExportType = exportType;
            Date = invoiceDate;
            CustomerName = customerName;
            CustomerBilledAddress = customerBilledAddress;
            Currency = currency;
            ConversionRate = conversionRate;
            ShippingDate = shippingDate;
            ShippingAddress = shippingAddress;
            ShippingBillNumber = shippingBillNumber;
            GstPayment = gstPayment;
            PortCode = portCode;
            CountryOfSupply = countryOfSupply;
            TotalInvoiceValue = totalInvoiceValue;
            TotalTaxableValue = totalTaxableValue;
            TotalTaxAmount = totalTaxAmount;
            SupplyType = supplyType;
            ExportInvoiceItems = exportInvoiceItems;
        }


    }
}
