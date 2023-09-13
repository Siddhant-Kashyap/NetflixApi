using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetflixApi.Model;
using NetflixApi.Services;
using System.Security.Claims;

namespace NetflixApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieListController : ControllerBase
    {
        private readonly MovieListServices _services;
        public MovieListController(MovieListServices movieListServices)
        {
            _services = movieListServices;
            
        }

        [HttpPut("{userId}")]
        [Authorize]
        public async Task<IActionResult> UpdateMovieNames(string userId, [FromBody] List<string> newMovieNames)
        {
            var updated = await _services.UpdateMovieNamesAsync(userId, newMovieNames);

            if (updated)
            {
                return Ok("Movie names updated successfully");
            }
            else
            {
                return NotFound("User not found");
            }
        }
    }
}
