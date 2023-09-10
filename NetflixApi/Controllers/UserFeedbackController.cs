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
    public class UserFeedbackController : ControllerBase
    {
       // private readonly UserFeedbackService _UserFeedbackService;

        //public UserFeedbackController(UserFeedbackService UserFeedbackService)
        //{
        //    _UserFeedbackService = UserFeedbackService;
        //}

        //[HttpGet]
        //public ActionResult<IEnumerable<UserFeedback>> GetUserFeedback(string userId)
        //{
        //    // In a real application, you would typically get the user ID from authentication.
        //    // For this example, we'll use a placeholder user ID.
        //    string userIdToFilterBy = "user123"; // Replace with the actual user's ID.

        //    var userFeedback = _UserFeedbackService.GetUserFeedback(userIdToFilterBy);

        //    return Ok(userFeedback);
        //}

        //[HttpPost]
        //public ActionResult CreateFeedback([FromBody] UserFeedback feedback)
        //{
        //    // In a real application, you would typically get the user ID from authentication.
        //    // For this example, we'll use a placeholder user ID.
        //    string userId = "user123"; // Replace with the actual user's ID.

        //    // Set the user ID for the feedback (assuming it's a property of the Feedback model).
        //    feedback.UserId = userId;

        //    // Call the service to create the feedback.
        //    _UserFeedbackService.CreateFeedback(feedback);

        //    return Ok("Feedback created successfully.");
        //}

        private readonly UserFeedbackService _userFeedbackService;
        public UserFeedbackController(UserFeedbackService userFeedbackService) =>
            _userFeedbackService = userFeedbackService;


        [HttpGet("id")]
        public async Task<ActionResult<UserFeedback>> Get(string id)
        {
            var userFeedback = await _userFeedbackService.GetAsync(id);
            if (userFeedback is null)
            {
                return NotFound();
            }
            return Ok(userFeedback);
        }
        [HttpPost]
        public async Task<IActionResult> Post(UserFeedback userFeedback)
        {
            await _userFeedbackService.CreateAsync(userFeedback);

            return CreatedAtAction(nameof(Get), new { id = userFeedback.Id }, userFeedback);
        }

        [HttpPut("id")]
        public async Task<IActionResult> Update(string UserId, UserFeedback updatedUserFeedback)
        {
            var userFeedback = await _userFeedbackService.GetAsync(UserId);


            if (userFeedback is null)
            {
                return NotFound();
            }


            await _userFeedbackService.UpdateAsync(UserId, updatedUserFeedback);

            return NoContent();
        }








    }
}
