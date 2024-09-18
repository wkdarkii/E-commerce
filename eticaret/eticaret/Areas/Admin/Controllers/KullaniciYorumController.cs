using eticaret.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eticaret.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class KullaniciYorumController : Controller
    {
            private readonly AppDbContext _context;
            public KullaniciYorumController(AppDbContext context)
            {
                _context = context;
            }
        public async Task<IActionResult> Index()
        {



            // Veritabanından yorumları çek
            var yorumlar = await _context.UrunYorumlari
                .Include(u => u.User)   // Kullanıcı bilgilerini dahil et
                .Include(u => u.Product) // Ürün bilgilerini dahil et
                .ToListAsync();

            return View(yorumlar);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var yorum = await _context.UrunYorumlari.FindAsync(id);
            if (yorum != null)
            {
                _context.UrunYorumlari.Remove(yorum);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
