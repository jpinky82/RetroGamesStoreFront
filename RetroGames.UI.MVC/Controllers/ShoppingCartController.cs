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
            var sessionCart = HttpContext.Session.GetString("cart");

            Dictionary<int, CartItemViewModel> shoppingCart = null;

            if (sessionCart == null || sessionCart.Count() == 0)
            {
                shoppingCart = new Dictionary<int, CartItemViewModel>();

                ViewBag.Message = "There are no items in your cart.";
            }
            else
            {
                ViewBag.Message = null;

                shoppingCart = JsonConvert.DeserializeObject<Dictionary<int, CartItemViewModel>>(sessionCart);
            }



            return View(shoppingCart);
        }

        public IActionResult AddToCart(int id)
        {
            Dictionary<int, CartItemViewModel> shoppingCart = null;

            var sessionCart = HttpContext.Session.GetString("cart");

            if (String.IsNullOrEmpty(sessionCart))
            {
                shoppingCart = new Dictionary<int, CartItemViewModel>();
            }
            else
            {
                shoppingCart = JsonConvert.DeserializeObject<Dictionary<int, CartItemViewModel>>(sessionCart);
            }

            Product product = _context.Products.Find(id);

            CartItemViewModel civm = new CartItemViewModel(1, product);

            if (shoppingCart.ContainsKey(product.ProductId))
            {
                shoppingCart[product.ProductId].Qty++;
            }
            else
            {
                shoppingCart.Add(product.ProductId, civm);
            }

            string jsonCart = JsonConvert.SerializeObject(shoppingCart);

            HttpContext.Session.SetString("cart", jsonCart);

            return RedirectToAction("Index", "Products");
        }

        public IActionResult RemoveFromCart(int id)
        {
            var sessionCart = HttpContext.Session.GetString("cart");

            Dictionary<int, CartItemViewModel> shoppingCart = JsonConvert.DeserializeObject<Dictionary<int, CartItemViewModel>>(sessionCart);

            shoppingCart.Remove(id);

            if(shoppingCart.Count == 0)
            {
                HttpContext.Session.Remove("cart");
            }
            else
            {
                string jsonCart = JsonConvert.SerializeObject(shoppingCart);
                HttpContext.Session.SetString("cart", jsonCart);
            }

            return RedirectToAction("Index");
        }

        public IActionResult UpdateCart(int productId, int qty)
        {
            var sessionCart = HttpContext.Session.GetString("cart");

            Dictionary<int, CartItemViewModel> shoppingCart = JsonConvert.DeserializeObject<Dictionary<int, CartItemViewModel>>(sessionCart);

            shoppingCart[productId].Qty = qty;

            string jsonCart = JsonConvert.SerializeObject(shoppingCart);
            HttpContext.Session.SetString("cart", jsonCart);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> SubmitOrder()
        {
            #region Planning Out Order Submission

            //The Big Picture:
            //Create an Order object when the user submits and save it to the database
            // - OrderDate (supplied programmaticallly)
            // - UserId (supplied by the active user)
            // - shipToName, ShipCity, ShipState, ShipZip > this info will be pulled from 
            //      the UserDetails table by looking up the record for the current UserId.
            //Add the record to the _context (queue it up to be added in the DB)
            //Save the database changes 

            //Create an OrderProduct object for each item in the cart
            // - ProductId > Pulled from the cart
            // - OrderId > Pulled from the Order object
            // - Qty > pulled from the cart
            // - ProductPrice > Available from the cart.
            //Add the record to the _context
            //Save the database changes.

            #endregion

            //Retrieve the current user's Id
            //The code below is a mechanism provided by Identity to retrieve the UserID in the
            //current HttpContext. If you need to retrieve the UserId in any controller, you
            //can use this.

            string? userId = (await _userManager.GetUserAsync(HttpContext.User))?.Id;

            //Retrieve the UserDetails record
            User ud = _context.Users.Find(userId);

            //Create the order object
            Order o = new Order()
            {
                UserId = userId,
                OrderDate = DateTime.Now,
                ShipToName = ud.FirstName + " " + ud.LastName,
                ShipCity = ud.City,
                ShipState = ud.State,
                ShipZip = ud.Zip
            };

            //Add the Order to _context
            _context.Orders.Add(o);

            //Retrieve the session shopping cart
            var sessionCart = HttpContext.Session.GetString("cart");

            //Convert it to C#
            Dictionary<int, CartItemViewModel> shoppingCart = JsonConvert.DeserializeObject<Dictionary<int, CartItemViewModel>>(sessionCart);

            //Create an OrderProduct object for each item in the cart
            foreach (var item in shoppingCart)
            {
                OrderProduct op = new OrderProduct()
                {
                    ProductId = item.Key,
                    OrderId = o.OrderId,
                    Quantity = (short)item.Value.Qty,
                    ProductPrice = item.Value.Product.ProductPrice
                };

                //For linking table records, we can add items/records to an existing entity
                //if the records are related.
                o.OrderProducts.Add(op);    

                ////remove the item from the cart - alternative to the Session.Remove below
                //shoppingCart.Remove(item.Key);
            }

            //Save the changes to the database
            _context.SaveChanges();

            //Removing the cart string from the session
            HttpContext.Session.Remove("cart");

            TempData["SuccessMessage"] = "Order created successfully.";

            //Redirect the user to the Orders Index
            //return RedirectToAction("Index", "Home");
            return RedirectToAction("Details", "Orders", new { id = o.OrderId });
        }
    }
}
