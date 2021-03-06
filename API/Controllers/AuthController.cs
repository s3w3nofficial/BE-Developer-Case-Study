using API.Dtos;
using API.Errors;
using API.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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

        /// <summary>
        /// Retrieves jwt token
        /// </summary>
        /// <param name="loginDto"></param>
        /// <returns>Token</returns>
        /// <response code="200">Returns when successfully logged in</response>
        /// <response code="403">If user does not exist or provided incorect password</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await this._userManager.FindByEmailAsync(loginDto.UserName);
            if (user != null && await this._userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                var secretKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(Configuration.GetSection("JwtSecret").Value));
                var signInCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim>() 
                {
                    new Claim(ClaimTypes.Name, loginDto.UserName),
                };

                if (await this._userManager.IsInRoleAsync(user, "Admin"))
                {
                    claims.Add(new Claim(ClaimTypes.Role, "Admin"));
                }

                var tokenOptions = new JwtSecurityToken(
                    issuer: this._currentEnvironment.IsDevelopment() ? "https://localhost:5001" : "http://localhost:5900",
                    audience: this._currentEnvironment.IsDevelopment() ? "https://localhost:5001" : "http://localhost:5900",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: signInCredentials);

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                return Ok(new { Token = tokenString });
            }

            return Unauthorized();
        }

        /// <summary>
        /// Retrieves jwt token and creates new user
        /// </summary>
        /// <param name="registerDto"></param>
        /// <returns>Token</returns>
        /// <response code="200">Returns when successfully registered</response>
        /// <response code="400">If user exists</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            if (await this._userManager.FindByEmailAsync(registerDto.UserName) != null)
                return BadRequest(new ErrorDetails
                {
                    StatusCode = "auth-1",
                    Message = "user already exists"
                });

            await this._userManager.CreateAsync(new ApplicationUser
            {
                Email = registerDto.UserName,
                UserName = registerDto.UserName,
            }, registerDto.Password);

            var secretKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(Configuration.GetSection("JwtSecret").Value));
            var signInCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokenOptions = new JwtSecurityToken(
                issuer: this._currentEnvironment.IsDevelopment() ? "https://localhost:5001" : "http://localhost:5900",
                audience: this._currentEnvironment.IsDevelopment() ? "https://localhost:5001" : "http://localhost:5900",
                claims: new List<Claim>() 
                {
                    new Claim(ClaimTypes.Name, registerDto.UserName)
                },
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: signInCredentials);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return Ok(new { Token = tokenString });
        }
    }
}
