using Microsoft.AspNetCore.Mvc;
using RetroGames.UI.MVC.Models;
using RetroGames.DATA.EF.Models;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using MailKit.Net.Smtp;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace RetroGames.UI.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly RetroGamesContext _context;
		private readonly IConfiguration _config;

		public HomeController(ILogger<HomeController> logger, RetroGamesContext context, IConfiguration config)
        {
            _logger = logger;
            _context = context;
			_config = config;
		}

        public IActionResult Index()
        {

            var products = _context.Products.Include(p => p.Category).Include(p => p.ConsoleType).Include(p => p.Genre).Include(p => p.Manufacturer).ToList();
            Random rnd = new Random();
            int randNum;

            List<Product> indexProducts = new List<Product>();

            for (int i = 0; i < 8; i++)
            {
                randNum = rnd.Next(0, products.Count());
                indexProducts.Add(products[randNum]);
                
                products.RemoveAt(randNum);
            }


            return View(indexProducts);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Products()
        {
            return View();
        }

        public IActionResult SingleProduct()
        {
            return View();
        }

        public IActionResult Cart()
        {
            return View();
        }
        public IActionResult Checkout()
        {
            return View();
        }

		public IActionResult Contact()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Contact(ContactViewModel cvm)
        {
			//Check if the model is valid before you do anything else.
			if (!ModelState.IsValid)
			{
				//Send them back to the form. We can pass the object to the View
				//So the form will contain the original information they provided.
				return View(cvm);
			}

			string message = $"You have received a new email from your site's contact form!<br />" +
			   $"Sender: {cvm.Name}<br />" +
			   $"Email: {cvm.Email}<br />" +
			   $"Subject: {cvm.Subject}<br />" +
			   $"Message: {cvm.Message}";

			//Create a MimeMessage object to assist with storing/transferring the email information
			//from the contact form.
			var mm = new MimeMessage();

			//Even though the user is the one attempting to send a message to us, the actual sender of the email
			//is the "email user" we set up in SmarterASP

			mm.From.Add(new MailboxAddress("Sender", _config.GetValue<string>("Credentials:Email:User")));
			//The recipient of this email wil be our personal email address, also stored in appsettings.json
			mm.To.Add(new MailboxAddress("Personal", _config.GetValue<string>("Credentials:Email:Recipient")));

			//We can add the user's provided email address to the list of ReplyTo addresses so our replies
			//can be sent directly to them instead of the "email user" on smarter asp.
			mm.ReplyTo.Add(new MailboxAddress("User", cvm.Email));

			//the subject will be the subject provided by the user in the form. We have this stored inthe cvm object.
			mm.Subject = cvm.Subject;

			//if we don't have any HTML, we can assing the body as mm.Body = message.
			mm.Body = new TextPart("HTML") { Text = message };

			//We can set the priority of the message so it will be flagged in our email client.
			mm.Priority = MessagePriority.Urgent;

			//the using directive will create an SMTP Client object used to send the email.
			//Once all of the code inside is done, it will close any open connections an dispose the object for us.
			using (var client = new SmtpClient())
			{

				try
				{
					client.Connect(_config.GetValue<string>("Credentials:Email:Client"), Convert.ToInt32(_config.GetValue<string>("Credentials:Email:Port")), MailKit.Security.SecureSocketOptions.SslOnConnect);
					//log in to the mail server using the credentials for our email user
					client.Authenticate(
					//username
						_config.GetValue<string>("Credentials:Email:User"),
					//password
						_config.GetValue<string>("Credentials:Email:Password")
						);
					client.Send(mm);
				}
				catch (Exception ex)
				{

					//If there is an issue, we can store an error message in a ViewBag variable to be displayed in the View
					ViewBag.ErrorMessage = $"There was an issue processing your request. Please" +
						$" try again later. <br/>Error Message: {ex.Message}";
					return View(cvm);
				}
			}//end using

			//if all goes well, return a View that displays a confirmation to the user that their email was sent successfully
			return View("EmailConfirmation", cvm);
		}



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}