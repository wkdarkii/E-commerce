using Microsoft.AspNetCore.Mvc;

namespace eticaret.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult about()
        {
            return View();
        }

      
    }
}
