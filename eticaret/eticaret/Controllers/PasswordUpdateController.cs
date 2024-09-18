using eticaret.Areas.Admin.Models;
using eticaret.Data;
using Microsoft.AspNetCore.Mvc;

namespace eticaret.Controllers
{
    public class PasswordUpdateController : Controller
    {
        private readonly AppDbContext _context;

        public PasswordUpdateController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult UpdatePassword(string newPassword)
        {
            var email = HttpContext.Session.GetString("Email");

            if (email == null)
            {
                return Json(new { success = false, message = "Oturum verileri bulunamadı." });
            }

            var user = _context.Users.SingleOrDefault(u => u.Email == email);

            if (user == null)
            {
                return Json(new { success = false, message = "Kullanıcı bulunamadı." });
            }

            // Yeni şifreyi hashleyip güncelle
            user.Sifre = PasswordHelper.HashPassword(newPassword);

            _context.SaveChanges();

            // Başarılı işlemden sonra JSON formatında yönlendirme bilgisi gönder
            return Json(new { success = true, redirectUrl = Url.Action("Login", "Account", new { area = "Admin", Giris_Yapabilmesi_icin_Onayla = "true" }) });
        }

    }
}
