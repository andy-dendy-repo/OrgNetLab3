using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace OrgNetLab3.Token
{
    public static class TokenHelper
    {
        public static string GetUserId(this IHttpContextAccessor controller)
        {
            JwtSecurityTokenHandler token_handler = new JwtSecurityTokenHandler();

            var tokenValidationParams = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                ValidateLifetime = false
            };

            try
            {
                string bearer = controller.HttpContext.Request.Cookies["token"];

                List<Claim> claims = token_handler.ValidateToken(bearer, tokenValidationParams, out SecurityToken token).Claims.ToList();

                Claim userClaim = claims.FirstOrDefault(x => x.Type == "Id");

                return userClaim.Value;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public static string GetUserRole(this ControllerBase controller)
        {
            JwtSecurityTokenHandler token_handler = new JwtSecurityTokenHandler();

            var tokenValidationParams = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                ValidateLifetime = false
            };

            string bearer = controller.HttpContext.Request.Cookies["token"];

            List<Claim> claims = token_handler.ValidateToken(bearer, tokenValidationParams, out SecurityToken token).Claims.ToList();

            Claim userClaim = claims.FirstOrDefault(x => x.Type == ClaimsIdentity.DefaultRoleClaimType);

            if (userClaim != null)
            {
                return userClaim.Value;
            }
            else
            {
                throw new Exception("Invalid AccessToken. UserId claim not found");
            }
        }
    }
}
