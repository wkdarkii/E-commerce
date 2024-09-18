using eticaret.Data;
using eticaret.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eticaret.Controllers
{
    public class IlanlarController : Controller
    {
        private readonly AppDbContext _context;

        public IlanlarController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Kullanıcı oturumu bilgilerini al
            string kullaniciAdi = HttpContext.Session.GetString("Kullanici_adi");
            string email = HttpContext.Session.GetString("Email");

            // Eğer kullanıcı adı veya email session'da yoksa, hata mesajı döndür
            if (string.IsNullOrEmpty(kullaniciAdi) || string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Login", "Account", new { area = "Admin", Giris_Yapabilmesi_icin_Onayla = "true" });

            }

            // Kullanıcı adını ve email'i kullanarak User tablosunda kullanıcıyı bul
            var user = _context.Users.FirstOrDefault(u => u.Kullanici_adi == kullaniciAdi && u.Email == email);
            if (user == null)
            {
                ModelState.AddModelError("", "Kullanıcı bulunamadı");
                return View();
            }

            int userId = user.id;

            // Kullanıcının Seller tablolarındaki ilanlarını çek
            var userSellers = _context.Sellers
                .Where(s => s.UserId == userId)
                .ToList();

            // Kullanıcının bilgilerini çek
            var userInfo = _context.Users
                .FirstOrDefault(u => u.id == userId);

            // ViewData veya ViewBag ile bilgileri view'a gönder
            ViewData["UserSellers"] = userSellers;
            ViewData["UserInfo"] = userInfo;

            return View();
        }


        // Düzenle sayfası için GET metodu
        public IActionResult Duzenle(int id)
        {
            // ID'ye göre ilgili ürünü bul
            var seller = _context.Sellers.FirstOrDefault(s => s.Id == id);

            if (seller == null)
            {
                return NotFound();
            }

            return View(seller); // Seller modelini view'a gönder
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Duzenle(Seller seller, IFormFile newImage)
        {
            if (!ModelState.IsValid)
            {
                // Kullanıcı oturum bilgilerini al
                string kullaniciAdi = HttpContext.Session.GetString("Kullanici_adi");
                string email = HttpContext.Session.GetString("Email");

                if (string.IsNullOrEmpty(kullaniciAdi) || string.IsNullOrEmpty(email))
                {
                    return Unauthorized(); // Yetkisiz erişim
                }

                // Veritabanından kullanıcı ID'sini bul
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Kullanici_adi == kullaniciAdi && u.Email == email);

                if (user == null)
                {
                    ModelState.AddModelError("", "Geçersiz kullanıcı.");
                    return View(seller);
                }

                // UserId'yi seller objesine ata
                seller.UserId = user.id;

                var existingSeller = await _context.Sellers.AsNoTracking().FirstOrDefaultAsync(s => s.Id == seller.Id);

                if (existingSeller == null)
                {
                    ModelState.AddModelError("", "Ürün bulunamadı.");
                    return View(seller);
                }

                // Eğer yeni bir resim yüklenmişse, işlemleri yap
                if (newImage != null && newImage.Length > 0)
                {
                    // Eski resmi sil
                    var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", existingSeller.UrunResim);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }

                    // Yeni resmi kaydet
                    var newImageName = Guid.NewGuid().ToString() + Path.GetExtension(newImage.FileName);
                    var newImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", newImageName);

                    using (var stream = new FileStream(newImagePath, FileMode.Create))
                    {
                        await newImage.CopyToAsync(stream);
                    }

                    seller.UrunResim = newImageName;
                }
                else
                {
                    // Yeni resim yoksa, mevcut resmi koru
                    seller.UrunResim = existingSeller.UrunResim;
                }

                _context.Update(seller);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(seller);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Sil(int id)
        {
            // Kullanıcı oturum bilgilerini al
            string kullaniciAdi = HttpContext.Session.GetString("Kullanici_adi");
            string email = HttpContext.Session.GetString("Email");

            if (string.IsNullOrEmpty(kullaniciAdi) || string.IsNullOrEmpty(email))
            {
                return Unauthorized(); // Yetkisiz erişim
            }

            // Veritabanından kullanıcı ID'sini bul
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Kullanici_adi == kullaniciAdi && u.Email == email);

            if (user == null)
            {
                return Unauthorized(); // Kullanıcı bulunamadı
            }

            // Seller kaydını bul ve sil
            var seller = await _context.Sellers
                .FirstOrDefaultAsync(s => s.Id == id && s.UserId == user.id);

            if (seller != null)
            {
                _context.Sellers.Remove(seller);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index"); // Index'e yönlendir
        }



    }
}
