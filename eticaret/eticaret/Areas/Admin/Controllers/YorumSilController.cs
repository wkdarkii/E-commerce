using eticaret.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eticaret.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class YorumSilController : Controller
    {
        private readonly AppDbContext _context;
        public YorumSilController(AppDbContext context)
        {
            _context = context;
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var yorum = await _context.UrunYorumlari.FindAsync(id);
            if (yorum != null)
            {
                _context.UrunYorumlari.Remove(yorum);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index", "KullaniciYorum");
        }
    }
}
