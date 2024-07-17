using KitapService.Helpers;
using KitapService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace KitapService.Controllers
{
    public class Credentials
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        private readonly DataContext _context;

        public AuthController(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public IActionResult Register(User user)
        {
            try
            {
                string passwordHash = CreatePasswordHash(user.Password);
                _context.Users.FromSqlRaw($"user_Registration @p0,@p1,@p2", new string[] { user.Username, passwordHash, "1" }).ToList();
                BaseResponse<User> response = new BaseResponse<User>()
                {
                    success = true,
                    data = null,
                };
                _context.SaveChanges();
                return Ok(response);
            }
            catch (Exception)
            {

                return BadRequest();
            }
          
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public IActionResult Login(Credentials credentials)
        {
            var user = _context.Users.FirstOrDefault(x => x.Username == credentials.Username);
            
            if (user == null)
            {
                return BadRequest();
            }
            bool isPasswordCorrect = VerifyPasswordHash(credentials.Password, user.Password);
            if (isPasswordCorrect != true)
            {
                return BadRequest();
            }
            string token = CreateToken(user);
            var userInfo = new
            {
                username = user.Username,
                token,
                user.Id
            };
            var response = new BaseResponse<Object>()
            {
                success = true,
                data = userInfo,
            };
            return Ok(response);
        }

        private string CreateToken(User user)
        {
            var issuer = _configuration["AppSettings:Issuer"];
            var audience = _configuration["AppSettings:Audience"];
            var key = Encoding.ASCII.GetBytes(_configuration["AppSettings:Key"]!);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Sid, user.Id.ToString()), new Claim(ClaimTypes.Name, user.Username) }),
                Expires = DateTime.UtcNow.AddYears(1),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);
            var stringToken = tokenHandler.WriteToken(token);

            return stringToken;
        }

        private string CreatePasswordHash(string password)
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
            return hashedPassword;
        }

        private bool VerifyPasswordHash(string password, string passwordHash)
        {
            bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(password, passwordHash);
            return isPasswordCorrect;
        }

    }
}
