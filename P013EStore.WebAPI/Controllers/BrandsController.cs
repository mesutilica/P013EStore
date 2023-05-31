﻿using Microsoft.AspNetCore.Mvc;
using P013EStore.Core.Entities;
using P013EStore.Service.Abstract;

namespace P013EStore.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IService<Brand> _service; // readonly nesneler sadece constructor metotta doldurulabilir
        public BrandsController(IService<Brand> service)
        {
            _service = service;
        }
        // GET: api/<BrandsController>
        [HttpGet]
        public async Task<IEnumerable<Brand>> GetAsync()
        {
            return await _service.GetAllAsync();
        }

        // GET api/<BrandsController>/5
        [HttpGet("{id}")]
        public async Task<Brand> GetAsync(int id)
        {
            return await _service.FindAsync(id);
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
        [HttpPut("{id}")]
        public async Task<ActionResult> PutAsync(int id, [FromBody] Brand value)
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
                return NoContent();
            }
            _service.Delete(kayit);
            _service.Save();
            return Ok(kayit);
        }
    }
}
