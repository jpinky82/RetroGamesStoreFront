using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace RetroGames.DATA.EF.Models//.Metadata
{
	//internal class Partials
	//{
	//}
	[ModelMetadataType(type: typeof(CategoryMetadata))]
	public partial class Category { }

	[ModelMetadataType(type: typeof(ConsoleTypeMetadata))]
	public partial class ConsoleType { }

	[ModelMetadataType(type: typeof(GenreMetadata))]
	public partial class Genre { }

	[ModelMetadataType(type: typeof(ManufacturerMetadata))]
	public partial class Manufacturer { }

	[ModelMetadataType(type: typeof(OrderMetadata))]
	public partial class Order { }

	[ModelMetadataType(type: typeof(OrderProductMetadata))]
	public partial class OrderProduct { }

	[ModelMetadataType(type: typeof(ProductMetadata))]
	public partial class Product 
	{
        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }

	[ModelMetadataType(type: typeof(UserMetadata))]
	public partial class User { }
}
