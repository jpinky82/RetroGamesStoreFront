using Microsoft.AspNetCore.Mvc;
using RetroGames.DATA.EF.Models;
using RetroGames.UI.MVC.Models;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;


namespace RetroGames.UI.MVC.Controllers
{
    public class ShoppingCartController : Controller
    {

        private readonly RetroGamesContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ShoppingCartController(RetroGamesContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddToCart(int id)
        {
            Dictionary<int, CartItemViewModel> shoppingCart = null;

            var sessionCart = HttpContext.Session.GetString("cart");

            return View();
        }
    }
}
