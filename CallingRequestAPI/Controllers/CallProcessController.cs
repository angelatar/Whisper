using System.Linq;
using System.Security.Claims;
using CallingRequestAPI.Businness;
using CallingRequestAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CallingRequestAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/CallProcess")]
    public class CallProcessController : Controller
    {
        private readonly CallProcess repository;

        public CallProcessController(CallProcess repo)
        {
            this.repository = repo;
        }

        // GET api/values
        [HttpGet]
        [Authorize]
        public void Get()
        {
        }

        // GET api/values/5
        [HttpGet]
        [Authorize]
        public IActionResult Get(int senderID, int receiverID)
        {
            return Json(this.repository.GetCall(senderID, receiverID));
        }

        // POST api/values
        [HttpPost]
        [Authorize]
        public bool Post([FromBody]Call value)
        {
            return this.repository.SendCall(value);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        [Authorize]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{senderID}&{receiverID}")]
        public bool Delete(int senderID, int receiverID)
        {
            return this.repository.ClearBuffer(senderID, receiverID);
        }

        private int GetUserId()
        {
            return int.Parse(
                ((ClaimsIdentity)this.User.Identity).Claims
                .Where(claim => claim.Type == "user_id").First().Value);
        }
    }
}