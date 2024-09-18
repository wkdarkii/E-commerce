using Microsoft.AspNetCore.Mvc;

namespace eticaret.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
