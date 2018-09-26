using System;
using BiharEnergy.Core.Enums;

namespace BiharEnergy.WebApp.Api.CustomerApi
{
    public class SaveCustomerResource
    {
        public string Name { get; set; }
        public string  Gstin { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string ContactNumber { get; set; }
        public RegistrationType RegistrationType { get; set; }
        public string Email { get; set; }
    }
}