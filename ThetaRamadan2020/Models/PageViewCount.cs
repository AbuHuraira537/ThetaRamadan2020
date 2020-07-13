using System;
using System.Collections.Generic;

namespace ThetaRamadan2020.Models
{
    public partial class PageViewCount
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public int Count { get; set; }
    }
}
