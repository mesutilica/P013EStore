using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using P013EStore.Core.Entities;
using P013EStore.WebAPIUsing.Utils;

namespace P013EStore.WebAPIUsing.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiAdres = "https://localhost:7032/api/Products";
        private readonly string _apiAdresKategori = "https://localhost:7032/api/Categories";
        private readonly string _apiAdresMarka = "https://localhost:7032/api/Brands";
        public ProductsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        // GET: ProductsController
        public async Task<ActionResult> Index()
        {
            var model = await _httpClient.GetFromJsonAsync<List<Product>>(_apiAdres);
            return View(model);
        }

        // GET: ProductsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductsController/Create
        public async Task<ActionResult> CreateAsync()
        {
            ViewBag.CategoryId = new SelectList(await _httpClient.GetFromJsonAsync<List<Category>>(_apiAdresKategori), "Id", "Name");
            ViewBag.BrandId = new SelectList(await _httpClient.GetFromJsonAsync<List<Brand>>(_apiAdresMarka), "Id", "Name");
            return View();
        }

        // POST: ProductsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(Product collection, IFormFile? Image)
        {
            try
            {
                if (Image is not null)
                {
                    collection.Image = await FileHelper.FileLoaderAsync(Image);
                }
                var response = await _httpClient.PostAsJsonAsync(_apiAdres, collection);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                ModelState.AddModelError("", "Hata Oluştu!");
            }
            ViewBag.CategoryId = new SelectList(await _httpClient.GetFromJsonAsync<List<Category>>(_apiAdresKategori), "Id", "Name");
            ViewBag.BrandId = new SelectList(await _httpClient.GetFromJsonAsync<List<Brand>>(_apiAdresMarka), "Id", "Name");
            return View();
        }

        // GET: ProductsController/Edit/5
        public async Task<ActionResult> EditAsync(int id)
        {
            ViewBag.CategoryId = new SelectList(await _httpClient.GetFromJsonAsync<List<Category>>(_apiAdresKategori), "Id", "Name");
            ViewBag.BrandId = new SelectList(await _httpClient.GetFromJsonAsync<List<Brand>>(_apiAdresMarka), "Id", "Name");
            var model = await _httpClient.GetFromJsonAsync<Product>(_apiAdres + "/" + id);
            return View(model);
        }

        // POST: ProductsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, Product collection, IFormFile? Image, bool? resmiSil)
        {
            try
            {
                if (resmiSil is not null && resmiSil == true)
                {
                    FileHelper.FileRemover(collection.Image);
                    collection.Image = "";
                }
                if (Image is not null)
                {
                    collection.Image = await FileHelper.FileLoaderAsync(Image);
                }
                var response = await _httpClient.PutAsJsonAsync(_apiAdres, collection);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                ModelState.AddModelError("", "Hata Oluştu!");
            }
            ViewBag.CategoryId = new SelectList(await _httpClient.GetFromJsonAsync<List<Category>>(_apiAdresKategori), "Id", "Name");
            ViewBag.BrandId = new SelectList(await _httpClient.GetFromJsonAsync<List<Brand>>(_apiAdresMarka), "Id", "Name");
            return View();
        }

        // GET: ProductsController/Delete/5
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var model = await _httpClient.GetFromJsonAsync<Product>(_apiAdres + "/" + id);
            return View(model);
        }

        // POST: ProductsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAsync(int id, Product collection)
        {
            try
            {
                FileHelper.FileRemover(collection.Image);
                await _httpClient.DeleteAsync(_apiAdres + "/" + id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
