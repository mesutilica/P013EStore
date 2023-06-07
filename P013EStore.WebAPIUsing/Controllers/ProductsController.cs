using Microsoft.AspNetCore.Mvc;
using P013EStore.Core.Entities;

namespace P013EStore.WebAPIUsing.Controllers
{
    public class ProductsController : Controller
    {
        private readonly HttpClient _httpClient;

        public ProductsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private readonly string _apiAdres = "https://localhost:7032/api/Products";
        [Route("tum-urunlerimiz")]
        public async Task<IActionResult> IndexAsync()
        {
            var model = await _httpClient.GetFromJsonAsync<List<Product>>(_apiAdres);
            return View(model);
        }
    }
}
