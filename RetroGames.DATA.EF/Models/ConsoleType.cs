using System;
using System.Collections.Generic;

namespace RetroGames.DATA.EF.Models
{
    public partial class ConsoleType
    {
        public ConsoleType()
        {
            Products = new HashSet<Product>();
        }

        public int ConsoleTypeId { get; set; }
        public string ConsoleName { get; set; } = null!;

        public virtual ICollection<Product> Products { get; set; }
    }
}
