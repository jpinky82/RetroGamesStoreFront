using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RetroGames.DATA.EF.Models//.Metadata
{
	//internal class Metadata
	//{
	//}

	public class CategoryMetadata
	{
		public int CategoryId { get; set; }

		[Required(ErrorMessage = "Name is required")]
		[StringLength(50)]
		[Display(Name = "Category")]
		public string CategoryName { get; set; } = null!;

		[StringLength(500)]
		[Display(Name = "Description")]
		[DisplayFormat(NullDisplayText="n/a")]
		public string? CategoryDescription { get; set; }
	}

	public class ConsoleTypeMetadata
	{
		public int ConsoleTypeId { get; set; }

		[Required(ErrorMessage = "Name is required")]
		[StringLength(50)]
		[Display(Name = "Console")]
		public string ConsoleName { get; set; } = null!;
	}

	public class GenreMetadata
	{
		public int GenreId { get; set; }

		[Required(ErrorMessage = "Name is required")]
		[StringLength(50)]
		[Display(Name = "Genre")]
		public string GenreName { get; set; } = null!;

		[StringLength(500)]
		[Display(Name = "Description")]
		[DisplayFormat(NullDisplayText = "n/a")]
		public string? GenreDescription { get; set; }
	}
	public class ManufacturerMetadata
	{
		public int ManufacturerId { get; set; }

		[Required(ErrorMessage = "Name is required")]
		[StringLength(200)]
		[Display(Name = "Manufacturer")]
		public string ManufacturerName { get; set; } = null!;

		[StringLength(150)]
		[Display(Name = "Address")]
		[DisplayFormat(NullDisplayText = "n/a")]
		public string? ManAddress { get; set; }

		[StringLength(50)]
		[Display(Name = "City")]
		[DisplayFormat(NullDisplayText = "n/a")]
		public string? ManCity { get; set; }

		[StringLength(2)]
		[Display(Name = "State")]
		[DisplayFormat(NullDisplayText = "n/a")]
		public string? ManState { get; set; }

		[StringLength(5)]
		[Display(Name = "Zip Code")]
		[DisplayFormat(NullDisplayText = "n/a")]
		public string? ManZip { get; set; }

		[StringLength(24)]
		[Display(Name = "Phone")]
		[DisplayFormat(NullDisplayText = "n/a")]
		public string? ManPhone { get; set; }

	}
	public class OrderMetadata
	{
		public int OrderId { get; set; }
		public string UserId { get; set; } = null!;

		[Required(ErrorMessage = "Date is Required")]
		[Display(Name = "Date")]
		[DisplayFormat(DataFormatString ="{0:d}, ApplyFormatInEditMode=true")]
		public DateTime OrderDate { get; set; }

		[Required(ErrorMessage = "Name is required")]
		[StringLength(150)]
		[Display(Name = "Name")]
		public string ShipToName { get; set; } = null!;

		[Required(ErrorMessage = "City is required")]
		[StringLength(50)]
		[Display(Name = "City")]
		public string ShipCity { get; set; } = null!;

		[StringLength(2)]
		[Display(Name = "State")]
		public string? ShipState { get; set; }

		[Required(ErrorMessage = "Zip Code is Required")]
		[StringLength(5)]
		[Display(Name = "Zip Code")]
		public string ShipZip { get; set; } = null!;
	}
	public class OrderProductMetadata
	{
		public int OrderProductId { get; set; }
		public int ProductId { get; set; }
		public int OrderId { get; set; }

		[Range(0, (double)(short.MaxValue))]
		public short? Quantity { get; set; }

		[Range(0, (double)(decimal.MaxValue))]
		[Display(Name = "Price")]
		[DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:c}")]
		public decimal? ProductPrice { get; set; }
	}
	public class ProductMetadata
	{
		public int ProductId { get; set; }

		[Required(ErrorMessage = "Name is required")]
		[StringLength(200)]
		[Display(Name = "Name")]
		public string ProductName { get; set; } = null!;

		[Required(ErrorMessage = "Price is Required")]
		[Range(0, (double)(decimal.MaxValue))]
		[Display(Name = "Price")]
		[DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:c}")]
		public decimal ProductPrice { get; set; }

		[Display(Name = "Date")]
		[DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode=true)]
		public DateTime? ReleaseDate { get; set; }

		[StringLength(500)]
		[Display(Name = "Description")]
		[DisplayFormat(NullDisplayText = "n/a")]
		public string? ProductDescription { get; set; }

		[Required(ErrorMessage = "Units In Stock is Required")]
		[Range(0, (double)(short.MaxValue))]
		[Display(Name = "Stock")]
		public short UnitsInStock { get; set; }
		public int? ConsoleTypeId { get; set; }
		public int ManufacturerId { get; set; }
		public int CategoryId { get; set; }
		public int? GenreId { get; set; }

		[Display(Name = "Discontinued?")]
		public bool IsDiscontinued { get; set; }

		[StringLength(75)]
		[Display(Name = "Image")]
		public string? ProductImage { get; set; }
	}
	public class UserMetadata
	{
		public string UserId { get; set; } = null!;

		[Required(ErrorMessage = "First Name is required")]
		[StringLength(50)]
		[Display(Name = "First Name")]
		public string FirstName { get; set; } = null!;

		[Required(ErrorMessage = "Last Name is required")]
		[StringLength(50)]
		[Display(Name = "Last Name")]
		public string LastName { get; set; } = null!;

		[StringLength(150)]
		[DisplayFormat(NullDisplayText = "n/a")]
		public string? Address { get; set; }

		[StringLength(50)]
		[DisplayFormat(NullDisplayText = "n/a")]
		public string? City { get; set; }

		[StringLength(2)]
		public string? State { get; set; }

		[StringLength(5)]
		[Display(Name = "Zip Code")]
		[DisplayFormat(NullDisplayText = "n/a")]
		public string? Zip { get; set; }

		[StringLength(24)]
		[DisplayFormat(NullDisplayText = "n/a")]
		public string? Phone { get; set; }
	}

}
