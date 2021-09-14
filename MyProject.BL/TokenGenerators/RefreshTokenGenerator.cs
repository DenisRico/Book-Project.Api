using MyProject.Common.Models;
using MyProject.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.BL.TokenGenerators
{
    public class RefreshTokenGenerator : IRefreshTokenGenerator
    {
        private readonly AuthentificationConfiguration _configuration;
        private readonly ITokenGenerator _tokenGenerator;

        public RefreshTokenGenerator(AuthentificationConfiguration configuration, ITokenGenerator tokenGenerator)
        {
            _configuration = configuration;
            _tokenGenerator = tokenGenerator;
        }
        public string GenerateToken()
        {


            return _tokenGenerator.GenerateToken(
               _configuration.RefreshTokenSecret,
               _configuration.Issuer,
               _configuration.Audience,
               _configuration.RefreshTokenExpirationMinutes
               );
        }
    }
}
