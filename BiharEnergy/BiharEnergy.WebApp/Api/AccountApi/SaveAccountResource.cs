using BiharEnergy.WebApp.Api.AccountingUnitApi;

namespace BiharEnergy.WebApp.Api.AccountApi
{
    public class SaveAccountResource
    {
        public AccountingUnitResource AccountingUnit { get; set; }
        public string Nature { get; set; }
        public string AccountName { get; set; }
        public string Description { get; set; }
        public double OpeningBalance { get; set; }
        public string AccountingUnitId { get; set; }
    }
}