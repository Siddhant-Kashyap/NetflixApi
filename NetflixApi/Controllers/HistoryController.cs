using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using NetflixApi.Model;
using NetflixApi.Services;

namespace NetflixApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryController : ControllerBase
    {

        private readonly HistoryServices _historyService;

        public HistoryController(HistoryServices historyService)
        {
            _historyService = historyService;
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<History>>> GetUserHistory(string userId)
        {
            return await _historyService.GetUserHistory(userId);
        }

        [HttpPost]
        public async Task<ActionResult<History>> AddHistory(History history)
        {
            if (string.IsNullOrEmpty(history.ContentId) || string.IsNullOrEmpty(history.Title))
            {
                // Return a bad request response with an error message
                return BadRequest("ContentId and Title must not be null or empty.");
            }
            return await _historyService.AddHistory(history);
        }
    }
}
