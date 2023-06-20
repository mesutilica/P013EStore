using Microsoft.AspNetCore.Mvc;
using P013EStore.Core.Entities;
using P013EStore.MVCUI.Models;
using P013EStore.Service.Abstract;

namespace P013EStore.MVCUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IService<Slider> _serviceSlider;
        private readonly IService<Product> _serviceProduct;
        private readonly IService<Contact> _serviceContact;
        private readonly IService<News> _serviceNews;
        private readonly IService<Brand> _serviceBrand;
        private readonly IService<AppLog> _serviceLog;
        private readonly IService<Setting> _serviceSetting;

        public HomeController(IService<Slider> serviceSlider, IService<Product> serviceProduct, IService<Contact> serviceContact, IService<News> serviceNews, IService<Brand> serviceBrand, IService<AppLog> serviceLog, IService<Setting> serviceSetting)
        {
            _serviceSlider = serviceSlider;
            _serviceProduct = serviceProduct;
            _serviceContact = serviceContact;
            _serviceNews = serviceNews;
            _serviceBrand = serviceBrand;
            _serviceLog = serviceLog;
            _serviceSetting = serviceSetting;
        }

        public async Task<IActionResult> Index()
        {
            var model = new HomePageViewModel()
            {
                Sliders = await _serviceSlider.GetAllAsync(),
                Products = await _serviceProduct.GetAllAsync(p => p.IsActive && p.IsHome),
                Brands = await _serviceBrand.GetAllAsync(b => b.IsActive),
                News = await _serviceNews.GetAllAsync(n => n.IsActive && n.IsHome)
            };
            return View(model);
        }

        public async Task<IActionResult> PrivacyAsync()
        {
            //
            try
            {
                throw new Exception("Bilinmeyen bir hata");
            }
            catch (Exception hata)
            {
                await _serviceLog.AddAsync(new P013EStore.Core.Entities.AppLog()
                {
                    Title = "Home/Privacy Sayfasında Hata Oluştu " + hata.Message,
                    Description = "Oluşan Hata " + hata.InnerException.ToString()
                });
                await _serviceLog.SaveAsync();
                //
            }
            return View();
        }
        [Route("iletisim")]
        public async Task<IActionResult> ContactUsAsync()
        {
            //var model = await _serviceSetting.GetAllAsync();
            //if (model != null)
            //    return View(model.FirstOrDefault());
            return View();
        }
        [Route("iletisim"), HttpPost]
        public async Task<IActionResult> ContactUsAsync(Contact contact)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _serviceContact.AddAsync(contact);
                    var sonuc = await _serviceContact.SaveAsync();
                    if (sonuc > 0)
                    {
                        // await MailHelper.SendMailAsync(contact); // gelen mesajı mail gönder.
                        TempData["Message"] = "<div class='alert alert-success'>Mesajınız Gönderildi! Teşekkürler..</div>";
                        return RedirectToAction("ContactUs");
                    }
                }
                catch (Exception hata)
                {
                    await _serviceLog.AddAsync(new P013EStore.Core.Entities.AppLog()
                    {
                        Title = "İletişim Formu Gönderilirken Hata Oluştu!",
                        Description = hata.Message
                    });
                    await _serviceLog.SaveAsync();
                    // await MailHelper.SendMailAsync(contact); // oluşan hatayı yazılımcıya mail gönder.
                    ModelState.AddModelError("", "Hata Oluştu!");
                }
            }
            return View(contact);
        }
        [Route("AccessDenied")]
        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}