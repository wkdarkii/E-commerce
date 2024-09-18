using eticaret.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eticaret.Controllers
{
    public class ProductController : Controller
    {


        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Products_Grid(int categoryId)
        {
            var products = await _context.Products
                .Where(p => p.category_id == categoryId)
                .ToListAsync();

            return View(products);
        }

        [HttpGet]
        public JsonResult GetProductsByCategory(int categoryId)
        {
            var products = _context.Products
                .Where(p => p.category_id == categoryId)
                .Select(p => new
                {
                    p.id,
                    p.name,
                    p.price,
                    p.description,
                    p.img,
                    p.discount,
                    p.label,
                    
                })
                .ToList();

            return Json(products);
        }

        public IActionResult product(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }


    }
}
