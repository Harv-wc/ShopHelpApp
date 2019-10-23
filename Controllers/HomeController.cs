using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop_Help.Models;
using Shop_Help.Models.ViewModel;

namespace Shop_Help.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly ShopHelpContext _dbContext;
        private readonly Dictionary<int, string> categories = new Dictionary<int, string>();
        public ItemsViewModel itemsViewModel = new ItemsViewModel { };
        [TempData]
        public int Store { get; set; }
        [TempData]
        public int Zip { get; set; }
        

        public HomeController(ShopHelpContext dbContext)
        {
            _dbContext = dbContext;
            foreach (var item in dbContext.Itemtype)
            {
                itemsViewModel.ItemTypes.Add(item.Itemtypeid, item.Itemtypename);
            }
            foreach (var item in dbContext.Items)
            {
                
                itemsViewModel.Items.Add(item);
            }
        }

        public IActionResult Index()
        {
            return View();
        }

        
        public IActionResult StoreItems(int id = 1, int store = 0, int zip = 0) //change store to storeid pass int instead of string
        {

            var name = itemsViewModel.ItemTypes[id];
            
            if (store != 0)
            {
                Store = store;
            }
            if (zip != 0)
            {
                Zip = zip;
            }

            ViewBag.Table = from Itemcost in _dbContext.Itemcost
                            join Items in _dbContext.Items on Itemcost.Itemid equals Items.Itemid
                            where Itemcost.Storeid == store && Items.Itemtypeid == id
                            select Itemcost;

            ViewBag.Name = name;
            ViewBag.Store = TempData["Store"];
            ViewBag.Zip = TempData["Zip"];
            TempData.Keep("Store");
            TempData.Keep("Zip");

            return View(itemsViewModel); // needs model view containing ItemType and Items
        }

        public IActionResult Items(int id)
        {
            string name = categories[id];
            ViewBag.Name = name;
            ViewBag.Store = TempData["Store"];
            ViewBag.Zip = TempData["Zip"];
            TempData.Keep("Store");
            TempData.Keep("Zip");
            return View();
        }
        
        public IActionResult GetStores(int zip)
        {
            var stores =
                from Locations in _dbContext.Locations
                join Stores in _dbContext.Stores on Locations.Locationid equals Stores.Locationid
                where Locations.Zipcode == zip
                select Stores;

            return Json(stores);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
