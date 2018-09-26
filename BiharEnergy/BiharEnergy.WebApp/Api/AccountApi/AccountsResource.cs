using System;
using System.Collections.Generic;
using System.Text;
using BiharEnergy.WebApp.Mappings;

namespace BiharEnergy.WebApp.Api.AccountApi
{
    public class AccountsResource : KeyValuePairResource
    {
        public string Nature { get; set; }
        public string AccountName { get; set; }
        public string Description { get; set; }
        public double OpeningBalance { get; set; }
    }
}
