using System;
using System.Collections.Generic;

namespace Shop_Help.Models
{
    public partial class Itemtype
    {
        public Itemtype()
        {
            Items = new HashSet<Items>();
        }

        public int Itemtypeid { get; set; }
        public string Itemtypename { get; set; }

        public virtual ICollection<Items> Items { get; set; }
    }
}
