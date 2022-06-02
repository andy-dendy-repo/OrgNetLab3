using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace OrgNetLab3.Token
{
    public class AuthOptions
    {
        public const string ISSUER = "MyAuthServer";
        public const string AUDIENCE = "MyAuthClient"; 
        private const string KEY = "mysupersecret_secretkey!123";   
        public const int LIFETIME = 30; 
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
