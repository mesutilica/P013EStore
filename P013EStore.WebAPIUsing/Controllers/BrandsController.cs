using Microsoft.AspNetCore.Mvc;
using P013EStore.Core.Entities;

namespace P013EStore.WebAPIUsing.Controllers
{
    public class BrandsController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiAdres = "https://localhost:7032/api/Brands/";
        public BrandsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> IndexAsync(int? id)
        {
            if (id is null)
            {
                return BadRequest();
            }
            var model = await _httpClient.GetFromJsonAsync<Brand>(_apiAdres + id.Value);
            if (model == null)
                return NotFound();
            return View(model);
        }
    }
}
