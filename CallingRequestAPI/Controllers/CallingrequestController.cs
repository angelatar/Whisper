using System.Collections.Generic;
using CallingRequestAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CallingRequestAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class CallingRequestController : Controller
    {
        // GET api/values
        [HttpGet]
        [Authorize]
        public IEnumerable<Request> Get()
        {
            return null;//new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        [Authorize]
        public Request Get(int id)
        {
            return null;//"value";
        }

        // POST api/values
        [HttpPost]
        [Authorize]
        public void Post([FromBody]Request value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        [Authorize]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        [Authorize]
        public void Delete(int id)
        {
        }
    }
}
