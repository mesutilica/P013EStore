using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using P013EStore.Core.Entities;
using P013EStore.MVCUI.Utils;
using P013EStore.Service.Abstract;

namespace P013EStore.MVCUI.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Policy = "AdminPolicy")]
    public class SettingsController : Controller
    {
        private readonly IService<Setting> _service;

        public SettingsController(IService<Setting> service)
        {
            _service = service;
        }

        // GET: SettingsController
        public async Task<ActionResult> Index()
        {
            var model = await _service.GetAllAsync();
            return View(model);
        }

        // GET: SettingsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SettingsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SettingsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(Setting collection, IFormFile? Logo, IFormFile? Favicon)
        {
            try
            {
                if (Logo is not null)
                    collection.Logo = await FileHelper.FileLoaderAsync(Logo);
                if (Favicon is not null)
                    collection.Favicon = await FileHelper.FileLoaderAsync(Favicon);
                await _service.AddAsync(collection);
                await _service.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Hata Oluştu!");
            }
            return View(collection);
        }

        // GET: SettingsController/Edit/5
        public async Task<ActionResult> EditAsync(int? id)
        {
            if (id is not null)
            {
                return View(await _service.FindAsync(id.Value));
            }
            return View();
        }

        // POST: SettingsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, Setting collection, IFormFile? Favicon, IFormFile? Logo, bool? logoyuSil, bool? faviconuSil)
        {
            try
            {
                if (Logo is not null)
                {
                    collection.Logo = await FileHelper.FileLoaderAsync(Logo);
                }
                if (Favicon is not null)
                    collection.Favicon = await FileHelper.FileLoaderAsync(Favicon);
                _service.Update(collection);
                await _service.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Hata Oluştu!");
            }
            return View(collection);
        }

        // GET: SettingsController/Delete/5
        public async Task<ActionResult> DeleteAsync(int id)
        {
            return View(await _service.FindAsync(id));
        }

        // POST: SettingsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAsync(int id, Setting collection)
        {
            try
            {
                var ayarlar = await _service.GetAllAsync();
                if (ayarlar.Count > 1)
                {
                    _service.Delete(collection);
                    await _service.SaveAsync();
                }
                else
                {
                    TempData["Message"] = "<div class='alert alert-danger'>Kayıt Silinemedi! Sistemde en azından 1 tane ayar bulunmalıdır!!</div>";
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Hata Oluştu!");
            }
            return View(collection);
        }
    }
}
