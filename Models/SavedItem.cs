using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Help.Models
{
    public class SavedItem
    {
        public string Name { get; set; }
        public int Qty { get; set; }
        public decimal Cost { get; set; }

        public SavedItem(string name, decimal cost)
        {
            Name = name;
            Qty = 1;
            Cost = cost;
        }
    }
}
