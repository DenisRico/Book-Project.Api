using MyProject.Common.Models;
using MyProject.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.EntityFrameworkCore;

namespace MyProject.BL.Services
{
    public class RefreshTokenService : IRefreshTokenService
    {
        /// <summary>
        /// Rewrite here . Should be saving refresh token  in cache  
        /// </summary>
        private readonly List<RefreshToken> _refreshTokens = new List<RefreshToken>();
        public Task Create(RefreshToken refreshToken)
        {
            refreshToken.Id = Guid.NewGuid();

            _refreshTokens.Add(refreshToken);

            return Task.CompletedTask;
        }

        public Task Delete(Guid id)
        {
            _refreshTokens.RemoveAll(r => r.Id == id);
            return Task.CompletedTask;
        }

        public Task DeleteAll(Guid userId)
        {
            _refreshTokens.RemoveAll(r => r.UserId == userId);

            return Task.CompletedTask;
        }

        public Task<RefreshToken> GetByToken(string token)
        {
            RefreshToken refreshToken = _refreshTokens.FirstOrDefault(r => r.Token == token);

            return Task.FromResult(refreshToken);
        }
    }
}
