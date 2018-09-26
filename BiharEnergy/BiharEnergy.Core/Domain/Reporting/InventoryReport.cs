namespace BiharEnergy.Core.Domain.Reporting
{
    public class InventoryReport
    {
        public string ItemName { get; set; }
        public double StockInHand { private get; set; }


        public double SalesTillDate { private get; set; }
        public double SalesForDate { private get; set; }

        public double PurchaseTillDate { private get; set; }
        public double PurchaseForDate { private get; set; }


        public double CreditedQuantityIssuedToCustomerTillDate { private get; set; }
        public double CreditedQuantityIssuedToCustomer { private get; set; }

        public double DebitedQuantityIssuedToCustomerTillDate { private get; set; }
        public double DebitedQuantityIssuedToCustomer { private get; set; }


        public double CreditedQuantityTakenFromSupplierTillDate { private get; set; }
        public double CreditedQuantityTakenFromSupplier { private get; set; }


        public double DebitedQuantityTakenFromSupplierTillDate { private get; set; }
        public double DebitedQuantityTakenFromSupplier { private get; set; }

        public double DamageTillDate { private get; set; }
        public double Damage { get; set; }


        public double OpeningStock => StockInHand
                                      + (PurchaseTillDate + DebitedQuantityTakenFromSupplierTillDate - CreditedQuantityTakenFromSupplierTillDate)
                                      - (SalesTillDate  + DebitedQuantityIssuedToCustomerTillDate - CreditedQuantityIssuedToCustomerTillDate)
                                      - DamageTillDate;

        public double Sales => SalesForDate +DebitedQuantityIssuedToCustomer -CreditedQuantityIssuedToCustomer;
        public double Purchases => PurchaseForDate + DebitedQuantityTakenFromSupplier - CreditedQuantityTakenFromSupplier;

        public double TotalLeftStock => OpeningStock + Purchases - Sales - Damage ;
    }

}
