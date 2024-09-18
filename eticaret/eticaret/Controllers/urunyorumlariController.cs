using eticaret.Data;
using eticaret.Models;
using Microsoft.AspNetCore.Mvc;

namespace eticaret.Controllers
{
    public class urunyorumlariController : Controller
    {
        private readonly AppDbContext _context;

        public urunyorumlariController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int productId)
        {
            if (productId <= 0)
            {
                return NotFound(); // Geçersiz bir ID geldiyse 404 döndür
            }

            // Oturum bilgilerini al
            string kullaniciAdi = HttpContext.Session.GetString("Kullanici_adi");
            string email = HttpContext.Session.GetString("Email");

            ViewBag.KullaniciAdi = kullaniciAdi;
            ViewBag.Email = email;

            // Eğer oturum bilgileri yoksa yetkisiz erişim
            if (string.IsNullOrEmpty(kullaniciAdi) || string.IsNullOrEmpty(email))
            {
                ViewBag.CanAddComment = false; // Yorum ekleme yetkisi yok
            }
            else
            {
                // Kullanıcıyı bul
                var user = _context.Users
                    .FirstOrDefault(u => u.Kullanici_adi == kullaniciAdi && u.Email == email);

                if (user == null)
                {
                    return Unauthorized(); // Kullanıcı bulunamadı
                }

                // Kullanıcının siparişlerini kontrol et
                bool hasDeliveredOrder = _context.Siparisler
                    .Any(o => o.UserId == user.id && o.ProductId == productId && o.SiparisDurumu == "Teslim Edildi");

                // Eğer herhangi bir sipariş teslim edildiyse, yorum ekleme yetkisini aç
                ViewBag.CanAddComment = hasDeliveredOrder;
                if (!hasDeliveredOrder)
                {
                    TempData["yorum_Yazamaz"] = true; // SweetAlert için TempData ile flag gönderiyoruz
                }
            }

            // Ürün yorumlarını çek
            var productComments = _context.UrunYorumlari
                     .Where(c => c.ProductID == productId)
                     .Select(c => new
                     {
                         UserName = _context.Users
                             .Where(u => u.id == c.UserId)
                             .Select(u => u.Kullanici_adi)
                             .FirstOrDefault(),
                         UserImage = _context.Users
                             .Where(u => u.id == c.UserId)
                             .Select(u => u.Profil_Foto)  // Profil fotoğrafı URL'sinin doğru ismini kullanın
                             .FirstOrDefault(),
                         Comment = c.Yorum,
                         CommentDate = c.YorumTarihi
                          })
                  .ToList();


            ViewBag.ProductComments = productComments;

            // Görünümde kullanmak üzere productId'yi View'a gönderiyoruz
            ViewBag.ProductId = productId;

        

            ViewBag.YorumKaydedildi = TempData["YorumKaydedildi"] != null;
            return View();
        }




        [HttpPost]
        public IActionResult YorumEkle(string userName, string email, string comment, int productId)
        {
            // Kullanıcıyı bul
            var user = _context.Users.FirstOrDefault(u => u.Kullanici_adi == userName && u.Email == email);

            if (user == null)
            {
                return NotFound(); // Kullanıcı bulunamadıysa hata döndür
            }

            // Yeni yorum ekle
            var newComment = new UrunYorumlari
            {
                UserId = user.id,
                ProductID = productId,
                Yorum = comment,
                YorumTarihi = DateTime.Now
            };

            _context.UrunYorumlari.Add(newComment);
            _context.SaveChanges();

            // SweetAlert gösterimini tetikle
            TempData["YorumKaydedildi"] = true;

            // Aynı sayfayı yeniden yükleyin
            return RedirectToAction("Index", new { productId = productId });
        }



    }
}
