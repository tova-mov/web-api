using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace login.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class bedController : ControllerBase
    {
        // GET: api/<bedController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<bedController>/
        [HttpGet]       
        [Route("/tova")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<bedController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<bedController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<bedController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
