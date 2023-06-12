using Microsoft.AspNetCore.Mvc;
using P013EStore.Core.Entities;
using P013EStore.WebAPIUsing.Models;

namespace P013EStore.WebAPIUsing.Controllers
{
    public class AccountController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiAdres = "https://localhost:7032/api/AppUsers";

        public AccountController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IActionResult> IndexAsync()
        {
            var userId = HttpContext.Session.GetInt32("userId");
            if (userId is null)
            {
                TempData["Message"] = "<div class='alert alert-danger'>Lütfen Giriş Yapınız!</div>";
                return RedirectToAction("Login");
            }
            else
            {
                var user = await _httpClient.GetFromJsonAsync<AppUser>(_apiAdres + "/" + userId);
                return View(user);
            }
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateUserAsync(AppUser appUser)
        {
            try
            {
                var userId = HttpContext.Session.GetInt32("userId");
                var user = await _httpClient.GetFromJsonAsync<AppUser>(_apiAdres + "/" + userId);
                if (user is not null)
                {
                    user.Name = appUser.Name;
                    user.Surname = appUser.Surname;
                    user.Email = appUser.Email;
                    user.Phone = appUser.Phone;
                    user.Password = appUser.Password;
                    var response = await _httpClient.PutAsJsonAsync(_apiAdres, user);
                    if (response.IsSuccessStatusCode) // api den başarılı bir istek kodu geldiyse(200 ok)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Hata Oluştu!");
            }
            return View("Index", appUser);
        }
        [HttpPost]
        public async Task<IActionResult> LoginAsync(UserLoginViewModel viewModel)
        {
            var users = await _httpClient.GetFromJsonAsync<List<AppUser>>(_apiAdres);
            var user = users.FirstOrDefault(x => x.Email == viewModel.Email && x.Password == viewModel.Password && x.IsActive);
            if (user == null)
            {
                ModelState.AddModelError("", "Giriş Başarısız!");
            }
            else
            {
                HttpContext.Session.SetInt32("userId", user.Id);
                HttpContext.Session.SetString("userGuid", user.UserGuid.ToString());
                return RedirectToAction(nameof(Index));
            }
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
                var users = await _httpClient.GetFromJsonAsync<List<AppUser>>(_apiAdres);
                var kullanici = users.FirstOrDefault(x => x.Email == appUser.Email);
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
                    var sonuc = await _httpClient.PostAsJsonAsync(_apiAdres, appUser);
                    if (sonuc.IsSuccessStatusCode)
                    {
                        TempData["Message"] = "<div class='alert alert-success'>Kayıt Başarılı! Giriş Yapabilirsiniz..</div>";
                        return RedirectToAction(nameof(Login));
                    }
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
        public IActionResult Logout()
        {
            try
            {
                HttpContext.Session.Remove("userId");
                HttpContext.Session.Remove("userGuid");
            }
            catch
            {
                HttpContext.Session.Clear();
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
