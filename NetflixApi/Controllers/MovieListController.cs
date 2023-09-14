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
        [HttpGet("{userId}")]
        public async Task<ActionResult<MovieList>> GetUserMovieList(string userId)
        {
            try
            {
                var movieList = await _services.GetUserMovieListAsync(userId);

                if (movieList != null)
                {
                    return Ok(movieList);
                }
                else
                {
                    return NotFound($"Movie list not found for user with ID: {userId}");
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions as needed, log, and return an error response.
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
