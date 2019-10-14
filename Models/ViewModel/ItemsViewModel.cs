using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shop_Help.Models;

namespace Shop_Help.Models.ViewModel
{
    public class ItemsViewModel
    {

        public Dictionary<int, string> ItemTypes { get; set; } = new Dictionary<int, string>();
        public List<Items> Items { get; set; } = new List<Items>();
       
        

    }
}
