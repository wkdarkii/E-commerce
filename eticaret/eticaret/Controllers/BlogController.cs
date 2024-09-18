using Microsoft.AspNetCore.Mvc;

namespace eticaret.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Blog_Grid()
        {
            return View();
        }
    }
}
