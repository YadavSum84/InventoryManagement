using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryAPI.Models;

namespace InventoryManagement.Models
{
    public interface IInventoryRepository
    {
        Task Add(Inventory Inventories);
        Task Update(Inventory Inventories);
        Task Delete(string id);
        Task<Inventory> GetInventory(string id);
        Task<IEnumerable<Inventory>> GetInventories();
    }
}