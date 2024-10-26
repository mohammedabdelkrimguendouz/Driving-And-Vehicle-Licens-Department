using DVLD.Global.Classes;
using DVLD_Buisness.Global_Classes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using DVLD_Buisness;
using DVLD_DataAccess;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Sales_BackEnd.Controllers
{
    [Route("api/Auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpPost("Login/{UserName},{Password}",Name ="Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Login(string UserName,string Password)
        {
            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password))
                return BadRequest("Invalide Data : ");

            string PasswordHased = clsCryptography.ComputeHash(Password);

            clsUser User = clsUser.FindByUserNameAndPassword(UserName, PasswordHased);

            if (User == null)
                return Unauthorized("Invalid email or password.");


            var Token = GenerateJwtToken(User.UserID);

            return Ok(new
            {
                token = Token,
                userInfo= User.AllUserInfo,
            });





        }


        [HttpPost("Logout", Name = "Logout")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize]
        public IActionResult Logout()
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized();
            }

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            
            var expiryClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Exp);
            if (expiryClaim == null)
            {
                return StatusCode(StatusCodes.Status409Conflict, "Invalid token");
            }

            var expiryDateUnix = long.Parse(expiryClaim.Value);
            var expiryDate = DateTimeOffset.FromUnixTimeSeconds(expiryDateUnix).UtcDateTime;

            
            clsBlackList blackList = new clsBlackList(new BlackListDTO(-1, token, expiryDate));
            if (!blackList.Save())
                return StatusCode(StatusCodes.Status409Conflict, "Error To Logout");

            return Ok(new { message = "Logged out successfully" });
        }

        [HttpPost("Refresh", Name = "Refresh")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [Authorize]
        public IActionResult Refresh()
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Token is missing.");
            }

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            var expiryClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Exp);
            if (expiryClaim == null)
            {
                return Unauthorized("Invalid token.");
            }

            var expiryDateUnix = long.Parse(expiryClaim.Value);
            var expiryDate = DateTimeOffset.FromUnixTimeSeconds(expiryDateUnix).UtcDateTime;

            if(expiryDate < DateTime.UtcNow)
                return Unauthorized("Token has expired.");

            clsBlackList blackList = new clsBlackList(new BlackListDTO(-1, token, expiryDate));
            if (!blackList.Save())
                return StatusCode(StatusCodes.Status409Conflict, "Error saving to blacklist.");

            var userId = int.Parse(jwtToken.Claims.First(c => c.Type == JwtRegisteredClaimNames.Sub).Value);
            var newToken = GenerateJwtToken(userId);

            clsUser user = clsUser.FindByUserID(userId);

           return Ok(new
           {
               token = newToken,
               userInfo = user,
           });
            
        }

        private string GenerateJwtToken(int UserId)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                   new Claim(JwtRegisteredClaimNames.Sub, UserId.ToString()), // Sub: الموضوع (المستخدم أو الهوية)
                   new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) // معرف التوكن الفريد
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"], // الجهة المُصدِرة للتوكن
                audience: _configuration["Jwt:Audience"], // الجمهور المستهدف للتوكن
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpireMinutes"])), // انتهاء الصلاحية
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token); // تحويل التوكن إلى نص
        }

    }
}
