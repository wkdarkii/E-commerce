using Microsoft.AspNetCore.Mvc;

namespace eticaret.Controllers
{
    public class ArticleController : Controller
    {
        public IActionResult article()
        {
            return View();
        }
    }
}
