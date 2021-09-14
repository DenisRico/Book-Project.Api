using Microsoft.EntityFrameworkCore;
using MyProject.Common.Models.Domain;
using MyProject.Common.Repositories;
using MyProject.Dal.SqlContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Dal.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MyProjectContext _dbContext; 
        public UserRepository(MyProjectContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> Create(User user)
        {
            user.Id = Guid.NewGuid();

            var context = _dbContext;

            await context.Users.AddAsync(user);

            await context.SaveChangesAsync().ConfigureAwait(false);

            return user;
        }

        public async Task<User> GetByEmail(string email)
        {
            var context = _dbContext;

            var entity = await context.Users.FirstOrDefaultAsync(i => i.Email.Equals(email));

            return entity;
        }

        public async Task<User> GetByUserName(string userName)
        {
            var context = _dbContext;

            var entity = await context.Users.FirstOrDefaultAsync(i => i.UserName.Equals(userName));

            return entity;
        }

        public async Task<User> GetById(Guid userId)
        {
            var context = _dbContext;

            var entity = await context.Users.FirstOrDefaultAsync(i => i.Id.Equals(userId));

            return entity;
        }
    }
}
