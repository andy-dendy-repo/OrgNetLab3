using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OrgNetLab3.Data.Entity;
using OrgNetLab3.Data.Services;
using OrgNetLab3.Token;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace OrgNetLab3.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserRepository _userRepository;
        public AccountController(UserRepository userRepository, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _userRepository.UserId = httpContextAccessor.GetUserId();
        }

        [HttpGet("logout")]
        public IActionResult LogOut()
        {
            Response.Cookies.Delete("token");

            return Redirect("/");
        }

        [HttpGet("current")]
        public async Task<IActionResult> GetCurrentUser([FromServices] IHttpContextAccessor httpContextAccessor)
        {
            var id = httpContextAccessor.GetUserId();

            try
            {
                var user = await _userRepository.GetById(id);

                return Ok(new { id = user.Id, firstName = user.FirstName, lastName = user.LastName});
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        public record Account(string Email, string Password);

        [HttpPost("signIn")]
        public async Task<IActionResult> SignIn([FromForm] Account account)
        {
            var identity = await GetIdentity(account.Email, account.Password);
            if (identity == null)
            {
                return BadRequest(new { errorText = "Invalid username or password." });
            }

            var now = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            Response.Cookies.Append("token", encodedJwt, new CookieOptions
            {
                Secure = true,
            });

            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name
            };

            return Redirect("/home/" + identity.FindFirst(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).Value);
        }

        private async Task<ClaimsIdentity> GetIdentity(string username, string password)
        {
            User person = await _userRepository.GetUserByEmail(username, password);

            if (person != null)
            {
                var claims = new List<Claim>
                {
                    new Claim("Id", person.Id.ToString()),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, person.Role.ToString())
                };

                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);

                return claimsIdentity;
            }

            return null;
        }
    }
}
