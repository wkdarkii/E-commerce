using eticaret.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eticaret.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SiparislerController : Controller
    {

        private readonly AppDbContext _context; // DbContext

        public SiparislerController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Siparis()
        {
            // Siparişleri ve ilişkili ürün ve kullanıcı bilgilerini getirir
            var siparisler = _context.Siparisler
                .Include(s => s.Product)
                .Include(s => s.User)
                .ToList();

            return View(siparisler);
        }



        [HttpPost]
        public async Task<IActionResult> UpdateOrderStatus(int id, string newStatus)
        {
            // İlgili siparişi bul
            var siparis = await _context.Siparisler.FindAsync(id);

            if (siparis == null)
            {
                return NotFound();
            }

            // Sipariş durumunu güncelle
            siparis.SiparisDurumu = newStatus;

            // Değişiklikleri kaydet
            _context.Update(siparis);
            await _context.SaveChangesAsync();

            // Geriye Index action'una yönlendirme yap
            return RedirectToAction("Siparis", "Siparisler", new {Areas="Admin"});
        }
    }
}
