using Microsoft.AspNetCore.Mvc;
using eticaret.Models;
using System.IO;
using eticaret.Data;
using Iyzipay.Model.V2.Subscription;
using Microsoft.AspNetCore.Hosting;

namespace eticaret.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductEditController : Controller
    {
        private readonly AppDbContext _context;  // Veritabanı context'i
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductEditController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // Tüm ürünleri listeleme
        public IActionResult Index()
        {
            var products = _context.Products.ToList(); // Ürün listesini al
            return View(products);
        }

        // Ürün güncelleme sayfası (modal'dan tetiklenecek)
        [HttpGet]
        public IActionResult Product_Edit(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: /Admin/ProductEdit/Product_Edit
        [HttpPost]
        public async Task<IActionResult> Product_Edit(eticaret.Models.Product model)
        {

            if (!ModelState.IsValid)
            {

                var product = await _context.Products.FindAsync(model.id);
                if (product == null)
                {
                    return NotFound();
                }

                product.name = model.name;
                product.price = model.price;
                product.description = model.description;
                product.discount = model.discount;
                product.label = model.label;
                product.cinsiyet = model.cinsiyet;
                product.cocuk = model.cocuk;
                product.alan_adi = model.alan_adi;
                product.category_id = model.category_id;

                // Resim güncelleme işlemi
                if (model.FotoFile != null)
                {
                    var fileName = Path.GetFileName(model.FotoFile.FileName);
                    var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "wwwroot/dasdasdsadasas/www.elathemes.com/themes/lager/assets/images", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.FotoFile.CopyToAsync(stream);
                    }

                    product.img = fileName;
                }

                _context.Update(product);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Ürün başarıyla güncellendi!";
                return RedirectToAction("Index"); // Başarıyla güncellenirse listeye dön
            }

            if (ModelState.IsValid)
            {
                var product = await _context.Products.FindAsync(model.id);
                if (product == null)
                {
                    return NotFound();
                }

                // Güncellenen verileri set et
                product.name = model.name;
                product.price = model.price;
                product.description = model.description;
                product.discount = model.discount;
                product.label = model.label;
                product.cinsiyet = model.cinsiyet;
                product.cocuk = model.cocuk;
                product.alan_adi = model.alan_adi;
                product.category_id = model.category_id;

                // Fotoğraf dosyasını güncelle
                if (model.FotoFile != null)
                {
                    // Dosya adını al
                    var fileName = Path.GetFileName(model.FotoFile.FileName);

                    // wwwroot dizininin doğru yolunu belirleyin
                    var imagesPath = Path.Combine(_webHostEnvironment.WebRootPath, "dasdasdsadasas/www.elathemes.com/themes/lager/assets/images");

                    // Dosyanın tam yolunu oluştur
                    var filePath = Path.Combine(imagesPath, fileName);

                    // Klasörün mevcut olduğundan emin olun
                    if (!Directory.Exists(imagesPath))
                    {
                        Directory.CreateDirectory(imagesPath);
                    }

                    // Dosyayı oluştur ve veriyi kopyala
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.FotoFile.CopyToAsync(stream);
                    }

                    // Ürün resmini güncelle
                    product.img = fileName;
                }


                _context.Update(product);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Ürün başarıyla güncellendi!";
                return RedirectToAction("Index"); // Başarıyla güncellendikten sonra listeleme sayfasına yönlendir
            }

            return View(model);
        }


        // Ürün güncelleme işlemi (modal'daki form submit edilecek)
        //[HttpPost]
        //public IActionResult Edit(eticaret.Models.Product model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var product = _context.Products.FirstOrDefault(p => p.id == model.id);
        //        if (product != null)
        //        {
        //            // Dosya yükleme işlemi
        //            if (model.FotoFile != null)
        //            {
        //                var fileName = Path.GetFileName(model.FotoFile.FileName);
        //                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", fileName);

        //                using (var stream = new FileStream(filePath, FileMode.Create))
        //                {
        //                    model.FotoFile.CopyTo(stream);
        //                }

        //                // Yüklenen dosyanın adı img kolonuna yazılacak
        //                product.img = fileName;
        //            }

        //            // Diğer alanlar güncelleniyor
        //            product.name = model.name;
        //            product.price = model.price;
        //            product.description = model.description;
        //            product.discount = model.discount;
        //            product.label = model.label;
        //            product.cinsiyet = model.cinsiyet;
        //            product.cocuk = model.cocuk;
        //            product.alan_adi = model.alan_adi;
        //            product.category_id = model.category_id;

        //            _context.SaveChanges(); // Değişiklikleri kaydet
        //        }

        //        return RedirectToAction("Index");
        //    }

        //    return View(model);  // Hata olursa tekrar formu göster
        //}

        // Ürün silme işlemi
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.id == id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
