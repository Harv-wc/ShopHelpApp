using System;
using System.Collections.Generic;

namespace Shop_Help.Models
{
    public partial class Stores
    {
        public Stores()
        {
            Itemcost = new HashSet<Itemcost>();
        }

        public int Storeid { get; set; }
        public int? Locationid { get; set; }
        public string Storename { get; set; }
        public string Storeaddress { get; set; }

        public virtual Locations Location { get; set; }
        public virtual ICollection<Itemcost> Itemcost { get; set; }
    }
}
