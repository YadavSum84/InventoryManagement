
using System.Data.Entity;
using InventoryAPI.Models;

namespace InventoryManagement.Models
{
    public class SqlDbContext : DbContext
    {
        public SqlDbContext() : base("name=SqlConn")
        {
        }
        public DbSet<Inventory> Inventories { get; set; }
    }
}