using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using UrlChanger.Abstract;
using UrlChanger.Helpers;
using UrlChanger.Models;

namespace UrlChanger.Concrete
{
    public class JWTUserService : IJWTUserService
    {
        private readonly AppSettings _appSettings;
        private readonly DatabaseRepo _context;

        public JWTUserService(IOptions<AppSettings> appSettings, ApplicationContext context)
        {
            _appSettings = appSettings.Value;
            _context = new DatabaseRepo(context);
        }

        public string Authenticate(User model)
        {
            var user = _context.Users.GetRecords().Where(x => x.Login == model.Login && x.Password == model.Password).FirstOrDefault();

            // return null if user not found
            if (user == null) return null;

            // authentication successful so generate jwt token
            var token = generateJwtToken(user);


            return token;
        }

        public User GetById(int id)
        {
            return _context.Users.GetRecord(id);
        }

        // helper methods

        private string generateJwtToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
