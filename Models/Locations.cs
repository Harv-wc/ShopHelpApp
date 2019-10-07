using System;
using System.Collections.Generic;

namespace Shop_Help.Models
{
    public partial class Locations
    {
        public Locations()
        {
            Stores = new HashSet<Stores>();
        }

        public int Locationid { get; set; }
        public int? Zipcode { get; set; }

        public virtual ICollection<Stores> Stores { get; set; }
    }
}
