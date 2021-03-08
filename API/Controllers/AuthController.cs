using API.Dtos;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiExplorerSettings(GroupName = "v1")]
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IWebHostEnvironment _currentEnvironment;
        public AuthController(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            this._currentEnvironment = env;
        }

        public IConfiguration Configuration { get; }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            if (loginDto.UserName == "admin" && loginDto.Password == "Heslo1234.")
            {
                var secretKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(Configuration.GetSection("JwtSecret").Value));
                var signInCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var tokenOptions = new JwtSecurityToken(
                    issuer: this._currentEnvironment.IsDevelopment() ? "https://localhost:5001" : "https://localhost:5900",
                    audience: this._currentEnvironment.IsDevelopment() ? "https://localhost:5001" : "https://localhost:5900",
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: signInCredentials);

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                return Ok(new { Token = tokenString });
            }

            return Unauthorized();
        }
    }
}
