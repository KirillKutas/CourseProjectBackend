using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Common.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using API.Common.Extensions;
using API.Settings;
using Application.Common.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace API.Services
{
    public class TokenService : ITokenService
    {

        private readonly AppConfiguration _appConfiguration;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public TokenService(IOptions<AppConfiguration> appConfiguration, IHttpContextAccessor httpContextAccessor)
        {
            _appConfiguration = appConfiguration.Value;
            _httpContextAccessor = httpContextAccessor;
        }

        private ClaimsPrincipal User => _httpContextAccessor?.HttpContext?.User;

        public int GetUserId() => User.GetId();

        public string AppendSecurityToken()
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appConfiguration.Audience.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    // TODO: setup claims
                    //new Claim(CustomClaimTypes.Id, user.Id.ToString()),
                }),
                Expires = DateTime.UtcNow.AddMinutes(_appConfiguration.Audience.TokenExpiryMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _appConfiguration.Audience.Iss,
                Audience = _appConfiguration.Audience.Aud,
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            // append authorization header
            _httpContextAccessor.HttpContext.Response.Headers.Append("Authorization", "Bearer " + tokenHandler.WriteToken(token));
            return tokenHandler.WriteToken(token);
        }

        public string GetUserName()
        {
            // TODO: hardcoded;

            return "no username";
        }
    }
}
