using Microsoft.AspNetCore.Mvc;
using P013EStore.Core.Entities;
using P013EStore.MVCUI.Models;
using P013EStore.Service.Abstract;

namespace P013EStore.MVCUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IService<AppUser> _service;

        public AccountController(IService<AppUser> service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(UserLoginViewModel viewModel)
        {
            return View();
        }
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignInAsync(AppUser appUser)
        {
            try
            {
                var kullanici = await _service.GetAsync(x => x.Email == appUser.Email);
                if (kullanici != null)
                {
                    ModelState.AddModelError("", "Bu Mail İle Daha Önce Kayıt Olunmuş!");
                    return View();
                }
                else
                {
                    appUser.UserGuid = Guid.NewGuid();
                    appUser.IsActive = true;
                    appUser.IsAdmin = false;
                    await _service.AddAsync(appUser);
                    await _service.SaveAsync();
                    return RedirectToAction(nameof(Login));
                }
            }
            catch
            {
                ModelState.AddModelError("", "Hata Oluştu!");
            }
            return View(appUser);
        }
        public IActionResult NewPassword()
        {
            return View();
        }
    }
}
