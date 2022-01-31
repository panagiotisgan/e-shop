using eShop.DataAccess;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace eShop.WebApi.Auth
{
    public class JwtTokenManager : ICustomManager
    {
        private readonly IConfiguration configuration;
        private JwtSecurityTokenHandler _jwtTokenHandler;
        private readonly IUserUnitOfWork _userUnitOfWork;//Tha prepei na fugei kai na mpei kapoios CacheMemory mhxanismos gia na mhn xtupaw ksana to API, 8elw ton xrhsth gia na valw sta CLAIMS ki alla
        private byte[] secretKey;

        public JwtTokenManager(IConfiguration configuration, IUserUnitOfWork userUnitOfWork)
        {
            this.configuration = configuration;
            _jwtTokenHandler = new JwtSecurityTokenHandler();
            secretKey = Encoding.ASCII.GetBytes(configuration.GetValue<string>("JwtTokenKey"));
            _userUnitOfWork = userUnitOfWork;
        }

        public async Task<string> CreateToken(string username)
        {
            var user = await _userUnitOfWork.UserDbRepository.GetUserByUserName(username);

            var jwtSecurityToken = new SecurityTokenDescriptor
            {

               Subject = new ClaimsIdentity(
               new Claim[] {
                   new Claim(ClaimTypes.Name, username),
                   new Claim(ClaimTypes.Role, user.Role),
                   new Claim(ClaimTypes.Email, user.Email)
               }
               //new Claim(ClaimTypes.Role, user.Role)
               ),
                Expires = DateTime.UtcNow.AddMinutes(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = _jwtTokenHandler.CreateToken(jwtSecurityToken);
            return _jwtTokenHandler.WriteToken(token);
        }


        public string GetUserByToken(string token)
        {
            if(token == null)  return null;
            var tk = _jwtTokenHandler.ReadToken(token.Replace("\"",string.Empty)) as JwtSecurityToken;
            var claim = tk.Claims.FirstOrDefault(x => x.Type == "unique_name");
            if(claim != null) return claim.Value;

            return null;
        }

        public void ResetToken()
        {
            throw new System.NotImplementedException();
        }

        public bool VerifyToken(string token)
        {
            SecurityToken securityToken = null;
            if(string.IsNullOrEmpty(token)) return false;

            try
            {
                _jwtTokenHandler.ValidateToken(token.Replace("\"", string.Empty), new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                },
                out securityToken);
            }
            catch (SecurityTokenException)
            {
                return false;
            }
            catch(Exception)
            {
                throw;
            }

            return securityToken!=null;
        }
    }
}
