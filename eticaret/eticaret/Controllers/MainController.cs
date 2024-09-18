using Microsoft.AspNetCore.Mvc;
using eticaret.Data;
using Microsoft.EntityFrameworkCore;
using eticaret.Models;
using Newtonsoft.Json;


namespace eticaret.Controllers
{
    public class MainController : Controller
    {

        private readonly AppDbContext _context;

        public MainController(AppDbContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            // Session'dan kullanıcı bilgilerini al
            string kullaniciAdi = HttpContext.Session.GetString("Kullanici_adi");
            string email = HttpContext.Session.GetString("Email");

            // Kullanıcı bilgilerini kontrol et
            if (!string.IsNullOrEmpty(kullaniciAdi) && !string.IsNullOrEmpty(email))
            {
                // Veritabanından kullanıcıyı bul
                var user = _context.Users
                    .FirstOrDefault(u => u.Kullanici_adi == kullaniciAdi && u.Email == email);

                if (user != null)
                {
                    var model = new TempUserModel
                    {
                        Username = user.Kullanici_adi,
                        Email = user.Email,
                        Profil_Foto = user.Profil_Foto
                    };

                    // Yalnızca Onaylanan ürünleri getiriyoruz
                    var sellers = _context.Sellers
                        .Where(s => s.Onay_Asaması == "Onaylandı")
                        .ToList();

                    var webproductt = _context.Products.ToList();


                    // Ürünleri ViewBag ile view'a gönderin
                    ViewBag.Sellers = sellers;
                    ViewBag.Products = webproductt;
                    return View(model);
                }
            }

            // Eğer kullanıcı oturumu yoksa, yine onaylanan ürünleri getir
            var allSellers = _context.Sellers
                .Where(s => s.Onay_Asaması == "Onaylandı")
                .ToList();

            // Eğer kullanıcı oturumu yoksa, yine ürünler gözüksün
            var webproduct = _context.Products.ToList();



            // Ürünleri ViewBag ile view'a gönderin
            ViewBag.Sellers = allSellers;
            ViewBag.Products = webproduct;

            return View(new TempUserModel());
        }












    }
}
