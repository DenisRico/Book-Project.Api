using MyProject.Common.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Common.Repositories
{
    public interface IUserRepository
    {
        Task<User> Create(User user);
        Task<User> GetByEmail(string email);
        Task<User> GetByUserName(string userName);
        Task<User> GetById(Guid userId);
    }
}
