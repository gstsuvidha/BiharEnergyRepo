using System;
using BiharEnergy.Core.Domain.Accounting;
using BiharEnergy.Core.Domain.Inferface;
using BiharEnergy.Core.Domain.SupplierModule;

namespace BiharEnergy.Core.Domain.TdsModule
{
    public class Tds : IHaveAccountingUnit, IHaveDateFilter
    {
        public int Id { get; set; }
        public Supplier Supplier { get; set; }

        public int SupplierId { get; set; }
        public DateTime Date { get; set; }



        public string PlaceOfSupply { get; set; }

        public double AmountPaid { get; set; }

        public double CgstAmount { get; set; }
        public double SgstAmount { get; set; }
        public double IgstAmount { get; set; }
        public double TdsAmount { get; set; }
        public double NetAmount { get; set; }

        public bool IsActive { get; set; }

        public AccountingUnit AccountingUnit { get; set; }

        public string AccountingUnitId { get; set; }

        public Tds()
        {

        }


        public Tds(int supplierId, DateTime date, string placeOfSupply, double amountPaid, double cgstAmount,
                    double sgstAmount, double igstAmount, double tdsAmount, double netAmount, string accountingUnitId)
        {
            SupplierId = supplierId;
            Date = date;
            PlaceOfSupply = placeOfSupply;
            AmountPaid = amountPaid;
            CgstAmount = cgstAmount;
            SgstAmount = sgstAmount;
            IgstAmount = igstAmount;
            TdsAmount = tdsAmount;
            NetAmount = netAmount;
            AccountingUnitId = accountingUnitId;
            IsActive = true;

        }
        private readonly double netAmount;

        public void Modify(int supplierId, DateTime date, string placeOfSupply, double amountPaid, double cgstAmount,
                   double sgstAmount, double igstAmount, double tdsAmount, double netAmount, string accountingUnitId)
        {
            NetAmount = netAmount;
            SupplierId = supplierId;
            Date = date;
            PlaceOfSupply = placeOfSupply;
            AmountPaid = amountPaid;
            CgstAmount = cgstAmount;
            SgstAmount = sgstAmount;
            IgstAmount = igstAmount;
            TdsAmount = tdsAmount;
            AccountingUnitId = accountingUnitId;
            IsActive = true;

        }

        public void Delete()
        {
            IsActive = false;
        }

    }
}