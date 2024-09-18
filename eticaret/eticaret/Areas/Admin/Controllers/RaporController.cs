using eticaret.Data;
using Microsoft.AspNetCore.Mvc;

namespace eticaret.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RaporController : Controller
    {

        private readonly AppDbContext _context;

        public RaporController(AppDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public IActionResult Index(string UrunId, string SaticiId, string SatinAlanId, string UrunAdi, decimal UrunFiyat, string UrunAciklama, string UrunResim)
        {
            // Burada veriler işlenebilir ya da rapor oluşturulabilir
            // Örnek: Bu veriler başka bir sayfada gösterilebilir ya da işlenebilir

            // Verileri ViewBag veya Model üzerinden görünüme aktarabilirsiniz
            ViewBag.UrunId = UrunId;
            ViewBag.SaticiId = SaticiId;
            ViewBag.SatinAlanId = SatinAlanId;
            ViewBag.UrunAdi = UrunAdi;
            ViewBag.UrunFiyat = UrunFiyat;
            ViewBag.UrunAciklama = UrunAciklama;
            ViewBag.UrunResim = UrunResim;

            return View();
        }
    }
}
