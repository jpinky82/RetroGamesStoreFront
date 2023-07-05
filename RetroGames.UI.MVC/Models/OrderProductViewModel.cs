using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace RetroGames.UI.MVC.Models
{
    public class OrderProductViewModel
    {

        public int OrderId { get; set; }
        public short? OrderQty { get; set; }
        public string ProdName { get; set; }
        public decimal? Price { get; set; }
    }
}
