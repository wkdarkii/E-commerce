using eticaret.Data;
using eticaret.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;
using eticaret.Data;

namespace eticaret.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UrunEkleController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UrunEkleController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(Product model)
        {
            if (!ModelState.IsValid)
            {

                if (model.FotoFile != null && model.FotoFile.Length > 0)
                {
                    // Dosya ismi ile birlikte uzantısını al
                    var fileName = Path.GetFileName(model.FotoFile.FileName);

                    // Kaydetme yolu (wwwroot içerisindeki istediğiniz dizine kaydedilecek)
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/dasdasdsadasas/www.elathemes.com/themes/lager/assets/images", fileName);

                    // Dosyayı kaydet
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.FotoFile.CopyToAsync(stream);
                    }

                    // Dosya adını veritabanına kaydetmek için modelin img propertysine ata
                    model.img = fileName;
                }
                // Diğer alanları da veritabanına kaydet
                _context.Products.Add(model); // Products DbSet tanımlı olmalı
                await _context.SaveChangesAsync();

                // Başarılı olursa bir başarı sayfasına veya aynı sayfaya yönlendirebilirsin
                TempData["Product_Urun_Basarıyla_Eklendi"] = true;
                return RedirectToAction("Index");
            }
            TempData["Product_Urun_Basarıyla_Eklendi"] = true;
            return View(model);
            //foreach (var modelState in ModelState.Values)
            //{
            //    foreach (var error in modelState.Errors)
            //    {
            //        Console.WriteLine(error.ErrorMessage); // veya loglayabilirsiniz
            //    }
            //}
        }


            //if (ModelState.IsValid)
            //{
            //    // 1. Resmin ismini alalım
              
            //    if (model.FotoFile != null && model.FotoFile.Length > 0)
            //    {
            //        // Dosya ismi ile birlikte uzantısını al
            //        var fileName = Path.GetFileName(model.FotoFile.FileName);

            //        // Kaydetme yolu (wwwroot içerisindeki istediğiniz dizine kaydedilecek)
            //        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/dasdasdsadasas/www.elathemes.com/themes/lager/assets/images", fileName);

            //        // Dosyayı kaydet
            //        using (var stream = new FileStream(filePath, FileMode.Create))
            //        {
            //            await model.FotoFile.CopyToAsync(stream);
            //        }

            //        // Dosya adını veritabanına kaydetmek için modelin img propertysine ata
            //        model.img = fileName;
            //    }
            //    // Diğer alanları da veritabanına kaydet
            //    _context.Products.Add(model); // Products DbSet tanımlı olmalı
            //    await _context.SaveChangesAsync();

            //    // Başarılı olursa bir başarı sayfasına veya aynı sayfaya yönlendirebilirsin
            //    return RedirectToAction("Index");
            //}

            //return View(model);
        }
    }

