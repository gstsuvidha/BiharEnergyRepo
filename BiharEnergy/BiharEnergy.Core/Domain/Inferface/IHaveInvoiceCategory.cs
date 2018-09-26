using System;
using BiharEnergy.Core.Enums;

namespace BiharEnergy.Core.Domain.Inferface
{
    public interface IHaveInvoiceCategory
    {
        InvoiceCategory InvoiceCategory { get; }
    }
}