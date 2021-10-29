using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using InventoryAPI.Models;
using System.Threading.Tasks;

namespace InventoryManagement.Controllers
{
    public class InventoriesController : Controller
    {
        // apiBaseAddress - <app key defined in web.config file
        readonly string apiBaseAddress = ConfigurationManager.AppSettings["apiBaseAddress"];
        //Index to be called when website is called Default
        public async Task<ActionResult> Index()
        {
            IEnumerable<Inventory> Inventories = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiBaseAddress);

                var result = await client.GetAsync("api/Inventory/get");

                if (result.IsSuccessStatusCode)
                {
                    Inventories = await result.Content.ReadAsAsync<IList<Inventory>>();
                }
                else
                {
                    Inventories = Enumerable.Empty<Inventory>();
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            return View(Inventories);
        }

        //Fetch Details with ItemId Column
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Inventory Inventories = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiBaseAddress);

                var result = await client.GetAsync($"api/Inventory/details/{id}");

                if (result.IsSuccessStatusCode)
                {
                    Inventories = await result.Content.ReadAsAsync<Inventory>();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }

            if (Inventories == null)
            {
                return HttpNotFound();
            }
            return View(Inventories);
        }
        //Get Respose for Create Item View Page
        public ActionResult Create()
        {
            return View();
        }

        //POST - Create Item Data
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Name,Description,Price,Unit")] Inventory Inventories)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiBaseAddress);

                    var response = await client.PostAsJsonAsync("api/Inventory/Create", Inventories);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Server error try after some time.");
                    }
                }
            }
            return View(Inventories);
        }

        //Fetch Data to Edit with ItemId={id}
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inventory Inventories = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiBaseAddress);

                /*var result = await client.GetAsync("api/Inventory/details/{id}");*/
                var result = await client.GetAsync("api/Inventory/details/" + id); 

                if (result.IsSuccessStatusCode)
                {
                    Inventories = await result.Content.ReadAsAsync<Inventory>();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            if (Inventories == null)
            {
                return HttpNotFound();
            }
            return View(Inventories);
        }

        //POST Edited Data into Database to update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ItemId,Name,Description,Price,Unit")] Inventory Inventories)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiBaseAddress);
                    var response = await client.PutAsJsonAsync("api/Inventory/edit", Inventories);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Server error try after some time.");
                    }
                }
                return RedirectToAction("Index");
            }
            return View(Inventories);
        }

        //TO Render Delete View with Details as per ItemId={id} 
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inventory Inventories = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiBaseAddress);

                var result = await client.GetAsync("api/Inventory/details/"+id);

                if (result.IsSuccessStatusCode)
                {
                    Inventories = await result.Content.ReadAsAsync<Inventory>();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }

            if (Inventories == null)
            {
                return HttpNotFound();
            }
            return View(Inventories);
        }
        //DeleteConfirmed Task to Delete Row based on ItemId={id} after delete confirmation from Delete View with details
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiBaseAddress);

                var response = await client.DeleteAsync("api/Inventory/delete/"+id);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
            }
            return View();
        }
    }
}
