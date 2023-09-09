using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using NetflixApi.Model;
using NetflixApi.Services;

namespace NetflixApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class SubscriptionController : ControllerBase
    {
        private readonly SubscriptionServices _subscriptionService;
        public SubscriptionController(SubscriptionServices subscriptionServices)
        {
            _subscriptionService = subscriptionServices;
        }
        [HttpGet("{userId}")]
        public IActionResult GetSubscriptionsByUserId(string userId)
        {
            var subscriptions = _subscriptionService.GetSubscriptionsByUserId(userId);
            return Ok(subscriptions);
        }

        [HttpPost]
        public IActionResult CreateSubscription(Subscription subscription)
        {
            // Perform payment processing and validation here
            // You may need to integrate with a payment gateway

            _subscriptionService.CreateSubscription(subscription);

            return Ok(subscription);
        }
    }
}
