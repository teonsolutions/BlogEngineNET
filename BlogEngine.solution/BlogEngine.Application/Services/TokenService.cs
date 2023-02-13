using BlogEngine.Application.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlogEngine.Application.Services
{
    public class TokenService: ITokenService
    {
        private readonly IConfiguration configuration;
        private readonly TokenValidationParameters tokenValidationParameters;

        public TokenService(IConfiguration configuration, TokenValidationParameters tokenValidationParameters)
        {
            this.configuration = configuration;
            this.tokenValidationParameters = tokenValidationParameters;
        }

        public string GenerateJwtLogin(int userID, string userLogin, string fullName, Guid rolGuid)
        {
            var key = Encoding.ASCII.GetBytes(this.configuration["Jwt:SecureToken"]);
            ClaimsIdentity claims = new ClaimsIdentity();
            claims.AddClaim(new Claim("UserID", userID.ToString()));
            claims.AddClaim(new Claim("UserLogin", userLogin));
            claims.AddClaim(new Claim("FullName", fullName));
            claims.AddClaim(new Claim("RolGuid", rolGuid.ToString()));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddMinutes(45),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var createdToken = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(createdToken).ToString();
        }
    }
}
