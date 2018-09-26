using System;
using System.Collections.Generic;
using System.Text;
using BiharEnergy.Core.Domain.Accounting;
using BiharEnergy.Core.Domain.Inferface;
using BiharEnergy.Core.Domain.ProductModule;

namespace BiharEnergy.Core.Domain.InventoryModule
{
   public class Inventory:IHaveAccountingUnit
    {
        public int Id { get; set; }
        public double Damage { get; set; }
        public DateTime Date { get; set; }
        public AccountingUnit AccountingUnit { get; set; }
        public string AccountingUnitId { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
      

        public Inventory()
        {

        }
        public Inventory(double damage, DateTime date, int productId, string accountingUnitId)
        {

            Damage = damage;
            Date = date;
            ProductId = productId;
            AccountingUnitId = accountingUnitId;
        }
        public void Modify(double damage)
        {
            Damage = damage;
          

        }
        public void Delete()
        {
        }
    }
}
