using Microsoft.AspNetCore.Mvc;
using P013EStore.Core.Entities;
using P013EStore.Service.Abstract;

namespace P013EStore.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IService<Brand> _service; // readonly nesneler sadece constructor metotta doldurulabilir
        private readonly IService<Product> _serviceProduct;
        public BrandsController(IService<Brand> service, IService<Product> serviceProduct)
        {
            _service = service;
            _serviceProduct = serviceProduct;
        }
        // GET: api/<BrandsController>
        [HttpGet]
        public async Task<IEnumerable<Brand>> GetAsync()
        {
            return await _service.GetAllAsync();
        }

        // GET api/<BrandsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Brand>> GetAsync(int id)
        {
            var model = await _service.FindAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            model.Products = await _serviceProduct.GetAllAsync(p => p.BrandId == id);
            return Ok(model);
        }

        // POST api/<BrandsController>
        [HttpPost]
        public async Task<Brand> PostAsync([FromBody] Brand value)
        {
            await _service.AddAsync(value);
            await _service.SaveAsync();
            return value;
        }

        // PUT api/<BrandsController>/5
        [HttpPut]
        public async Task<ActionResult> PutAsync([FromBody] Brand value)
        {
            _service.Update(value);
            int sonuc = await _service.SaveAsync();
            if (sonuc > 0)
            {
                return Ok(value);
            }
            return StatusCode(StatusCodes.Status304NotModified);
        }

        // DELETE api/<BrandsController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var kayit = _service.Find(id);
            if (kayit == null)
            {
                return NotFound();
            }
            _service.Delete(kayit);
            _service.Save();
            return Ok(kayit);
        }
    }
}
