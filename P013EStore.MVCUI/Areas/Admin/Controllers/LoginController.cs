using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using P013EStore.Core.Entities;
using P013EStore.MVCUI.Models;
using P013EStore.Service.Abstract;
using System.Security.Claims;

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
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(); // Sistemden çıkış yap
            return Redirect("/Admin/Login"); // tekrardan giriş sayfasına yönlendir
        }
        [HttpPost]
        public async Task<IActionResult> IndexAsync(AdminLoginViewModel admin)
        {
            try
            {
                var kullanici = await _service.GetAsync(k => k.IsActive && k.Email == admin.Email && k.Password == admin.Password);
                if (kullanici != null)
                {
                    var kullaniciYetkileri = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, kullanici.Email),
                        new Claim("Role", kullanici.IsAdmin ? "Admin" : "User"),
                        new Claim("UserGuid", kullanici.UserGuid.ToString())
                    };
                    var kullaniciKimligi = new ClaimsIdentity(kullaniciYetkileri, "Login");
                    ClaimsPrincipal principal = new(kullaniciKimligi);
                    await HttpContext.SignInAsync(principal); // HttpContext .net içerisinden geliyor ve uygulamanın çalışma anındaki içeriğe ulaşmamızı sağlıyor. SignInAsync metodu da .net içerisinde mevcut login işlemi yapmak istersek buradaki gibi kullanıyoruz.
                    return Redirect("/Admin/Main");
                }
                else // eğer kullanıcı bilgileri yanlış girmişse veya kullanıcı db den silinmişse
                    ModelState.AddModelError("", "Giriş Başarısız!");
            }
            catch (Exception hata)
            {
                ModelState.AddModelError("", "Hata Oluştu! " + hata.Message);
            }
            return View();
        }
    }
}
