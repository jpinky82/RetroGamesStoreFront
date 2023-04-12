using System;
using System.Collections.Generic;

namespace RetroGames.DATA.EF.Models
{
    public partial class Manufacturer
    {
        public Manufacturer()
        {
            Products = new HashSet<Product>();
        }

        public int ManufacturerId { get; set; }
        public string ManufacturerName { get; set; } = null!;
        public string? ManAddress { get; set; }
        public string? ManCity { get; set; }
        public string? ManState { get; set; }
        public string? ManZip { get; set; }
        public string? ManPhone { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
