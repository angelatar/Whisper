using Microsoft.AspNetCore.Mvc;
using UserAPI.Businness;
using UserAPI.Models;

namespace UserAPI.Controllers
{
    [Produces("application/json")]
    public class RegisterController : Controller
    {
        private readonly UsersRepository repository;

        public RegisterController(UsersRepository repo)
        {
            this.repository = repo;
        }

        [Route("api/Register")]
        [HttpPost]
        public bool Post([FromBody]User user)
        {
            return this.repository.CreateUser(user);
        }
    }
}