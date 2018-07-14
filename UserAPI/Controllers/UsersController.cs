using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserAPI.Businness;

namespace UserAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/users")]
    public class UsersController : Controller
    {
        private readonly UsersRepository repository;

        public UsersController(UsersRepository repo)
        {
            this.repository = repo;
        }

        // GET api/values
        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            //return new List<string>() { "Hi", " user" };
            return Json(this.repository.GetUsers());
        }

        // GET api/values/5
        [HttpGet("{id:int}")]
        [Authorize]
        public IActionResult Get(int id)
        {
            return Json(this.repository.GetUserByID(id));
        }
        
        [HttpGet("{username}")]
        [Authorize]
        public IActionResult Get(string username)
        {
            return Json(this.repository.GetUserByUsername(username));
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        [Authorize]
        public void Put(int id, [FromBody]string value)
        {
            this.repository.UpdateUserPassword(id, value);
        }

        // DELETE api/values/5
        [HttpDelete]
        [Authorize]
        public void Delete()
        {
            var id = this.GetUserId();
            this.repository.DeleteUser(id);
        }

        private int GetUserId()
        {
            return int.Parse(
                ((ClaimsIdentity)this.User.Identity).Claims
                .Where(claim => claim.Type == "user_id").First().Value);
        }
    }
}
