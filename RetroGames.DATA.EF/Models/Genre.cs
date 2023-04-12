using System;
using System.Collections.Generic;

namespace RetroGames.DATA.EF.Models
{
    public partial class Genre
    {
        public Genre()
        {
            Products = new HashSet<Product>();
        }

        public int GenreId { get; set; }
        public string GenreName { get; set; } = null!;
        public string? GenreDescription { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
