using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using API.Entities;
using API.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace API.Services
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key; //Symmetric key is basically a single key used to encrypt and decrypt the information. And this does not leave
                                                    //the server.
        public TokenService(IConfiguration config)
        {
            //var tokenKey = config["TokenKey"];
            
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        }

        public string CreateToken(AppUser user)
        {
            var claims = new List<Claim>                                                                                                         
            {                                                             
                new Claim(JwtRegisteredClaimNames.NameId, user.Username)
            };

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature); //Credentials which will be used to sign a token.

            var tokenDescriptor = new SecurityTokenDescriptor //This SecurityTokenDescriptor class is used to construct JWT token and it holds its configuration such 
            {                                                 //the expire time, subjects and signing credentials through with the token will be signed and later on 
                Subject = new ClaimsIdentity(claims),         //verified when the user requests the API.
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}