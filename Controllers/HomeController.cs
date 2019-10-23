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
        private readonly Dictionary<int, string> stores = new Dictionary<int, string>();
        public ItemsViewModel itemsViewModel = new ItemsViewModel { };
        [TempData]
        public int Storeid { get; set; }
        [TempData]
        public string StoreName { get; set; }
        

        public HomeController(ShopHelpContext dbContext)
        {
            _dbContext = dbContext;
            foreach (var item in dbContext.Itemtype)
            {
                itemsViewModel.ItemTypes.Add(item.Itemtypeid, item.Itemtypename); // do not need -- will remove/replace
                categories.Add(item.Itemtypeid, item.Itemtypename);
            }
            foreach (var item in dbContext.Items) // do not need -- will remove/replace
            {
                itemsViewModel.Items.Add(item);
            }
            foreach(var item in dbContext.Stores)
            {
                stores.Add(item.Storeid, item.Storename);
            }
        }

        public IActionResult Index()
        {
            return View();
        }

        
        public IActionResult StoreItems(int id = 1)
        {
            ViewBag.Table = from Itemcost in _dbContext.Itemcost
                            join Items in _dbContext.Items on Itemcost.Itemid equals Items.Itemid
                            where Itemcost.Storeid == Storeid && Items.Itemtypeid == id
                            select Itemcost;
            
            ViewBag.Categories = categories;
            ViewBag.Store = TempData["StoreName"];

            TempData.Keep("StoreName");
            TempData.Keep("Storeid");
            return View(itemsViewModel); // change viewmodel to pass the generated list.
        }

        public IActionResult Items()
        {
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
        public IActionResult SetStore(int store) // Only called by the index when first choosing a store. default id is for category.
        {
            Storeid = store;
            StoreName = stores[store];

            ViewBag.Table = from Itemcost in _dbContext.Itemcost
                            join Items in _dbContext.Items on Itemcost.Itemid equals Items.Itemid
                            where Itemcost.Storeid == Storeid && Items.Itemtypeid == 0
                            select Itemcost;

            ViewBag.Store = TempData["StoreName"];
            ViewBag.Categories = categories;

            TempData.Keep("StoreName");
            TempData.Keep("Storeid");
            return View("StoreItems");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
