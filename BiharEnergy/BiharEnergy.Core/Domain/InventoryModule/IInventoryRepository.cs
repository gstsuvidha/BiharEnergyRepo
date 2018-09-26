using System;
using System.Threading.Tasks;

namespace BiharEnergy.Core.Domain.InventoryModule
{
   public interface IInventoryRepository : IRepositoryBase<Inventory>
    {
        //Task<Inventory> GetAsync_IncludeAllProperties(int id, string tenantId);
      
        ////Task<string> GetDamageQuantityById(int productId);

    }
}
