using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OrgApi.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OrgApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly OrganizationContext _context;
        public readonly IConfiguration _config;

        public UserController(OrganizationContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }


        [HttpPost]
        [Route("signup")]
        public async Task<IActionResult> StoreUserDetails([FromBody] OrgUser user)
        {
            await _context.OrgUsers.AddAsync(user);
            await _context.SaveChangesAsync();
            return Ok(user);
        }

        [HttpPost, Route("userLogin")]
        public IActionResult LoginUser([FromBody] OrgUser user)
        {
            if (isUserExist(user.Username))
            {
                if (isUserAuthenticated(user.Username, user.Password))
                {
                    try
                    {
                        var claims = new[]
                        {
                new Claim(ClaimTypes.NameIdentifier,user.Username)
            };
                        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:TOken").Value));
                        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha512Signature);
                        var tokenDescriptor = new SecurityTokenDescriptor
                        {

                            Subject = new ClaimsIdentity(claims),
                            Expires = DateTime.Now.AddDays(30),
                            SigningCredentials = signinCredentials
                        };

                        var tokenHandler = new JwtSecurityTokenHandler();

                        var token = tokenHandler.CreateToken(tokenDescriptor);

                        //return Ok();
                        return Ok(new { token = tokenHandler.WriteToken(token) });
                    }
                    catch (Exception e)
                    {
                        return BadRequest(e.StackTrace);
                    }
                }
                else
                {
                    return Unauthorized("Password is incorrect!");
                }
            }
            else
            {
                return NotFound("User Doesn't Exists");
            }
        }


        private bool isUserExist(string username)
        {
            bool result = _context.OrgUsers.Any(a => a.Username == username);
            return result;
        }


        private bool isUserAuthenticated(string username, string password)
        {
            string pwd = _context.OrgUsers.Where(w => w.Username == username).Select(s => s.Password).First().ToString();

            if (pwd.Equals(password))
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        //[HttpPost]
        //[Route("userLogin")]

        //public IActionResult Login([FromBody] OrgUser user)
        //{
        //    return Ok(user);
        //}


    }
}

