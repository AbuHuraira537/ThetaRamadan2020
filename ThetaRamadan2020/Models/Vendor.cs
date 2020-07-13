using System;
using System.Collections.Generic;

namespace ThetaRamadan2020.Models
{
    public partial class Vendor
    {
        public Vendor()
        {
            Items = new HashSet<Items>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string VendorCompany { get; set; }
        public string VendorEmail { get; set; }

        public virtual ICollection<Items> Items { get; set; }
    }
}
