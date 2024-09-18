using eticaret.Data;
using eticaret.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq;

namespace eticaret.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Login(string kullaniciAdi, string sifre)
        {
            // 1. Gelen kullanıcı adını değişkene ata
            string userName = kullaniciAdi;

            // 2. Kullanıcı adını veritabanında bul
            var user = _context.Users.FirstOrDefault(u => u.Kullanici_adi == userName);

            if (user != null)
            {
                // 3. Kullanıcının giriş yapma hakkı var mı kontrol et
                if (!user.Giris_Yapabilirmi)
                {
                    // Kullanıcının giriş yapma hakkı yoksa SweetAlert ile uyarı göster
                    TempData["Giris_Sayisi_Hatasi"] = true;
                    return RedirectToAction("Login");  // Sadece Login sayfasına yönlendir
                }


                // 4. Kullanıcının şifresini doğrula (hashli şifreyle)
                bool sifreDogrula = BCrypt.Net.BCrypt.Verify(sifre, user.Sifre);

                if (sifreDogrula)
                {

                    if (user.Role == true)
                    {
                        user.Giris_Tarihi = DateTime.Now;
                        user.Giris_Sayisi = 3;

                        _context.SaveChanges();

                        // Yeni giriş kaydını veritabanına ekle
                        var girisKaydi = new giris_yapildi
                        {
                            Kullanici_adi = user.Kullanici_adi,
                            EMail = user.Email,
                            Profil_Foto = user.Profil_Foto
                        };
                        _context.Giris_Yapildi.Add(girisKaydi);
                        _context.SaveChanges();

                        // Geçici model oluştur
                        var tempUser = new TempUserModel
                        {
                            Username = user.Kullanici_adi,
                            Email = user.Email
                        };

                        // TempData ile verileri saklayın
                        TempData["TempUser"] = JsonConvert.SerializeObject(tempUser);

                        TempData["MainLoginOnay"] = true;

                        // Kullanıcı doğrulama işlemleri başarılıysa:
                        // Önce mevcut session değerlerini temizle
                        HttpContext.Session.Remove("Kullanici_adi");
                        HttpContext.Session.Remove("Email");

                        // Yeni session değerlerini ayarla
                        HttpContext.Session.SetString("Kullanici_adi", user.Kullanici_adi);
                        HttpContext.Session.SetString("Email", user.Email);


                        return RedirectToAction("Panel", "Account", new { area = "Admin" });
                    }

                    else
                    {
                        user.Giris_Tarihi = DateTime.Now;
                        user.Giris_Sayisi = 3;

                        _context.SaveChanges();

                        // Yeni giriş kaydını veritabanına ekle
                        var girisKaydi = new giris_yapildi
                        {
                            Kullanici_adi = user.Kullanici_adi,
                            EMail = user.Email,
                            Profil_Foto = user.Profil_Foto
                        };
                        _context.Giris_Yapildi.Add(girisKaydi);
                        _context.SaveChanges();

                        // Geçici model oluştur
                        var tempUser = new TempUserModel
                        {
                            Username = user.Kullanici_adi,
                            Email = user.Email
                        };

                        // TempData ile verileri saklayın
                        TempData["TempUser"] = JsonConvert.SerializeObject(tempUser);

                        TempData["MainLoginOnay"] = true;

                        // Kullanıcı doğrulama işlemleri başarılıysa:
                        // Önce mevcut session değerlerini temizle
                        HttpContext.Session.Remove("Kullanici_adi");
                        HttpContext.Session.Remove("Email");

                        // Yeni session değerlerini ayarla
                        HttpContext.Session.SetString("Kullanici_adi", user.Kullanici_adi);
                        HttpContext.Session.SetString("Email", user.Email);

                        // Yönlendirmeyi gerçekleştirin
                        return RedirectToAction("Login", "Account");
                    }




                    // 5. Giriş başarılı, giriş tarihini güncelle

                }



                else
                {
                    // 6. Şifre yanlışsa, giriş sayısını azalt
                    user.Giris_Sayisi -= 1;

                    if (user.Giris_Sayisi == 1)
                    {
                        TempData["Son_Giris_Hakki"] = true;
                        _context.SaveChanges();
                        return RedirectToAction("Login");
                    }

                    // 7. Eğer giriş sayısı 0'a ulaşırsa, giriş yapma hakkını kapat
                    if (user.Giris_Sayisi <= 0)
                    {
                        user.Giris_Yapabilirmi = false;
                        TempData["Giris_Sayisi_Hatasi"] = true;
                        _context.SaveChanges();
                        return RedirectToAction("Login");  // Sadece Login sayfasına yönlendir
                    }



                    _context.SaveChanges();

                    TempData["LoginError"] = "Kullanıcı adı veya şifre yanlış. Kalan deneme hakkı: " + user.Giris_Sayisi;
                    return RedirectToAction("Login", new { success = false, girisReddedildi = true });
                }
            }

            // 8. Kullanıcı bulunamazsa giriş başarısız
            TempData["LoginError"] = "Kullanıcı bulunamadı.";
            return RedirectToAction("Login", new { success = false, girisReddedildi = true });
        }




        public IActionResult Login(bool? success, bool? girisReddedildi)
        {
            ViewBag.Success = success.HasValue && success.Value;
            ViewBag.GirisReddedildi = girisReddedildi.HasValue && girisReddedildi.Value;

            // TempData["Giris_Sayisi_Hatasi"] kontrolü
            if (TempData["Giris_Sayisi_Hatasi"] != null && (bool)TempData["Giris_Sayisi_Hatasi"])
            {
                ViewBag.GirisSayisiHatasi = true;
            }
            return View();
        }





        public IActionResult Panel()
        {
            // Session'dan kullanıcı adı ve email al
            string kullaniciAdi = HttpContext.Session.GetString("Kullanici_adi");
            string email = HttpContext.Session.GetString("Email");

            // Eğer session'da kullanıcı adı veya email yoksa false dön
            if (string.IsNullOrEmpty(kullaniciAdi) || string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Login", "Account");
            }

            // Users tablosunda kullanıcı adı, email ve Role == true olan bir veri var mı kontrol et
            var user = _context.Users.FirstOrDefault(u => u.Kullanici_adi == kullaniciAdi && u.Email == email && u.Role == true);

            // Eğer kullanıcı bulunamadıysa false dön
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // Seller tablosundaki tüm verileri çekiyoruz
            var sellers = _context.Sellers.Include(s => s.User).ToList();

            // Verileri view'a gönderiyoruz
            return View(sellers);
        }
        [HttpPost]
        public IActionResult UpdateStatus(int id, string newStatus)
        {
            // Geçerli onay aşaması değerleri, tam olarak ENUM'da olduğu gibi
            var allowedStatuses = new List<string> { "Onaylandı", "Onaylanmadı", "Bekleniyor", "Askıya Alındı" };

            // Gelen newStatus değerini ENUM ile karşılaştırıyoruz
            // Fazladan boşlukları temizlemek ve büyük/küçük harfleri düzeltmek için ToLower ve Trim kullanıyoruz.
            newStatus = newStatus?.Trim();

            // Eğer gelen newStatus geçerli değerlerden biriyse güncellemeye devam ediyoruz
            if (allowedStatuses.Contains(newStatus))
            {
                var seller = _context.Sellers.FirstOrDefault(s => s.Id == id);
                if (seller != null)
                {
                    // Onay_Asaması sütununu güncelliyoruz
                    seller.Onay_Asaması = newStatus;
                    try
                    {
                        _context.SaveChanges(); // Değişiklikleri veritabanına kaydediyoruz
                    }
                    catch (Exception ex)
                    {
                        // SaveChanges hatası alındığında özel bir hata mesajı döndürüyoruz
                        return BadRequest("Veritabanı hatası: " + ex.Message);
                    }
                }
                else
                {
                    return NotFound("Satıcı bulunamadı.");
                }
            }
            else
            {
                return BadRequest("Geçersiz onay aşaması.");
            }

            return RedirectToAction("Panel");
        }











        public IActionResult Register()
        {
            return View();
        }
    }
}
