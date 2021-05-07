using System;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;
using System.Collections.Immutable;

using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

using CommentApi.Options;


namespace CommentApi.Services
{

    public interface IAuthenticationService
    {
        string GenerateAccessToken(string username, Claim[] claims, DateTime now);
    }

    public class AuthenticationService  : IAuthenticationService
    {
        private readonly JwtTokenConfiguration _jwtTokenConfig;
        private readonly byte[] _secret;

        public AuthenticationService(JwtTokenConfiguration jwtTokenConfig)
        {
            _jwtTokenConfig = jwtTokenConfig;
            _secret = Encoding.ASCII.GetBytes(jwtTokenConfig.Secret);
        }

        public string GenerateAccessToken(string username, Claim[] claims, DateTime now)
        {
            var jwtToken = new JwtSecurityToken(
                _jwtTokenConfig.Issuer,
                _jwtTokenConfig.Audience,
                claims,
                expires: now.AddMinutes(_jwtTokenConfig.AccessTokenExpiration),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(_secret), SecurityAlgorithms.HmacSha256Signature));

            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return accessToken;
        }
    }
}