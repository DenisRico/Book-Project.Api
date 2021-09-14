using AutoMapper.Configuration;
using MyProject.Common.Models;
using MyProject.Common.Models.Domain;
using MyProject.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.BL.TokenGenerators
{
    public class AccesTokenGenerator : IAccesTokenGenerator
    {
        private readonly AuthentificationConfiguration _configuration;
        private readonly ITokenGenerator _tokenGenerator;
        public AccesTokenGenerator(AuthentificationConfiguration configuration, ITokenGenerator tokenGenerator)
        {
            _configuration = configuration;
            _tokenGenerator = tokenGenerator;
        }
        public string GenerateToken(User user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim("id", user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserName)
            };



            return _tokenGenerator.GenerateToken(
                _configuration.AccesTokenSecret,
                _configuration.Issuer,
                _configuration.Audience,
                _configuration.AccesTokenExpirationMinutes,
                claims
                );
        }
    }
}
