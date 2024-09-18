using Microsoft.AspNetCore.Mvc;
using eticaret.Data;
using eticaret.Models; 
using System;
using eticaret.Areas.Admin.Models;

namespace eticaret.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class Registered : Controller
    {
        private readonly AppDbContext _context;

        public Registered(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Register(string Kullanici_adi, string PhoneNumber, string Email, string Sifre, string ConfirmPassword, bool Cinsiyet, DateTime D_Tarihi)
        {
            if (Sifre != ConfirmPassword)
            {
                ModelState.AddModelError("", "Passwords do not match.");
                return View();
            }

            // Şifreyi hashle
            string hashedPassword = PasswordHelper.HashPassword(Sifre);

            // Kullanıcı adı veya şifre kontrolü
            var existingUser = _context.Users.FirstOrDefault(u => u.Kullanici_adi == Kullanici_adi || u.Sifre == hashedPassword);
            if (existingUser != null)
            {
                // Kullanıcı adı veya şifre zaten mevcut
                TempData["RegistrationError"] = "kullanıcı adı veya şifre daha önceden alınmış!!";
                return RedirectToAction("Register", "Account");
            }


            var newUser = new User
            {
                Kullanici_adi = Kullanici_adi,
                PhoneNumber = PhoneNumber,
                Email = Email,
                Sifre = hashedPassword, 
                Role = false,
                Cinsiyet = Cinsiyet,
                D_Tarihi = D_Tarihi,
                Giris_Sayisi = 3, 
                Giris_Tarihi = DateTime.Now, 
                Giris_Yapabilirmi = true 
            };

            // Kullanıcıyı veritabanına ekleme
            _context.Users.Add(newUser);
            _context.SaveChanges();

            return RedirectToAction("Login", "Account", new { area = "Admin", success = true });
        }





    }
}
