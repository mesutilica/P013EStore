using Microsoft.AspNetCore.Mvc;
using P013EStore.Core.Entities;
using P013EStore.MVCUI.Models;
using P013EStore.Service.Abstract;

namespace P013EStore.MVCUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoginController : Controller
    {
        private readonly IService<AppUser> _service;

        public LoginController(IService<AppUser> service)
        {
            _service = service;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(AdminLoginViewModel admin)
        {
            return View();
        }
    }
}
