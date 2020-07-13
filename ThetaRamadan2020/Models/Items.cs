using System;
using System.Collections.Generic;

namespace ThetaRamadan2020.Models
{
    public partial class Items
    {
        public Items()
        {
            PurchasedItems = new HashSet<PurchasedItems>();
            SaledItems = new HashSet<SaledItems>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public double PerItemPrice { get; set; }
        public string Images { get; set; }
        public string Color { get; set; }
        public string Model { get; set; }
        public int CategoryId { get; set; }
        public int VendorId { get; set; }

        public virtual Categories Category { get; set; }
        public virtual Vendor Vendor { get; set; }
        public virtual ICollection<PurchasedItems> PurchasedItems { get; set; }
        public virtual ICollection<SaledItems> SaledItems { get; set; }
    }
}
