using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UrlChanger.Abstract;
using UrlChanger.Models;

namespace UrlChanger.Concrete
{
    public class JWTManagerRepository : IJWTManagerRepository
    {
        private readonly IConfiguration configuration;
        private ApplicationContext applicationContext;
        public JWTManagerRepository(IConfiguration config, ApplicationContext context)
        {
            configuration = config;
            applicationContext = context;

        }
        public Token Authenticate(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(configuration["JWT:key"]);
            
            var claim = GetIdentity(user.Login, user.Password);
            if (claim == null)
            {
                return null;
            }
            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: configuration["JWT:Issuer"],
                    audience: configuration["JWT:Audience"],
                    notBefore: now,
                    claims: claim.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(10)),
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            Token token = new Token { TokenName = encodedJwt, RefreshToken = "30" };

            //var tokenDescriptor = new SecurityTokenDescriptor
            //{
            //    Subject = new ClaimsIdentity(new Claim[]
            //    {
            //        new Claim(type: ClaimTypes.Name, value: user.Login)
            //    }),
            //    Expires = DateTime.UtcNow.AddMinutes(10),
            //    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            //};
            //var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            //Token token = new Token { TokenName = tokenHandler.WriteToken(securityToken) };

            return token;
        }

        private ClaimsIdentity GetIdentity(string username, string password)
        {
            User person = applicationContext.Users.FirstOrDefault(x => x.Login == username && x.Password == password);
            if (person != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, person.Login)

                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            // если пользователя не найдено
            return null;
        }
    }
}
