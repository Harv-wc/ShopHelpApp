using System;
using System.Collections.Generic;

namespace Shop_Help.Models
{
    public partial class Items
    {
        public Items()
        {
            Itemcost = new HashSet<Itemcost>();
        }

        public int Itemid { get; set; }
        public int? Itemtypeid { get; set; }
        public string Itemname { get; set; }

        public virtual Itemtype Itemtype { get; set; }
        public virtual ICollection<Itemcost> Itemcost { get; set; }
    }
}
