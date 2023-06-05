using Microsoft.AspNetCore.Mvc;
using P013EStore.Core.Entities;
using P013EStore.Service.Abstract;

namespace P013EStore.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUsersController : ControllerBase
    {
        private readonly IService<AppUser> _service;

        public AppUsersController(IService<AppUser> service)
        {
            _service = service;
        }
        // GET: api/<AppUsersController>
        [HttpGet]
        public IEnumerable<AppUser> Get()
        {
            return _service.GetAll();
        }

        // GET api/<AppUsersController>/5
        [HttpGet("{id}")]
        public AppUser Get(int id)
        {
            return _service.Find(id);
        }

        // POST api/<AppUsersController>
        [HttpPost]
        public void Post([FromBody] AppUser value)
        {
            _service.Add(value);
            _service.Save();
        }

        // PUT api/<AppUsersController>/5
        [HttpPut]
        public void Put([FromBody] AppUser value)
        {
            _service.Update(value);
            _service.Save();
        }

        // DELETE api/<AppUsersController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id) // geriye bir değer döneceksek void kullanmamalıyız
        {
            var kayit = _service.Find(id);
            if (kayit == null)
            {
                return BadRequest();
            }
            _service.Delete(kayit);
            _service.Save();
            return Ok(kayit);
        }
    }
}
