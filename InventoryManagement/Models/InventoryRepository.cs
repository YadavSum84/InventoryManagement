using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryAPI.Models;
using System.Data.Entity;

namespace InventoryManagement.Models
{
    //Interface Inherited to Declare task with Logic Codes
    public class InventoryRepository : IInventoryRepository
    {
        private readonly SqlDbContext db = new SqlDbContext();
        public async Task Add(Inventory Inventories)
        {
            //Inventories.ItemId = Convert.ToInt32(Guid.NewGuid());
            db.Inventories.Add(Inventories);
            try
            {
                await db.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        //Get Inventory Item Details as per ItemId={id}
        public async Task<Inventory> GetInventory(string id)
        {
            try
            {
                int Id = Convert.ToInt32(id);
                Inventory employee = await db.Inventories.FindAsync(Id);
                if (employee == null)
                {
                    return null;
                }
                return employee;
            }
            catch
            {
                throw;
            }
        }
        //Get Inventory Item List Details as per ItemId={id}
        public async Task<IEnumerable<Inventory>> GetInventories()
        {
            try
            {
                var Inventories = await db.Inventories.ToListAsync();
                return Inventories.AsQueryable();
            }
            catch
            {
                throw;
            }
        }
        //UPDATE & Save Inventory Detail from Edit View
        public async Task Update(Inventory Inventories)
        {
            try
            {
                db.Entry(Inventories).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }
        public async Task Delete(string id)
        {
            try
            {
                int Id = Convert.ToInt32(id);
                Inventory Inventories = await db.Inventories.FindAsync(Id);
                db.Inventories.Remove(Inventories);
                await db.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        private bool InventoryExists(string id)
        {
            int Id = Convert.ToInt32(id);
            return db.Inventories.Count(e => e.ItemId == Id) > 0;
        }

    }
}