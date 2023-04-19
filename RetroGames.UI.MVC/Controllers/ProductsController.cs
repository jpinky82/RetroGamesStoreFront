using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RetroGames.DATA.EF.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using RetroGames.UI.MVC.Utilities;
using System.Drawing;

namespace RetroGames.UI.MVC.Controllers
{


    [Authorize(Roles = "Admin")]
    public class ProductsController : Controller
    {
        private readonly RetroGamesContext _context;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductsController(RetroGamesContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Products
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("Admin"))
            {
                var products = _context.Products.Include(p => p.Category).Include(p => p.ConsoleType).Include(p => p.Genre).Include(p => p.Manufacturer);
                return View(await products.ToListAsync());
            }
            else
            {
                var products = _context.Products.Where(p => p.IsDiscontinued == false && p.UnitsInStock > 0)
                    .Include(p => p.Category).Include(p => p.ConsoleType).Include(p => p.Genre).Include(p => p.Manufacturer);
                return View(await products.ToListAsync());
            }
            
        }

        // GET: Products/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.ConsoleType)
                .Include(p => p.Genre)
                .Include(p => p.Manufacturer)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            ViewData["ConsoleTypeId"] = new SelectList(_context.ConsoleTypes, "ConsoleTypeId", "ConsoleName");
            ViewData["GenreId"] = new SelectList(_context.Genres, "GenreId", "GenreName");
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "ManufacturerId", "ManufacturerName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductName,ProductPrice,ReleaseDate,ProductDescription,UnitsInStock,ConsoleTypeId,ManufacturerId,CategoryId,GenreId,IsDiscontinued,ProductImage,ImageFile")] Product product)
        {
            if (ModelState.IsValid)
            {

                #region File Upload - Create w/Image Utility
                if (product.ImageFile != null)
                {
                    //process file

                    //check the file type
                    // - retrieve the extension of the uploaded file
                    string ext = Path.GetExtension(product.ImageFile.FileName);

                    // - Create a list of valid extensions
                    string[] validExts = { ".jpg", "jpeg", ".gif", ".png" };

                    // - Check the file extension against the list of valid extensions
                    if (validExts.Contains(ext.ToLower()) && product.ImageFile.Length < 4_194_303)//less than for megabytes as well.
                    {
                        // Generate a unique filename
                        product.ProductImage = Guid.NewGuid() + ext;

                        //Save the file to the web server (here it's saved to wwwroot/images)
                        // - retrieve the path to wwwroot
                        string webRootPath = _webHostEnvironment.WebRootPath;

                        // - create a variable for the full image path
                        string fullImagePath = webRootPath + "/images/";

                        // Create a MemoryStream to read the image into our web server's memory
                        using (var memoryStream = new MemoryStream())
                        {
                            await product.ImageFile.CopyToAsync(memoryStream);
                            using (var img = Image.FromStream(memoryStream))
                            {
                                //now, send the image to the ImageUtility for resizing and saving
                                //need 5 arguments for the utility to resize our image...
                                //1) (int) maximum image size
                                //2) (int) maximum thumbnail size
                                //3) (string) full path where the file will be saved
                                //4) (Image) an image
                                //5) (string) filename

                                int maxImageSize = 500;
                                int maxThumbSize = 100;

                                ImageUtility.ResizeImage(fullImagePath, product.ProductImage, img, maxImageSize, maxThumbSize);
                            }
                        }
                    }

                }
                else
                {
                    //assign a default image
                    //If no image was uploaded, assign a default filename
                    //Will also need to download a default image and name it 'noimage.png' -> place it in the wwwroot/images
                    product.ProductImage = "noimage.png";
                }
                #endregion

                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", product.CategoryId);
            ViewData["ConsoleTypeId"] = new SelectList(_context.ConsoleTypes, "ConsoleTypeId", "ConsoleName", product.ConsoleTypeId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "GenreId", "GenreName", product.GenreId);
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "ManufacturerId", "ManufacturerName", product.ManufacturerId);
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", product.CategoryId);
            ViewData["ConsoleTypeId"] = new SelectList(_context.ConsoleTypes, "ConsoleTypeId", "ConsoleName", product.ConsoleTypeId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "GenreId", "GenreName", product.GenreId);
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "ManufacturerId", "ManufacturerName", product.ManufacturerId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductName,ProductPrice,ReleaseDate,ProductDescription,UnitsInStock,ConsoleTypeId,ManufacturerId,CategoryId,GenreId,IsDiscontinued,ProductImage,ImageFile")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                #region File Upload - EDIT w/Image Utility
                // retain old image file name so we can reuse if needed, or delete if a new file is uploaded
                string oldImageName = product.ProductImage;

                //Check if the user uploaded a file
                if (product.ImageFile != null)
                {
                    // check the file extension
                    string ext = Path.GetExtension(product.ImageFile.FileName);

                    // list of valid extensions
                    string[] validExts = { ".jpg", ".jpeg", ".png", ".gif" };

                    // check the file extension against the valid extensions && check file size
                    if (validExts.Contains(ext.ToLower()) && product.ImageFile.Length < 4_194_303)
                    {
                        // generate a unique filename
                        product.ProductImage = Guid.NewGuid() + ext;

                        // define our filepath to save the image
                        string fullPath = _webHostEnvironment.WebRootPath + "/images/";

                        // Delete the old image
                        if (oldImageName != "noimage.png" && oldImageName != null)
                        {
                            ImageUtility.Delete(fullPath, oldImageName);
                        }

                        //Save new image to webroot
                        using (var memoryStream = new MemoryStream())
                        {
                            await product.ImageFile.CopyToAsync(memoryStream);
                            using (var img = Image.FromStream(memoryStream))
                            {
                                int maxImageSize = 500;
                                int maxThumbSize = 100;
                                ImageUtility.ResizeImage(fullPath, product.ProductImage, img, maxImageSize, maxThumbSize);
                            }
                        }
                    }
                }
                #endregion
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", product.CategoryId);
            ViewData["ConsoleTypeId"] = new SelectList(_context.ConsoleTypes, "ConsoleTypeId", "ConsoleName", product.ConsoleTypeId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "GenreId", "GenreName", product.GenreId);
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "ManufacturerId", "ManufacturerName", product.ManufacturerId);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.ConsoleType)
                .Include(p => p.Genre)
                .Include(p => p.Manufacturer)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'RetroGamesContext.Products'  is null.");
            }
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
          return (_context.Products?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }

        [AllowAnonymous]
        public async Task<IActionResult> ListProducts()
        {
            if (User.IsInRole("Admin"))
            {
                var products = _context.Products.Include(p => p.Category).Include(p => p.Manufacturer).Include(p => p.OrderProducts);
                return View(await products.ToListAsync());
            }
            else
            {
                //filterting products to those that aren't discontinued.
                var products = _context.Products.Where(x => x.IsDiscontinued == false && x.UnitsInStock > 0)
                    //allows the Categories and Suppliers to show up
                    .Include(p => p.Category).Include(p => p.Manufacturer).Include(p => p.OrderProducts);
                return View(await products.ToListAsync());
            }
        }
    }
}
