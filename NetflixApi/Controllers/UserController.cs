using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using NetflixApi.Model;
using NetflixApi.Services;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MongoWithDotNetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserServices _userService;
        private readonly IConfiguration _configuration;
        private readonly string secretKey;

        public UserController(UserServices userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
            secretKey = configuration.GetValue<string>("ApiSettings:Secret");
        }

        [HttpPost("signup")]
        [AllowAnonymous]
        public async Task<IActionResult> Signup([FromBody] LocalUsers newUser)
        {
            try
            {
                var user = await _userService.SignupAsync(newUser);
                if (user == null)
                {
                    return BadRequest("Username already exists.");
                }

                var token = GenerateJwtToken(user);
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            try
            {
                var user = await _userService.LoginAsync(loginModel.UserName, loginModel.Password);
                if (user == null)
                {
                    return Unauthorized("Invalid username or password.");
                }

                var token = GenerateJwtToken(user);
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        private string GenerateJwtToken(LocalUsers user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
             var key = Encoding.ASCII.GetBytes(secretKey); 
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    // Add other claims as needed (e.g., user roles, additional user data)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);


            return tokenHandler.WriteToken(token);
        }
    }
}
