using MyProject.BL.TokenGenerators;
using MyProject.Common.Models;
using MyProject.Common.Models.Domain;
using MyProject.Common.Models.Responses;
using MyProject.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.BL
{
    public class Authentificator : IAuthentificator
    {
        private readonly IAccesTokenGenerator _accesTokenGenerator;
        private readonly IRefreshTokenGenerator _refreshTokenGenerator;
        private readonly IRefreshTokenService _refreshTokenService;

        public Authentificator(IAccesTokenGenerator accesTokenGenerator, IRefreshTokenGenerator refreshTokenGenerator, IRefreshTokenService refreshTokenService)
        {
            _accesTokenGenerator = accesTokenGenerator;
            _refreshTokenGenerator = refreshTokenGenerator;
            _refreshTokenService = refreshTokenService;
        }
        public async Task<AuthenticatedUserResponse> Auntentificate(User user)
        {
            string accesToken = _accesTokenGenerator.GenerateToken(user);
            string refreshToken = _refreshTokenGenerator.GenerateToken();

            RefreshToken refreshTokenDTO = new RefreshToken()
            {
                Token = refreshToken,
                UserId = user.Id
            };

            await _refreshTokenService.Create(refreshTokenDTO);

            return new AuthenticatedUserResponse()
            {
                AccesToken = accesToken,
                RefreshToken = refreshToken
            };
        }
    }
}
