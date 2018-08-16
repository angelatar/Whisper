using System.Linq;
using System.Security.Claims;
using CallingRequestAPI.Businness;
using CallingRequestAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CallingRequestAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class CallingRequestController : Controller
    {
        private readonly CallingRequest repository;

        public CallingRequestController(CallingRequest repo)
        {
            this.repository = repo;
        }

        // GET api/values
        [HttpGet]
        [Authorize]
        public IActionResult Get(int id)
        {
            //var id = this.GetUserId();
            return Json(this.repository.GetRequests(id));
        }

        //// GET api/values/5
        //[HttpGet("{id}")]
        //[Authorize]
        //public IActionResult Get(int senderID,int receiverID)
        //{
        //    return Json(this.repository.GetRequests(senderID,receiverID));
        //}

        // POST api/values
        [HttpPost]
        [Authorize]
        public bool Post(Request value)
        {
            return this.repository.DoRequest(value);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        [Authorize]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete]
        [Authorize]
        public bool Delete(int senderID,int receiverID)
        {
            return this.repository.ClearRequests(senderID, receiverID);
        }

        private int GetUserId()
        {
            return int.Parse(
                ((ClaimsIdentity)this.User.Identity).Claims
                .Where(claim => claim.Type == "user_id").First().Value);
        }
    }
}
