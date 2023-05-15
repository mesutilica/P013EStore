using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace P013EStore.MVCUI.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize]
    public class MainController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
