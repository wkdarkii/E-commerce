using Microsoft.AspNetCore.Mvc;
using eticaret.Models;
using System.Linq;
using eticaret.Data;

namespace eticaret.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SatisBilgiController : Controller
    {
        private readonly AppDbContext _context;

        public SatisBilgiController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Satis()
        {
            var userToplamParalar = _context.ToplamParalar
                .GroupBy(tp => tp.UserId)
                .Select(group => new UserToplamParaViewModel
                {
                    UserId = group.Key,
                    KullaniciAdi = _context.Users.Where(u => u.id == group.Key).Select(u => u.Kullanici_adi).FirstOrDefault(),
                    ToplamMiktar = group.Sum(tp => tp.Miktar),
                    SonAlmaZamani = group.Max(tp => tp.alma_zamani) // Son alma zamanını al
                })
                .ToList();

            return View(userToplamParalar);
        }
    }
}
