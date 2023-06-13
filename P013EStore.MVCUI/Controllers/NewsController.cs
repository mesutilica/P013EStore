using Microsoft.AspNetCore.Mvc;
using P013EStore.Core.Entities;
using P013EStore.Service.Abstract;

namespace P013EStore.MVCUI.Controllers
{
    public class NewsController : Controller
    {
        private readonly IService<News> _serviceNews;

        public NewsController(IService<News> serviceNews)
        {
            _serviceNews = serviceNews;
        }

        public async Task<IActionResult> IndexAsync()
        {
            return View(await _serviceNews.GetAllAsync(n => n.IsActive));
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var model = await _serviceNews.FindAsync(id.Value);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }
    }
}
