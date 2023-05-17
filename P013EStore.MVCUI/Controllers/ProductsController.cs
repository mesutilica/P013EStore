using Microsoft.AspNetCore.Mvc;
using P013EStore.Core.Entities;
using P013EStore.MVCUI.Models;
using P013EStore.Service.Abstract;

namespace P013EStore.MVCUI.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _serviceProduct;

        public ProductsController(IProductService serviceProduct)
        {
            _serviceProduct = serviceProduct;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var model = await _serviceProduct.GetAllAsync(p => p.IsActive);
            return View(model);
        }

        public async Task<IActionResult> DetailAsync(int id)
        {
            var model = new ProductDetailViewModel();
            var product = await _serviceProduct.GetProductByIncludeAsync(id);
            model.Product = product;
            model.RelatedProducts = await _serviceProduct.GetAllAsync(p => p.CategoryId == product.CategoryId && p.Id != id);
            if (model is null)
            {
                return NotFound();
            }
            return View(model);
        }
    }
}
