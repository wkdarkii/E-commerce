using eticaret.Data;
using Microsoft.AspNetCore.Mvc;

namespace eticaret.Controllers
{
    public class KullaniciSatislariController : Controller
    {
        private readonly AppDbContext _context;

        public KullaniciSatislariController(AppDbContext context)
        {
            _context = context;
        }

        // Index action with sellerId parameter
        public IActionResult Index(int? id)
        {
            var products = _context.Sellers
                .Where(s => s.Onay_Asaması == "Onaylandı")
                .ToList();

            // Eğer bir ID geldiyse, ViewBag'e atıyoruz
            if (id.HasValue)
            {
                ViewBag.SelectedProductId = id.Value;
            }
            else
            {
                ViewBag.SelectedProductId = null;  // ID gelmediyse null yapıyoruz
            }

            return View(products);
        }





        //Action to fetch a specific product by id
        //public IActionResult OzelUrun(int id)
        //{
        //    var product = _context.Sellers
        //        .FirstOrDefault(s => s.Id == id);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }

        //    // id'yi ViewBag'e atıyoruz
        //    ViewBag.SelectedProductId = id;

        //    return RedirectToAction("Index", new { selectedProductId = id });
        //}


    }
}
