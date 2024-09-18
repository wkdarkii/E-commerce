using eticaret.Areas.Admin.Models;
using eticaret.Data;
using eticaret.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace eticaret.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SifreYenilemeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;    

        public SifreYenilemeController(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public IActionResult SifremiUnuttum()
        {
            return View();
        }


        public IActionResult Kod_Onay()
        {
            return View();
        }
        public IActionResult YeniSifre()
        { 
              return View();   
        }



        [HttpPost]
        public IActionResult CheckEmail(string email)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            if (user != null)
            {
                // E-posta adresini session'da sakla
                HttpContext.Session.SetString("Email", email);

                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, message = "Bu e-posta adresi kayıtlı değil." });
            }

        }

        [HttpPost]
        public IActionResult ValidateSecurityQuestions(string username, DateTime dob, string phonenumber)
        {
            var user = _context.Users.FirstOrDefault(u => u.Kullanici_adi == username && u.D_Tarihi == dob && u.PhoneNumber == phonenumber);

            if (user != null)
            {
                // 8 basamaklı rastgele bir kod oluştur
                var random = new Random();
                var resetCode = random.Next(10000000, 99999999);

                // E-posta gönderimi
                var emailService = new EmailService(_config);
                var subject = "Şifre Sıfırlama Kodu";
                var body = $"Şifre sıfırlama kodunuz: {resetCode}";
                emailService.SendEmail(user.Email, subject, body);

                // 'sifirlama_kodlari' tablosuna kayıt ekle
                var resetCodeEntry = new sifirlama_kodlari
                {
                    UserId = user.id, // Kullanıcının ID'sini al
                    sifirlama_rakami = resetCode // Üretilen kodu ekle
                };
                _context.SifirlamaKodlari.Add(resetCodeEntry);
                _context.SaveChanges();

                // İstemci tarafında SweetAlert göster
                return RedirectToAction("Kod_Onay", "SifreYenileme", new { area = "Admin" });

            }
            else
            {
                return Json(new { success = false, message = "Bilgiler yanlış. Lütfen tekrar deneyin.", script = "hatali_bilgi()" });

            }
        }



        [HttpPost]
        public IActionResult OnaylaKod(string resetCode)
        {
            var email = HttpContext.Session.GetString("Email");
            if (string.IsNullOrEmpty(email))
            {
                return Json(new { success = false, message = "E-posta bilgisi bulunamadı." });
            }

            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            if (user == null)
            {
                return Json(new { success = false, message = "Kullanıcı bulunamadı." });
            }

            var resetCodeEntry = _context.SifirlamaKodlari
                .FirstOrDefault(r => r.UserId == user.id && r.sifirlama_rakami == int.Parse(resetCode));

            if (resetCodeEntry != null)
            {
                _context.SifirlamaKodlari.Remove(resetCodeEntry);
                _context.SaveChanges();

                return RedirectToAction("YeniSifre", "SifreYenileme", new { area = "Admin" });
            }
            else
            {
                return Json(new { success = false, message = "Kod doğrulama hatası." });
            }
        }
    }
}
