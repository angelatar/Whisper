using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UserAPI.Businness;
using System.Linq;

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

        [HttpGet]
        public IActionResult Get(string code)
        {
            var id = this.GetUserId();
            return Json(this.repo.CheckValidationCode(id,code));
        }
        
        [HttpPost]
        public bool Post([FromBody]string code)
        { 
            var id = this.GetUserId();
            return this.repo.InstertValidationCode(id, code);
        }
        
        [HttpDelete]
        public void Delete()
        {
            var id = this.GetUserId();
            this.repo.DeleteValidationCode(id);
        }

        private int GetUserId()
        {
            return int.Parse(
                ((ClaimsIdentity)this.User.Identity).Claims
                .Where(claim => claim.Type == "user_id").First().Value);
        }
    }
}