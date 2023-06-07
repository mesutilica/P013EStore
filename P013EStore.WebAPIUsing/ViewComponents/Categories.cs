using Microsoft.AspNetCore.Mvc;
using P013EStore.Core.Entities;


namespace P013EStore.WebAPIUsing.ViewComponents
{
    public class Categories : ViewComponent
    {
        private readonly HttpClient _httpClient;
        public Categories(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _httpClient.GetFromJsonAsync<List<Category>>("https://localhost:7032/api/Categories"));
        }
    }
}
