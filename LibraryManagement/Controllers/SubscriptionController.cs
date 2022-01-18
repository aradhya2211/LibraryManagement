using LibraryManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        [HttpGet]
        public ActionResult GetSubscribers()
        {
            Subscribers[] subscribers = { 
                new() {Name = "Aradhya", MembershipPlan = 6},
                new() {Name = "Saxena", MembershipPlan = 2}
            };

            if (!subscribers.Any())
                return NotFound();

            return Ok(subscribers);
        }
    }
}
