
using Core.CEntities.Dtos;
using Core.CEntities.Models;
using Core.CExtensions;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Core.CSecurity
{
    public class JwtHelper : ITokenHelper
    {
        public IConfiguration Configuration { get; }
        private JwtTokenOptions _jwtTokenOptions;
        private DateTime _accessTokenExpiration;
        public JwtHelper(IConfiguration configuration)
        {
            Configuration = configuration;
            _jwtTokenOptions = Configuration.GetSection("JwtTokenOptions").Get<JwtTokenOptions>(); // Microsoft.Extensions.Configuration.Binder

        }
        public LoginResponseDto GenerateJwtToken(User user, string userType, List<RoleClaim> claims)
        {
            _accessTokenExpiration = DateTime.Now.AddMinutes(_jwtTokenOptions.AccessTokenExpiration);
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_jwtTokenOptions.SecurityKey);
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
            var jwt = CreateJwtSecurityToken(_jwtTokenOptions, user, signingCredentials, claims);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);
            var refreshToken = GenerateRefreshToken("0.0.0.1");

            return new LoginResponseDto
            {
                Token = token,
                RefreshToken = refreshToken,
                UserType = userType,
            };
        }

        public string GenerateRefreshToken(string ipAddress)
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public JwtSecurityToken CreateJwtSecurityToken(JwtTokenOptions jwtTokenOptions, User user,
            SigningCredentials signingCredentials, List<RoleClaim> claims)
        {
            var jwt = new JwtSecurityToken(
                issuer: jwtTokenOptions.Issuer,
                audience: jwtTokenOptions.Audience,
                expires: _accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: SetClaims(user, claims),
                signingCredentials: signingCredentials
            );
            return jwt;
        }

        private IEnumerable<Claim> SetClaims(User user, List<RoleClaim> roleClaims)
        {
            var claims = new List<Claim>();
            claims.AddNameIdentifier(user.Id.ToString());
            claims.AddEmail(user.Email);
            claims.AddName($"{user.FirstName} {user.LastName}");
            claims.AddRoles(roleClaims.Select(c => c.ClaimName).ToArray());

            return claims;
        }
    }
}
