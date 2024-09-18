using eticaret.Models; // Modelin bulunduğu namespace
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using eticaret.Data;
using Microsoft.EntityFrameworkCore;

namespace eticaret.Controllers
{
    public class Satici_OlusturController : Controller
    {
        private readonly AppDbContext _context;

        public Satici_OlusturController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Create(IFormCollection form, IFormFile UrunResim)
        {
            // Kullanıcı adını session'dan al
            string kullaniciAdi = HttpContext.Session.GetString("Kullanici_adi");

            // Kullanıcı adını User tablosunda arayıp ID'sini al
            var user = _context.Users.FirstOrDefault(u => u.Kullanici_adi == kullaniciAdi);
            if (user == null)
            {
                // Eğer kullanıcı bulunamazsa, bir hata mesajı döndürebilirsiniz
                ModelState.AddModelError("", "Kullanıcı bulunamadı");
                return View();
            }

            // Formdan gelen verileri alıyoruz
            var seller = new Seller
            {
                UserId = user.id, // Kullanıcı ID'sini ata
                UrunAdi = form["Product"],
                UrunAciklama = form["aciklama"],
                UrunKategori = form["Kategori"],
                Sehir = form["sehir"],
                Ilce = form["ilce"],
                UrunFiyat = (int?)(decimal?)decimal.Parse(form["fiyat"]),
                UrunDurumu = form["durum"],
                Marka = "SemihYürükcü" // Sabit değer
            };

            // Resim yükleme işlemi
            if (UrunResim != null && UrunResim.Length > 0)
            {
                var fileName = Path.GetFileName(UrunResim.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    UrunResim.CopyTo(stream);
                }

                seller.UrunResim = fileName;
            }

            // Veritabanına ekleme işlemi
            _context.Sellers.Add(seller);

            try
            {
                _context.SaveChanges();

                // Urun_Kayiti_Onaylandi TempData ayarı
                TempData["Urun_Kayiti_Onaylandi"] = true;

                return RedirectToAction("Index", "Satici_Olustur");
            }
            catch (Exception ex)
            {
                // Hata yönetimi
                return RedirectToAction("Error", "Main");
            }
            //catch (DbUpdateException ex)
            //{
            //    // Hata loglama veya hata mesajları
            //    ModelState.AddModelError("", "Veritabanı hatası: " + ex.Message);
            //    return View();
            //}
        }

        public IActionResult Index()
        {
            return View();
        }
    
 
    }
}
