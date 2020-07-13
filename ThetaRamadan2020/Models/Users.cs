using System;
using System.Collections.Generic;

namespace ThetaRamadan2020.Models
{
    public partial class Users
    {
        public Users()
        {
            SaledItems = new HashSet<SaledItems>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string UserRole { get; set; }
        public string Password { get; set; }
        public string Mobile { get; set; }

        public virtual ICollection<SaledItems> SaledItems { get; set; }
    }
}
