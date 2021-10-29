using System;
using System.Collections.Generic;
using System.Web.Http;
using InventoryAPI.Models;
using InventoryManagement.Models;
using System.Threading.Tasks;

namespace InventoryManagement.Controllers
{
    public class InventoryApiController : ApiController
    {
        private readonly IInventoryRepository _iInventoryRepository = new InventoryRepository();

        [HttpGet]
        [Route("api/Inventory/Get")]
        public async Task<IEnumerable<Inventory>> Get()
        {
            return await _iInventoryRepository.GetInventories();
        }
        [HttpPost]
        [Route("api/Inventory/Create")]
        public async Task CreateAsync([FromBody] Inventory Inventories)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _iInventoryRepository.Add(Inventories);
                }
            }
            catch(Exception ex)
            {

            }
        }
        [HttpGet]
        [Route("api/Inventory/Details/{id}")]
        public async Task<Inventory> Details(string id)
        {
            var result = await _iInventoryRepository.GetInventory(id);
            return result;
        }
        [HttpPut]
        [Route("api/Inventory/Edit")]
        public async Task EditAsync([FromBody] Inventory Inventories)
        {
            if (ModelState.IsValid)
            {
                await _iInventoryRepository.Update(Inventories);
            }
        }
        [HttpDelete]
        [Route("api/Inventory/Delete/{id}")]
        public async Task DeleteConfirmedAsync(string id)
        {
            await _iInventoryRepository.Delete(id);
        }
    }
}