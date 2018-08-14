using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using UserAPI.Businness;
using UserAPI.Models;

namespace UserAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Register")]
    public class RegisterController : Controller
    {
        private readonly UsersRepository repository;

        public RegisterController(UsersRepository repo)
        {
            this.repository = repo;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Json(new List<string>() { "Hi", " user" });
        }

        [HttpPost]
        public bool Post(User user)
        {
            return this.repository.CreateUser(user);
        }
    }
}