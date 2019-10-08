using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop_Help.Models;

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
        [TempData]
        public string Store { get; set; }
        [TempData]
        public int Zip { get; set; }
        

        public HomeController(ShopHelpContext dbContext)
        {
            _dbContext = dbContext;
            foreach(var item in dbContext.Itemtype)
            {
                categories.Add(item.Itemtypeid, item.Itemtypename);
            }
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult SelectStore(string store, int zip)
        {
            
            Store = store;
            Zip = zip;
            return View(_dbContext.Itemtype);
        }

        public IActionResult Privacy(int id)
        {
            string name = categories[id];
            ViewBag.Name = name;
            ViewBag.Store = Store;
            ViewBag.Zip = Zip;

            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
