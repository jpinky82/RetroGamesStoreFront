using System;
using System.Collections.Generic;

namespace RetroGames.DATA.EF.Models
{
    public partial class Product
    {
        public Product()
        {
            OrderProducts = new HashSet<OrderProduct>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public decimal ProductPrice { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string? ProductDescription { get; set; }
        public short UnitsInStock { get; set; }
        public int? ConsoleTypeId { get; set; }
        public int ManufacturerId { get; set; }
        public int CategoryId { get; set; }
        public int? GenreId { get; set; }
        public bool IsDiscontinued { get; set; }
        public string? ProductImage { get; set; }

        public virtual Category? Category { get; set; }
        public virtual ConsoleType? ConsoleType { get; set; }
        public virtual Genre? Genre { get; set; }
        public virtual Manufacturer? Manufacturer { get; set; }
        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
