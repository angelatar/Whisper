using Microsoft.AspNetCore.Mvc;
using UserAPI.Businness;

namespace UserAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Validation")]
    public class ValidationController : Controller
    {
        private readonly ValidationRepository repo;

        public ValidationController(ValidationRepository repository)
        {
            this.repo = repository;
        }

        //[HttpGet]
        //public IActionResult Get()
        //{
        //    return Json(new List<string>() { "Hi", " user" });
        //}

        [HttpGet]
        //[Route("{email}&{code}")]
        public IActionResult Get(string email,string code)
        {
            //return Json(new List<string>() { "bye", " user" });
            return Json(this.repo.CheckValidationCode(email, code));
        }
        
        [HttpPost]
        //[Route("api/Validation/{email}&{code}")]
        public bool Post(string email)
        { 
            return this.repo.InstertValidationCode(email);
        }
        
        [HttpDelete]
        //[Route("api/Validation/{email}")]
        public bool Delete(string email)
        {
            return this.repo.DeleteValidationCode(email);
        }

    }
}