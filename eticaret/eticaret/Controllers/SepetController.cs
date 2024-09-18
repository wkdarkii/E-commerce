using eticaret.Data;
using eticaret.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace eticaret.Controllers
{
    public class SepetController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SepetController(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        // Ürünü sepete ekler
        [HttpPost]
        public IActionResult SepeteEkle(int productId)
        {
            // Kullanıcı oturumu bilgilerini al
            string kullaniciAdi = HttpContext.Session.GetString("Kullanici_adi");
            string email = HttpContext.Session.GetString("Email");

            // Eğer oturum bilgileri yoksa yetkisiz erişim
            if (string.IsNullOrEmpty(kullaniciAdi) || string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Login", "Account", new { area = "Admin" });
            }

            // Veritabanından kullanıcı ID'sini bul
            var user = _context.Users
                .FirstOrDefault(u => u.Kullanici_adi == kullaniciAdi && u.Email == email);

            if (user == null)
            {
                return Unauthorized(); // Kullanıcı bulunamadı
            }


            var sepeteEklenenUrun = new SepeteEklenenUrunler
            {
                product_id = productId,
                user_id = user.id, 
                satın_alma_zamani = DateTime.Now
            };

            _context.SepeteEklenenUrun.Add(sepeteEklenenUrun);
            _context.SaveChanges();

            return RedirectToAction("Index", "Sepet");
        }

        // Sepeti görüntüler
        [HttpGet]
        public IActionResult Index()
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

            // Sepetteki ürünleri al
            var sepettekiUrunler = _context.SepeteEklenenUrun
                                    .Include(s => s.Product)
                                    .Where(s => s.user_id == user.id)
                                    .ToList();

            return View(sepettekiUrunler);
        }


        // Sepetten ürünü siler
        [HttpPost]
        public IActionResult UrunuSil(int id)
        {
            var urun = _context.SepeteEklenenUrun.Find(id);
            if (urun != null)
            {
                _context.SepeteEklenenUrun.Remove(urun);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }




        [HttpPost]
        public IActionResult Checkout()
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

            // Sepetteki ürünleri al
            var sepettekiUrunler = _context.SepeteEklenenUrun
                .Where(s => s.user_id == user.id)
                .ToList();

            // Sepetteki ürünleri siparişler tablosuna ekle
            foreach (var urun in sepettekiUrunler)
            {
                var siparis = new Siparis
                {
                    UserId = user.id,
                    ProductId = urun.product_id,
                    SiparisTarihi = DateTime.Now
                };

                _context.Siparisler.Add(siparis);
            }

            // Sepet ürünlerini temizle
            _context.SepeteEklenenUrun.RemoveRange(sepettekiUrunler);

            // Veritabanı işlemlerini kaydet
            _context.SaveChanges();

            return RedirectToAction("Siparislerim", "Sepet"); // Siparişler ekranına yönlendir
        }


        public IActionResult Siparislerim()
        {
            // Kullanıcı oturumu bilgilerini al
            string kullaniciAdi = HttpContext.Session.GetString("Kullanici_adi");
            string email = HttpContext.Session.GetString("Email");

            if (string.IsNullOrEmpty(kullaniciAdi) || string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Login", "Account", new { area = "Admin", Giris_Yapabilmesi_icin_Onayla = "true" });
            }

            // Veritabanından kullanıcı ID'sini bul
            var user = _context.Users
                .FirstOrDefault(u => u.Kullanici_adi == kullaniciAdi && u.Email == email);

            if (user == null)
            {
                return Unauthorized(); // Kullanıcı bulunamadı
            }

            // Kullanıcının siparişlerini al
            var siparisler = _context.Siparisler
                .Include(s => s.Product)
                .Where(s => s.UserId == user.id)
                .ToList();


            // Siparişleri güncelle
            foreach (var siparis in siparisler)
            {
                if (siparis.SiparisDurumu == null) // Eğer SiparisDurumu null ise
                {
                    siparis.SiparisDurumu = "Sipariş Onaylandı";
                }
                if (siparis.iptalet == null) // Eğer iptalet null ise
                {
                    siparis.iptalet = false;
                }
            }

            // Veritabanında güncellemeleri kaydet
            _context.SaveChanges();


            return View(siparisler);
            
        }


    }
}
