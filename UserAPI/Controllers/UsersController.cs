using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserAPI.Businness;
using UserAPI.Models;

namespace UserAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/users")]
    public class UsersController : Controller
    {
        private readonly UsersRepository repository;

        public UsersController()
        {
            this.repository = new UsersRepository();
        }

        // GET api/values
        [HttpGet]
        [Authorize]
        public IEnumerable<User> Get()
        {
            //return new List<string>() { "Hi", " user" };
            return this.repository.GetUsers();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public User Get(int id)
        {
            return this.repository.GetUserByID(id);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]User user)
        {
            this.repository.CreateUser(user);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
            this.repository.UpdateUserPassword(id, value);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.repository.DeleteUser(id);
        }
    }
}
