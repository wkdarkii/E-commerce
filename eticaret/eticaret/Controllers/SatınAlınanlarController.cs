using eticaret.Data;
using eticaret.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace eticaret.Controllers
{
    
    public class SatınAlınanlarController : Controller
    {
        private readonly AppDbContext _context;

        public SatınAlınanlarController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Purchase(int productId)
        {
            // Kullanıcı oturumu bilgilerini al
            string kullaniciAdi = HttpContext.Session.GetString("Kullanici_adi");
            string email = HttpContext.Session.GetString("Email");

            // Eğer oturum bilgileri yoksa yetkisiz erişim
            if (string.IsNullOrEmpty(kullaniciAdi) || string.IsNullOrEmpty(email))
            {
                return Unauthorized(); // Yetkisiz erişim
            }

            // Veritabanından kullanıcı ID'sini bul
            var user = _context.Users
                .FirstOrDefault(u => u.Kullanici_adi == kullaniciAdi && u.Email == email);

            if (user == null)
            {
                return Unauthorized(); // Kullanıcı bulunamadı
            }

            // Satın alınanlar tablosuna kaydet
            var purchase = new Satin_Alinanlar
            {
                ProductId = productId,
                UserId = user.id, // Kullanıcı ID'sini veritabanından aldık
                PurchaseDate = DateTime.Now
            };

            _context.Satin_Alinanlar.Add(purchase);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }


    }
}
