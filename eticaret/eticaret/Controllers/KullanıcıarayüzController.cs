using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;
using eticaret.Data;
using eticaret.Models;
using Microsoft.EntityFrameworkCore;

namespace eticaret.Controllers
{
    public class KullanıcıarayüzController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public KullanıcıarayüzController(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult CikisYap()
        {
            // Oturumdan kullanıcı adı ve e-posta bilgilerini al
            string kullaniciAdi = HttpContext.Session.GetString("Kullanici_adi");
            string email = HttpContext.Session.GetString("Email");

            // Oturumdan kullanıcı bilgilerini temizle
            HttpContext.Session.Remove("Kullanici_adi");
            HttpContext.Session.Remove("Email");

            // Giriş yapabilmesi için onaylayıcı parametre ile Login sayfasına yönlendir
            return RedirectToAction("Login", "Account", new { area = "Admin", Giris_Yapabilmesi_icin_Onayla = "true" });
        }
        public IActionResult Profili()
        {
            // Kullanıcı oturumu bilgilerini al
            string kullaniciAdi = HttpContext.Session.GetString("Kullanici_adi");
            string email = HttpContext.Session.GetString("Email");

            if (string.IsNullOrEmpty(kullaniciAdi) || string.IsNullOrEmpty(email))
            {


                return RedirectToAction("Login", "Account", new { area = "Admin", Giris_Yapabilmesi_icin_Onayla = "true" });


            }

            // Kullanıcı bilgilerini veritabanından al
            var user = _context.Users
                .FirstOrDefault(u => u.Kullanici_adi == kullaniciAdi && u.Email == email);

            if (user == null)
            {
                return NotFound(); // Kullanıcı bulunamadı
            }

            // Kullanıcı bilgilerini view'a gönder
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> Profili_Guncelle(User model, IFormFile newImage)
        {
            // Kullanıcı oturumu bilgilerini al
            string kullaniciAdi = HttpContext.Session.GetString("Kullanici_adi");

            if (string.IsNullOrEmpty(kullaniciAdi))
            {
                return Unauthorized(); // Yetkisiz erişim
            }

            // Kullanıcıyı veritabanından bul
            var user = _context.Users.FirstOrDefault(u => u.Kullanici_adi == kullaniciAdi);

            if (user == null)
            {
                return NotFound(); // Kullanıcı bulunamadı
            }

            // Güncelleme için `gender` verisini kontrol edin
            if (Request.Form["gender"] == "male")
            {
                user.Cinsiyet = true;  // Erkek ise true yap
            }
            else if (Request.Form["gender"] == "female")
            {
                user.Cinsiyet = false;  // Kadın ise false yap
            }

            // Kullanıcı adı veya e-posta değişti mi?
            bool kullaniciAdiDegisti = user.Kullanici_adi != model.Kullanici_adi;
            bool emailDegisti = user.Email != model.Email;

            // Diğer kullanıcı bilgilerini güncelle
            user.Kullanici_adi = model.Kullanici_adi;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            user.Adres = model.Adres;
            user.D_Tarihi = model.D_Tarihi;

            // Yeni profil fotoğrafı varsa, işle
            bool fotoDegisti = false;
            if (newImage != null && newImage.Length > 0)
            {
                // Eski fotoğrafı sil
                if (!string.IsNullOrEmpty(user.Profil_Foto))
                {
                    string oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Kullanici_img", user.Profil_Foto);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                // Yeni fotoğrafı yükle
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(newImage.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Kullanici_img", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await newImage.CopyToAsync(stream);
                }

                user.Profil_Foto = fileName;
                fotoDegisti = true;
            }

            _context.Update(user);
            await _context.SaveChangesAsync();

        
            if (fotoDegisti)
            {
                TempData["ProfilFotoGuncellendi"] = true;
            }


            // Eğer kullanıcı adı veya e-posta güncellendiyse, kullanıcıyı tekrar giriş yapması için yönlendir
            if (kullaniciAdiDegisti || emailDegisti)
            {
                return RedirectToAction("Login", "Account", new { area = "Admin" });
            }

            return RedirectToAction("Profili"); // Güncellemeyi tamamladıktan sonra aynı sayfaya yönlendir
        }



        public IActionResult sifreDegistir()
        {
            return View();
        }

        public IActionResult Rapor(int id)
        {
            // Ürünü ID'ye göre veritabanından al
            var urun = _context.KullanicininSattiklari
                .Include(u => u.Satici)  // Eğer Satıcı bilgilerini de almak istiyorsanız
                .FirstOrDefault(u => u.Id == id);

            if (urun == null)
            {
                return NotFound();
            }

            return View(urun);
        }

        public IActionResult SiparisOnay()
        {
            // Giriş yapan kişinin bilgilerini al
            string kullaniciAdi = HttpContext.Session.GetString("Kullanici_adi");
            string email = HttpContext.Session.GetString("Email");

            // Eğer oturum bilgileri boşsa, login sayfasına yönlendir
            if (string.IsNullOrEmpty(kullaniciAdi) || string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Login", "Account");
            }

            // Kullanıcıyı users tablosundan bul
            var kullanici = _context.Users.FirstOrDefault(u => u.Kullanici_adi == kullaniciAdi && u.Email == email);
            if (kullanici == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // Kullanıcının sattığı ürünleri al
            var satilanUrunler = _context.KullanicininSattiklari
                .Where(k => k.SatinAlanId == kullanici.id)
                .ToList();

            return View(satilanUrunler);
        }
        public IActionResult kartProfili()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UpdateCart(string cardNumber, string cardHolder, int expiryMonth, int expiryYear, int cvv)
        {
            // Giriş yapan kullanıcının bilgilerini al
            string kullaniciAdi = HttpContext.Session.GetString("Kullanici_adi");
            string email = HttpContext.Session.GetString("Email");

            // Kullanıcının ID'sini bul
            var user = _context.Users.FirstOrDefault(u => u.Kullanici_adi == kullaniciAdi && u.Email == email);
            if (user == null)
            {
                return NotFound("Kullanıcı bulunamadı.");
            }

            // Null, boş veya 0 kontrolü (expiryMonth, expiryYear, cvv için int tipinde 0 kontrolü yapılıyor)
            if (string.IsNullOrWhiteSpace(cardNumber) || string.IsNullOrWhiteSpace(cardHolder) ||
                expiryMonth == 0 || expiryYear == 0 || cvv == 0)
            {
                TempData["Error"] = "Lütfen tüm alanları doldurun.";
                return RedirectToAction("kartProfili"); // Aynı sayfaya geri yönlendirir.
            }

            // Kredi kartı format kontrolü
            if (cardNumber.Replace(" ", "").Length != 16) // Kart numarası 16 haneli olmalı
            {
                TempData["Error"] = "Geçersiz kart numarası, 16 haneli olmalıdır.";
                return RedirectToAction("kartProfili");
            }

            // ExpiryMonth 1-12 arasında olmalı
            if (expiryMonth < 1 || expiryMonth > 12)
            {
                TempData["Error"] = "Geçersiz ay formatı.";
                return RedirectToAction("kartProfili");
            }

            // ExpiryYear 4 haneli olmalı, örn: 2024
            if (expiryYear.ToString().Length != 2)
            {
                TempData["Error"] = "Geçersiz yıl formatı, 4 haneli olmalıdır.";
                return RedirectToAction("kartProfili");
            }
            

            // CVV 3 haneli olmalı
            if (cvv.ToString().Length != 3)
            {
                TempData["Error"] = "Geçersiz CVV, 3 haneli olmalıdır.";
                return RedirectToAction("kartProfili");
            }

            // Yukarıdaki tüm kontroller geçildiyse, veritabanına kaydet
            usercards newUserCard = new usercards
            {
                UserId = user.id,
                cartnumber = long.Parse(cardNumber.Replace(" ", "")), // int yerine long kullanılıyor
                cartholder = cardHolder,
                ExpiryMonth = expiryMonth,
                ExpiryYear = expiryYear,
                CVV = cvv
            };

            _context.UserCards.Add(newUserCard);
            _context.SaveChanges();

            TempData["Success"] = "Kart başarıyla güncellendi.";
            return RedirectToAction("Profili", "Kullanıcıarayüz"); // Kaydettikten sonra yönlendirme yapılır
        }
    }
}
