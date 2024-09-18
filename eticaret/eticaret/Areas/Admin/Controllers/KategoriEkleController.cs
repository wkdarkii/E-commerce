using eticaret.Data;
using Microsoft.AspNetCore.Mvc;

namespace eticaret.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class KategoriEkleController : Controller
    {
            private readonly AppDbContext _context;

            public KategoriEkleController(AppDbContext context)
            {
                _context = context;
            }
            public IActionResult Index()
        {
            return View();
        }
    }
}
