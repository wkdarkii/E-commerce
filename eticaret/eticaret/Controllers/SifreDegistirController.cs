using eticaret.Areas.Admin.Models;
using eticaret.Data;
using Microsoft.AspNetCore.Mvc;

namespace eticaret.Controllers
{
    public class SifreDegistirController : Controller
    {
        private readonly AppDbContext _context;

        public SifreDegistirController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Degistir(string currentPassword, string newPassword, string confirmPassword)
        {
            // Session'dan giriş yapan kullanıcının bilgilerini al
            string kullaniciAdi = HttpContext.Session.GetString("Kullanici_adi");
            string email = HttpContext.Session.GetString("Email");

            // Kullanıcının User tablosundaki verisini al
            var user = _context.Users.FirstOrDefault(u => u.Kullanici_adi == kullaniciAdi && u.Email == email);
            if (user == null)
            {
                return BadRequest("Kullanıcı bulunamadı.");
            }

            // Mevcut şifreyi hashleyip doğrula
            bool isPasswordValid = PasswordHelper.VerifyPassword(currentPassword, user.Sifre);
            if (!isPasswordValid)
            {
                return BadRequest("Mevcut şifre yanlış.");
            }

            // Yeni şifre ve onay şifresinin eşleşip eşleşmediğini kontrol et
            if (newPassword != confirmPassword)
            {
                return BadRequest("Yeni şifreler eşleşmiyor.");
            }

            // Yeni şifreyi hashleyip veritabanında güncelle
            user.Sifre = PasswordHelper.HashPassword(newPassword);
            _context.SaveChanges();

            // Session'dan kullanıcı bilgilerini temizle
            HttpContext.Session.Remove("Kullanici_adi");
            HttpContext.Session.Remove("Email");

            // Kullanıcıyı login sayfasına yönlendir ve SweetAlert tetiklemek için query string ekle
            return RedirectToAction("Login", "Account", new { area = "Admin", Giris_Yapabilmesi_icin_Onayla = "true" });

        }
    }
}
