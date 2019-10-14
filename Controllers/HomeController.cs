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
        /* Actions needed:
         * -returns index with all cleared fields. My "Restart" action
         * -returns index with zipcode entered. allows to select store from drop down menu. Zipcode action
         * -returns index with selected store. Displays categories, and empty items window, and empty list.
         */
        private readonly ShopHelpContext _dbContext;
        private readonly Dictionary<int, string> categories = new Dictionary<int, string>();
        public ItemsViewModel itemsViewModel = new ItemsViewModel { };
        [TempData]
        public string Store { get; set; }
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

        
        public IActionResult StoreItems(int id = 1, string store = "Default", int zip = 0)
        {

            var name = itemsViewModel.ItemTypes[id];
            // string name = categories[id];
            if (store != "Default")
            {
                Store = store;
            }
            if (zip != 0)
            {
                Zip = zip;
            }
            
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


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
