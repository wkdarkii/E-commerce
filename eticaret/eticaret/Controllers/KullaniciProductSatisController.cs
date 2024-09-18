using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using eticaret.Models;
using System.Linq;
using Microsoft.AspNetCore.Http;
using eticaret.Data;
using Microsoft.EntityFrameworkCore;

namespace eticaret.Controllers
{
    public class KullaniciProductSatisController : Controller
    {
        private readonly AppDbContext _context;

        public KullaniciProductSatisController(AppDbContext context)
        {
            _context = context;
        }

        // Ürün detaylarını gösterir ve kullanıcı oturumunu kontrol eder
        public IActionResult ProductDetails(int id)
        {
            // Kullanıcı oturumunu kontrol et
            string kullaniciAdi = HttpContext.Session.GetString("Kullanici_adi");
            string email = HttpContext.Session.GetString("Email");

            if (string.IsNullOrEmpty(kullaniciAdi) || string.IsNullOrEmpty(email))
            {
              return RedirectToAction("Login", "Account", new { area = "Admin" });

            }

            // Ürün detaylarını ve satıcı bilgilerini al
            var product = _context.Sellers
                .Where(s => s.Id == id)
                .Include(s => s.User)
                .FirstOrDefault();

            if (product == null)
            {
                return NotFound();
            }

            var viewModel = new ProductDetailsViewModel
            {
                Product = product,
                Seller = product.User
            };

            return View(viewModel);
        }


        [HttpGet]
        public IActionResult SatinAl(int productId)
        {
            // Oturumdaki kullanıcının bilgilerini al
            string kullaniciAdi = HttpContext.Session.GetString("Kullanici_adi");
            string email = HttpContext.Session.GetString("Email");

            // Kullanıcı oturum bilgileri boşsa login sayfasına yönlendir
            if (string.IsNullOrEmpty(kullaniciAdi) || string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Login", "Account", new { area = "Admin" });
            }

            // Giriş yapan kullanıcıyı users tablosundan bul
            var kullanici = _context.Users.FirstOrDefault(u => u.Kullanici_adi == kullaniciAdi && u.Email == email);
            if (kullanici == null)
            {
                return RedirectToAction("Login", "Account", new { area = "Admin" });
            }

            // Kullanıcının kart profiline sahip olup olmadığını kontrol et
            bool kartProfiliVarMi = _context.UserCards.Any(uc => uc.UserId == kullanici.id);
            if (!kartProfiliVarMi)
            {
                // Kart profili yoksa, kart profili oluşturma sayfasına yönlendir
                return RedirectToAction("KartProfili", "Kullaniciarayuz");
            }

            // Ürün ve satıcı bilgilerini al
            var product = _context.Sellers
                .Include(s => s.User)  // Satıcı bilgilerini getiriyoruz
                .FirstOrDefault(s => s.Id == productId);

            if (product == null)
            {
                return NotFound();
            }

            // Satıcı bilgilerini al
            var seller = _context.Users.FirstOrDefault(u => u.id == product.UserId);
            if (seller == null)
            {
                return NotFound();
            }

            // Satın alma işlemini Kullanicininsattig tablosuna kaydet
            var yeniKayit = new Kullanicininsattig
            {
                UrunId = product.Id,
                UrunAdi = product.UrunAdi,
                UrunAciklama = product.UrunAciklama,
                UrunFiyat = product.UrunFiyat.HasValue ? (decimal)product.UrunFiyat.Value : 0m,
                UrunResim = product.UrunResim,
                SaticiId = seller.id,   // Satıcı bilgisi User tablosuyla ilişkili
                SatinAlanId = kullanici.id  // Satın alan kullanıcı bilgisi
            };

            _context.KullanicininSattiklari.Add(yeniKayit);
            _context.SaveChanges();

            // Satın alınan ürünü sellers tablosundan sil
            _context.Sellers.Remove(product);
            _context.SaveChanges();

            // Başarılı işlem sonrası sipariş onay sayfasına yönlendir
            return RedirectToAction("SiparisOnay", "Kullanıcıarayüz");
        }

        [HttpPost]
        public IActionResult UrunBilgileriniAl(int productId, string urunAdi, decimal urunFiyat, string urunAciklama)
        {
            // Ürün bilgileri burada işlenebilir

            // Ürün bilgilerine göre bir yönlendirme yap
            // Örnek olarak 'Satinal' metoduna yönlendirme:
            return RedirectToAction("SatinAl", new { productId = productId });
        }

    }
}
