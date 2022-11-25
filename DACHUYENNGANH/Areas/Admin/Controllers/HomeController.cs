using Microsoft.AspNetCore.Mvc;

namespace DACHUYENNGANH.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public IActionResult IndexAdmin()
        {
            return View();
        }
      
    }
}
