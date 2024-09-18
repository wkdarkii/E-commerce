using eticaret.Data;
using Microsoft.AspNetCore.Mvc;

namespace eticaret.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class KullaniciSatisController : Controller
    {
        private readonly AppDbContext _context;

        public KullaniciSatisController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var satislar = _context.KullanicininSattiklari.ToList();

            // View'e model olarak verileri gönderiyoruz
            return View(satislar);
        }
    }
}
