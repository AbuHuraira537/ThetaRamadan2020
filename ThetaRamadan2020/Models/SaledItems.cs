using System;
using System.Collections.Generic;

namespace ThetaRamadan2020.Models
{
    public partial class SaledItems
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime SaledDate { get; set; }
        public int CategoryId { get; set; }
        public int ItemId { get; set; }
        public int ItemCount { get; set; }
        public double TotalBill { get; set; }

        public virtual Categories Category { get; set; }
        public virtual Users Customer { get; set; }
        public virtual Items Item { get; set; }
    }
}
