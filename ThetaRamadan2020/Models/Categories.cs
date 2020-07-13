using System;
using System.Collections.Generic;

namespace ThetaRamadan2020.Models
{
    public partial class Categories
    {
        public Categories()
        {
            Items = new HashSet<Items>();
            PurchasedItems = new HashSet<PurchasedItems>();
            SaledItems = new HashSet<SaledItems>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int? TotalItems { get; set; }

        public virtual ICollection<Items> Items { get; set; }
        public virtual ICollection<PurchasedItems> PurchasedItems { get; set; }
        public virtual ICollection<SaledItems> SaledItems { get; set; }
    }
}
