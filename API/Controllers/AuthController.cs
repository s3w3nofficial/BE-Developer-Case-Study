using API.Dtos;
using API.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<ApplicationUser> _userManager;
        public AuthController(
            IConfiguration configuration, 
            IWebHostEnvironment env,
            UserManager<ApplicationUser> userManager)
        {
            Configuration = configuration;
            this._currentEnvironment = env;
            this._userManager = userManager;
        }

        public IConfiguration Configuration { get; }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await this._userManager.FindByEmailAsync(loginDto.UserName);
            if (user != null && await this._userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                var secretKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(Configuration.GetSection("JwtSecret").Value));
                var signInCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim>();

                if (await this._userManager.IsInRoleAsync(user, "Admin"))
                {
                    claims.Add(new Claim(ClaimTypes.Name, loginDto.UserName));
                    claims.Add(new Claim(ClaimTypes.Role, "Admin"));
                }

                var tokenOptions = new JwtSecurityToken(
                    issuer: this._currentEnvironment.IsDevelopment() ? "https://localhost:5001" : "https://localhost:5900",
                    audience: this._currentEnvironment.IsDevelopment() ? "https://localhost:5001" : "https://localhost:5900",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: signInCredentials);

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                return Ok(new { Token = tokenString });
            }

            return Unauthorized();
        }
    }
}
