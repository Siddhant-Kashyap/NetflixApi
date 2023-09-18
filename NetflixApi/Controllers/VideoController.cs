using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetflixApi.Model;
using NetflixApi.Services;

namespace NetflixApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        private readonly VideoServices _videoServices;
        public VideoController(VideoServices videoServices)
        {
            _videoServices = videoServices;
        }


        [HttpPost]
        public async Task<IActionResult> PlayVideo(VideoIdReq request)
        {
            var videoId = request.videoId;
            var video = await _videoServices.GetVideoByIdAsync(videoId);

            if (video != null && video.Data != null)
            {
                // Set the response content type for video playback (e.g., video/mp4)
                Response.ContentType = "video/mp4";

                // Get the binary data as a byte array
                byte[] videoDataBytes = video.Data.AsByteArray;

                // Write the video data to the response stream
                await Response.Body.WriteAsync(videoDataBytes, 0, videoDataBytes.Length);

                return new ContentResult();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
