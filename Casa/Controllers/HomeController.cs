using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Casa.Models;
using System.Globalization;

namespace Casa.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        private readonly RegisterContext _context;

        public HomeController(RegisterContext context)
        {
            _context = context;
        }



        public IActionResult Index()
        {
            ViewBag.Categories = _context.Category.ToList();
            ViewBag.CategoryCount = _context.Category.ToList().Count;
            ViewBag.Products = _context.Product.ToList();
            ViewBag.ProductsCount = _context.Product.ToList().Count;
            return View();
        }

        public ActionResult GetProduct(int id)
        {
            Product product = _context.Product.Where(x => x.Id == id).FirstOrDefault();
            return Json(product);
        }

        public ActionResult GetProductList(int CategoryId)
        {
            if (CategoryId == 0)
            {
                List<Product> ProductList = _context.Product.ToList();
                return Json(ProductList);
            }
            else
            {
                List<Product> ProductList = _context.Product.Where(x => x.CategoryId == CategoryId).ToList();
                return Json(ProductList);
            }

        }

        public async Task<int> CreateCheck()
        {
            Check check = new Check();
            check.Date = DateTime.Now;            
            _context.Add(check);
            await _context.SaveChangesAsync();
            return check.Id;
        }

        public async Task<IActionResult> AddProductToCheck(int checkId, int productId, string quantity)
        {
            Product product = _context.Product.Where(x => x.Id == productId).FirstOrDefault();
            CheckProduct checkProduct = new CheckProduct();
            checkProduct.CheckId = checkId;
            checkProduct.ProductId = productId;
            if (product.Piece)
            {
                checkProduct.Quantity = int.Parse(quantity);
            }
            else
            {
                checkProduct.Quantity = double.Parse(quantity, CultureInfo.InvariantCulture);
            }
            Console.WriteLine(product.Piece);
            _context.Add(checkProduct);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
