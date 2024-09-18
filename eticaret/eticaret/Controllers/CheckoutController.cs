using eticaret.Data;
using eticaret.Models;
using Iyzipay.Model;
using Iyzipay.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Globalization;
using System.Linq;

namespace eticaret.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly Iyzipay.Options _options;
        private readonly AppDbContext _context;

        // Tek bir constructor
        public CheckoutController(IConfiguration configuration, AppDbContext context)
        {
            _context = context;
            _options = new Iyzipay.Options
            {
                ApiKey = configuration["IyzicoOptions:ApiKey"],
                SecretKey = configuration["IyzicoOptions:SecretKey"],
                BaseUrl = configuration["IyzicoOptions:BaseUrl"]
            };
        }





        [HttpPost]
        public IActionResult CheckBeforeCheckout(decimal totalPrice)
        {
            // Kullanıcı oturum bilgilerini al
            string kullaniciAdi = HttpContext.Session.GetString("Kullanici_adi");
            string email = HttpContext.Session.GetString("Email");

            // Eğer oturum bilgileri boşsa, kullanıcıyı giriş sayfasına yönlendir
            if (string.IsNullOrEmpty(kullaniciAdi) || string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Login", "Account"); // Giriş sayfası yönlendirmesi
            }

            // Kullanıcının ID'sini users tablosundan al
            var user = _context.Users.FirstOrDefault(u => u.Kullanici_adi == kullaniciAdi && u.Email == email);
            if (user == null)
            {
                return RedirectToAction("Login", "Account"); // Eğer kullanıcı bulunamazsa, giriş sayfasına yönlendirme
            }

            // Kullanıcıya ait id ile usercards tablosunda kayıt var mı kontrol et
            bool hasCard = _context.UserCards.Any(uc => uc.UserId == user.id);

            if (hasCard)
            {
                // Kayıt varsa ödeme işlemini başlatmak için Checkout/Index'e yönlendirme
                CreatePaymentRequest request = new CreatePaymentRequest
                {
                    Locale = Locale.TR.ToString(),
                    ConversationId = "123456789",  // Benzersiz bir ID oluşturabilirsiniz.
                    Price = totalPrice.ToString("F2", CultureInfo.InvariantCulture),
                    PaidPrice = totalPrice.ToString("F2", CultureInfo.InvariantCulture),
                    Currency = Currency.TRY.ToString(),
                    Installment = 1,
                    BasketId = "B67832",  // Bu sepet ID'si örnek.
                    PaymentChannel = PaymentChannel.WEB.ToString(),
                    PaymentGroup = PaymentGroup.PRODUCT.ToString(),
                    PaymentCard = new PaymentCard
                    {
                        CardHolderName = "John Doe",
                        CardNumber = "5528790000000008",  // Iyzico'nun sağladığı test kartı
                        ExpireMonth = "12",
                        ExpireYear = "2030",
                        Cvc = "123",
                        RegisterCard = 0
                    },
                    Buyer = new Buyer
                    {
                        Id = "BY789",
                        Name = "John",
                        Surname = "Doe",
                        GsmNumber = "+905350000000",
                        Email = "email@email.com",
                        IdentityNumber = "74300864791",
                        LastLoginDate = "2023-01-01 12:43:35",
                        RegistrationDate = "2023-01-01 12:43:35",
                        RegistrationAddress = "Nişantaşı Mahallesi, Şişli",
                        Ip = "85.34.78.112",
                        City = "Istanbul",
                        Country = "Turkey",
                        ZipCode = "34732"
                    },
                    ShippingAddress = new Address
                    {
                        ContactName = "Jane Doe",
                        City = "Istanbul",
                        Country = "Turkey",
                        Description = "Nişantaşı Mahallesi, Şişli",
                        ZipCode = "34732"
                    },
                    BillingAddress = new Address
                    {
                        ContactName = "Jane Doe",
                        City = "Istanbul",
                        Country = "Turkey",
                        Description = "Nişantaşı Mahallesi, Şişli",
                        ZipCode = "34732"
                    },
                    BasketItems = new List<BasketItem>
                {
                    new BasketItem
                    {
                        Id = "BI101",
                        Name = "Telefon",
                        Category1 = "Elektronik",
                        ItemType = BasketItemType.PHYSICAL.ToString(),
                        Price = totalPrice.ToString("F2", CultureInfo.InvariantCulture)
                    }
                }
                };

                Payment payment = Payment.Create(request, _options);

                if (payment.Status == "success")
                {
                    //  return RedirectToAction("Success");  // Başarılı ödeme sayfasına yönlendir.

                    // Veritabanından kullanıcı ID'sini bul
                    var userr = _context.Users
                        .FirstOrDefault(u => u.Kullanici_adi == kullaniciAdi && u.Email == email);

                    if (userr == null)
                    {
                        return Unauthorized(); // Kullanıcı bulunamadı
                    }

                    // Sepetteki ürünleri al
                    var sepettekiUrunler = _context.SepeteEklenenUrun
                        .Where(s => s.user_id == userr.id)
                        .ToList();

                    // Sepetteki ürünleri siparişler tablosuna ekle
                    foreach (var urun in sepettekiUrunler)
                    {
                        var siparis = new Siparis
                        {
                            UserId = userr.id,
                            ProductId = urun.product_id,
                            SiparisTarihi = DateTime.Now

                        };

                        _context.Siparisler.Add(siparis);
                    }

                    // Sepet ürünlerini temizle
                    _context.SepeteEklenenUrun.RemoveRange(sepettekiUrunler);

                    // Veritabanı işlemlerini kaydet
                    _context.SaveChanges();



                    var toplamPara = new ToplamPara
                    {
                        UserId = user.id,
                        Miktar = totalPrice,
                        alma_zamani = DateTime.Now
                    };

                    _context.ToplamParalar.Add(toplamPara);
                    _context.SaveChanges(); // Değişiklikleri kaydet



                    TempData["SiparisOnaylandi"] = true; // Sipariş onaylandı flag
                    return RedirectToAction("Siparislerim", "Sepet");
                }
                else
                {
                    var errorDetails = $"Hata Kodu: {payment.ErrorCode}, Hata Mesajı: {payment.ErrorMessage}";
                    ViewBag.ErrorMessage = errorDetails;
                    Console.WriteLine(JsonConvert.SerializeObject(request));

                    return View("Failure");
                }
            }
            else
            {
                // Kayıt yoksa kullanıcıyı kart profilini güncelleme sayfasına yönlendir
                return RedirectToAction("kartProfili", "Kullanıcıarayüz");
            }
        }


        //[HttpPost]
        //public IActionResult Index(decimal totalPrice)
        //{
        //    CreatePaymentRequest request = new CreatePaymentRequest
        //    {
        //        Locale = Locale.TR.ToString(),
        //        ConversationId = "123456789",  // Benzersiz bir ID oluşturabilirsiniz.
        //        Price = totalPrice.ToString("F2", CultureInfo.InvariantCulture),
        //        PaidPrice = totalPrice.ToString("F2", CultureInfo.InvariantCulture),
        //        Currency = Currency.TRY.ToString(),
        //        Installment = 1,
        //        BasketId = "B67832",  // Bu sepet ID'si örnek.
        //        PaymentChannel = PaymentChannel.WEB.ToString(),
        //        PaymentGroup = PaymentGroup.PRODUCT.ToString(),
        //        PaymentCard = new PaymentCard
        //        {
        //            CardHolderName = "John Doe",
        //            CardNumber = "5528790000000008",  // Iyzico'nun sağladığı test kartı
        //            ExpireMonth = "12",
        //            ExpireYear = "2030",
        //            Cvc = "123",
        //            RegisterCard = 0
        //        },
        //        Buyer = new Buyer
        //        {
        //            Id = "BY789",
        //            Name = "John",
        //            Surname = "Doe",
        //            GsmNumber = "+905350000000",
        //            Email = "email@email.com",
        //            IdentityNumber = "74300864791",
        //            LastLoginDate = "2023-01-01 12:43:35",
        //            RegistrationDate = "2023-01-01 12:43:35",
        //            RegistrationAddress = "Nişantaşı Mahallesi, Şişli",
        //            Ip = "85.34.78.112",
        //            City = "Istanbul",
        //            Country = "Turkey",
        //            ZipCode = "34732"
        //        },
        //        ShippingAddress = new Address
        //        {
        //            ContactName = "Jane Doe",
        //            City = "Istanbul",
        //            Country = "Turkey",
        //            Description = "Nişantaşı Mahallesi, Şişli",
        //            ZipCode = "34732"
        //        },
        //        BillingAddress = new Address
        //        {
        //            ContactName = "Jane Doe",
        //            City = "Istanbul",
        //            Country = "Turkey",
        //            Description = "Nişantaşı Mahallesi, Şişli",
        //            ZipCode = "34732"
        //        },
        //        BasketItems = new List<BasketItem>
        //        {
        //            new BasketItem
        //            {
        //                Id = "BI101",
        //                Name = "Telefon",
        //                Category1 = "Elektronik",
        //                ItemType = BasketItemType.PHYSICAL.ToString(),
        //                Price = totalPrice.ToString("F2", CultureInfo.InvariantCulture)
        //            }
        //        }
        //    };

        //    Payment payment = Payment.Create(request, _options);

        //    if (payment.Status == "success")
        //    {
        //        return RedirectToAction("Success");  // Başarılı ödeme sayfasına yönlendir.
        //    }
        //    else
        //    {
        //        var errorDetails = $"Hata Kodu: {payment.ErrorCode}, Hata Mesajı: {payment.ErrorMessage}";
        //        ViewBag.ErrorMessage = errorDetails;
        //        Console.WriteLine(JsonConvert.SerializeObject(request));

        //        return View("Failure");
        //    }
        //}

        [HttpGet]
        [Route("checkout/success")]
        public IActionResult Success()
        {
            // Başarıyla ilgili işlemleri burada yapabilirsiniz
            ViewBag.Message = "Ödeme işlemi başarıyla tamamlandı!";
            return View();
        }

        [HttpGet]
        [Route("checkout/failure")]
        public IActionResult Failure()
        {
            // Başarısızlıkla ilgili işlemleri burada yapabilirsiniz
            ViewBag.Message = "Ödeme işlemi başarısız oldu!";
            return View();
        }

        [HttpGet]
        [Route("checkout/callback")]
        public IActionResult Callback()
        {
            // Burada, ödeme işleminin sonucunu işleyebilirsiniz
            // Örneğin, başarılı veya başarısız bir ödeme sonucunu kullanıcıya gösterebilirsiniz

            // Bu örnekte basit bir başarı mesajı gösteriyoruz
            ViewBag.Message = "Ödeme işlemi başarılı!";
            return View();
        }

        [HttpPost]
        public IActionResult Rapor(int SiparisNo, string SiparisDurumu, DateTime SiparisTarihi, decimal Fiyat)
        {
            // Veritabanından ilgili sipariş kaydını bulalım
            var siparis = _context.Siparisler.FirstOrDefault(s => s.Id == SiparisNo);

            if (siparis == null)
            {
                // Eğer sipariş bulunamazsa bir hata sayfasına yönlendirebiliriz veya bir hata mesajı gösterebiliriz
                return NotFound();
            }

            // Siparişin product_id'sini alıyoruz
            int productId = siparis.ProductId;

            // Şimdi products tablosundan bu product_id'ye karşılık gelen ürünü bulalım
            var product = _context.Products.FirstOrDefault(p => p.id == productId);

            if (product == null)
            {
                // Eğer ürün bulunamazsa bir hata sayfasına yönlendirebiliriz veya bir hata mesajı gösterebiliriz
                return NotFound();
            }

            // Ürün adını (name) modeldeki SiparisNo alanına atıyoruz (isteğiniz doğrultusunda)
            var model = new RaporViewModel
            {
                SiparisNo = product.name, // Burada sipariş no yerine ürün adını yerleştiriyoruz
                SiparisDurumu = SiparisDurumu,
                SiparisTarihi = SiparisTarihi.ToString("dd MMMM yyyy"),
                Fiyat = Fiyat.ToString("C") // Fiyatı para birimi formatında göster
            };

            return View(model);
        }



    }
}
