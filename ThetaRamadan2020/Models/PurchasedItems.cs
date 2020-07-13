using System;
using System.Collections.Generic;

namespace ThetaRamadan2020.Models
{
    public partial class PurchasedItems
    {
        public int Id { get; set; }
        public int ItemCategory { get; set; }
        public int Item { get; set; }
        public int ItemCount { get; set; }
        public DateTime PurchasedDate { get; set; }
        public double PerItemPrice { get; set; }

        public virtual Categories ItemCategoryNavigation { get; set; }
        public virtual Items ItemNavigation { get; set; }
    }
}
