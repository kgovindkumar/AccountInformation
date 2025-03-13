using AccountService.Interface;
using AccountService.Model;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AccountService.BusinessLogic
{
    public class AuthenticateManager : IAuthenticationManager
    {
        private readonly Dictionary<string, string> userDetails = 
            new Dictionary<string, string>(){
            {"userName", "password" }};
        private string _key;
        public AuthenticateManager(string key)
        { 
            this._key = key;
        }
        public string Authenticate(UserDetail userDetail)
        {
            if (!userDetails.Any(x => x.Key == userDetail.UserName 
                    && x.Value == userDetail.Password))
                throw new Exception("UnAuthorized user");
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(_key);
            var tokenDesriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, userDetail.UserName)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDesriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
