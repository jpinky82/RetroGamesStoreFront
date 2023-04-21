using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

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
		[DisplayFormat(NullDisplayText="N/A")]
        [UIHint("MultilineText")]
        public string? CategoryDescription { get; set; }
	}

	public class ConsoleTypeMetadata
	{
		public int ConsoleTypeId { get; set; }

		[Required(ErrorMessage = "Name is required")]
		[StringLength(50, ErrorMessage = "Console Name cannot exceed 50 Characters")]
		[Display(Name = "Console")]
		public string ConsoleName { get; set; } = null!;
	}

	public class GenreMetadata
	{
		public int GenreId { get; set; }

		[Required(ErrorMessage = "Name is required")]
		[StringLength(50, ErrorMessage = "Name cannot exceed 50 Characters")]
		[Display(Name = "Genre")]
		public string GenreName { get; set; } = null!;

		[StringLength(500, ErrorMessage = "Description cannot exceed 500 Characters")]
		[Display(Name = "Description")]
		[DisplayFormat(NullDisplayText = "N/A")]
        [UIHint("MultilineText")]
        public string? GenreDescription { get; set; }
	}
	public class ManufacturerMetadata
	{
		public int ManufacturerId { get; set; }

		[Required(ErrorMessage = "Name is required")]
		[StringLength(200, ErrorMessage = "Name cannot exceed 200 Characters")]
		[Display(Name = "Name")]
		public string ManufacturerName { get; set; } = null!;

		[StringLength(150, ErrorMessage = "Address cannot exceed 150 Characters")]
		[Display(Name = "Address")]
		[DisplayFormat(NullDisplayText = "N/A")]
		public string? ManAddress { get; set; }

		[StringLength(50, ErrorMessage = "City cannot exceed 50 Characters")]
		[Display(Name = "City")]
		[DisplayFormat(NullDisplayText = "N/A")]
		public string? ManCity { get; set; }

		[StringLength(2, ErrorMessage = "Provide two letter Identifier for State")]
		[Display(Name = "State")]
		[DisplayFormat(NullDisplayText = "N/A")]
		public string? ManState { get; set; }

		[StringLength(5, ErrorMessage = "Zip Code cannot exceed 5 Characters")]
		[Display(Name = "Zip Code")]
		[DisplayFormat(NullDisplayText = "n/a")]
		public string? ManZip { get; set; }

		[StringLength(24, ErrorMessage = "Phone cannot exceed 24 Characters")]
		[Display(Name = "Phone")]
		[DisplayFormat(NullDisplayText = "N/A")]
        [DataType(DataType.PhoneNumber)]
        public string? ManPhone { get; set; }

	}
	public class OrderMetadata
	{
		public int OrderId { get; set; }
		public string UserId { get; set; } = null!;

		[Required(ErrorMessage = "Date is Required")]
		[Display(Name = "Date")]
		[DisplayFormat(DataFormatString ="{0:d}", ApplyFormatInEditMode=true)]
		public DateTime OrderDate { get; set; }

		[Required(ErrorMessage = "Name is required")]
		[StringLength(150, ErrorMessage = "Name cannot exceed 150 Characters")]
		[Display(Name = "Name")]
		public string ShipToName { get; set; } = null!;

        [Display(Name = "City")]
        [Required(ErrorMessage = "City is required")]
        [StringLength(50, ErrorMessage = "City cannot exceed 50 Characters")]
        public string ShipCity { get; set; } = null!;

        [Display(Name = "State")]
        [StringLength(2, ErrorMessage = "Provide two letter Identifier for State")]
        [DisplayFormat(NullDisplayText = "N/A")]
        public string? ShipState { get; set; }

		[Required(ErrorMessage = "Zip Code is Required")]
		[StringLength(5, ErrorMessage = "Zip Code cannot exceed 5 Characters")]
		[Display(Name = "Zip Code")]
		public string ShipZip { get; set; } = null!;
	}
	public class OrderProductMetadata
	{
		//public int OrderProductId { get; set; }
		//public int ProductId { get; set; }
		//public int OrderId { get; set; }

		[Range(0, (double)(short.MaxValue))]
		public short? Quantity { get; set; }

		[Range(0, (double)(decimal.MaxValue))]
		[Display(Name = "Price")]
        [DataType(DataType.Currency)]
        public decimal? ProductPrice { get; set; }
	}
	public class ProductMetadata
	{
		//public int ProductId { get; set; }

		[Required(ErrorMessage = "Name is Required")]
		[StringLength(200, ErrorMessage = "Name cannot exceed 200 Characters")]
		[Display(Name = "Name")]
		public string ProductName { get; set; } = null!;

		[Required(ErrorMessage = "Price is Required")]
		[Range(0, (double)(decimal.MaxValue))]
		[Display(Name = "Price")]
        [DataType(DataType.Currency)]
        public decimal ProductPrice { get; set; }

		[Display(Name = "Release Date")]
		[DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode=true)]
		public DateTime? ReleaseDate { get; set; }

		[StringLength(500, ErrorMessage = "Description cannot exceed 500 Characters")]
		[Display(Name = "Description")]
		[DisplayFormat(NullDisplayText = "N/A")]
        [UIHint("MultilineText")]
        public string? ProductDescription { get; set; }

		[Required(ErrorMessage = "Units In Stock is Required")]
		[Range(0, (double)(short.MaxValue))]
		[Display(Name = "Stock")]
		public short UnitsInStock { get; set; }

		//public int? ConsoleTypeId { get; set; }
		//public int ManufacturerId { get; set; }
		//public int CategoryId { get; set; }
		//public int? GenreId { get; set; }

		[Display(Name = "Discontinued?")]
		public bool IsDiscontinued { get; set; }

        [Display(Name = "Image")]
        public string? ProductImage { get; set; }
	}
	public class UserMetadata
	{
		public string UserId { get; set; } = null!;

		[Required(ErrorMessage = "First Name is required")]
		[StringLength(50, ErrorMessage = "First Name cannot exceed 50 Characters")]
		[Display(Name = "First Name")]
		public string FirstName { get; set; } = null!;

		[Required(ErrorMessage = "Last Name is required")]
		[StringLength(50, ErrorMessage = "Last Name cannot exceed 50 Characters")]
		[Display(Name = "Last Name")]
		public string LastName { get; set; } = null!;

		[StringLength(150, ErrorMessage = "Address cannot exceed 150 Characters")]
		[DisplayFormat(NullDisplayText = "N/A")]
		public string? Address { get; set; }

		[StringLength(50, ErrorMessage = "City cannot exceed 50 Characters")]
		[DisplayFormat(NullDisplayText = "N/A")]
		public string? City { get; set; }

        [Display(Name = "State")]
        [StringLength(2, ErrorMessage = "Provide two letter Identifier for State")]
        [DisplayFormat(NullDisplayText = "N/A")]
        public string? State { get; set; }

		[StringLength(5, ErrorMessage = "Zip Code cannot exceed 5 Characters")]
		[Display(Name = "Zip Code")]
        [DataType(DataType.PostalCode)]
        [DisplayFormat(NullDisplayText = "N/A")]
		public string? Zip { get; set; }

		[StringLength(24, ErrorMessage = "Phone cannot exceed 24 Characters")]
		[DisplayFormat(NullDisplayText = "N/A")]
		[DataType(DataType.PhoneNumber)]
		public string? Phone { get; set; }
	}

}
