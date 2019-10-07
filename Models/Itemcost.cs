using System;
using System.Collections.Generic;

namespace Shop_Help.Models
{
    public partial class Itemcost
    {
        public int Itemcostid { get; set; }
        public int? Storeid { get; set; }
        public int? Itemid { get; set; }
        public decimal? Itemcost1 { get; set; }

        public virtual Items Item { get; set; }
        public virtual Stores Store { get; set; }
    }
}
