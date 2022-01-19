using LibraryManagement.Models;
using LibraryManagement.Services;
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
        SubscriberServices subscriberServices = new SubscriberServices();
        [HttpGet]
        public async Task<ActionResult> GetSubscribers()
        {
            try
            {
                var result = await subscriberServices.GetAllSubscribers();
                if (!result.Any())
                    return NotFound();
                else
                    return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
                throw;
            }
        }
        [HttpGet("/Name/{Name}")]
        public async Task<ActionResult> GetubSubscriberByName(String Name)
        {
            try
            {
                var results = await subscriberServices.GetSubscribersByName(Name);
                if (!results.Any())
                    return NotFound();
                return Ok(results);
            }
            catch (Exception)
            {
                return BadRequest();
                throw;
            }
        }
        [HttpGet("/id")]
        public async Task<ActionResult> GetSubscriberById(Subscribers subscriber)
        {
            try
            {
                var result = await subscriberServices.GetSubscribersByBsonId(subscriber);
                if (result != null)
                    return NotFound();
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
                throw;
            }
        }
        [HttpPost]
        public async Task<ActionResult> AddNewSubscriber(Subscribers NewSubscriber)
        {
            try
            {
                var result = await subscriberServices.AddNewSubscriber(NewSubscriber);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
                throw;
            }
        }
        [HttpPut]
        public async Task<ActionResult> UpdateSubscriber(Subscribers UpdatedSubscriber)
        {
            try
            {
                var result = await subscriberServices.UpdateSubscriber(UpdatedSubscriber);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
                throw;
            }
        }
        [HttpDelete]
        public async Task<ActionResult> DeleletSubscriber(Subscribers subscriber)
        {
            try
            {
                var result = await subscriberServices.DeleteSubscriber(subscriber);
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest();
                throw;
            }
        }
    }
}
