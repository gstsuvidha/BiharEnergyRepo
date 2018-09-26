namespace BiharEnergy.WebApp.Api.PurchaseApi
{
    public class SavePurchaseInvoiceItemResource
    {

        public int ProductId { get; set; }

        public double Quantity { get; set; }

        public double UnitPrice { get; set; }

        public double Discount { get; set; }

        public double TaxRate { get; set; }

        public double TaxableValue { get; set; }

        public double TaxAmount { get; set; }

        public double IgstAmount { get; set; }
        public double CgstAmount { get; set; }
        public double SgstAmount { get; set; }

        public double CessAmount { get; set; }
        public double Total { get; set; }

    }
}