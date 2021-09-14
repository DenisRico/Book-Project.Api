using MyProject.Common.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Common.Services
{
    public interface IUserService
    {
        Task<bool> UserExisting(string userName, string email);
        Task<Guid> Create(string userName, string email, string passwordHash);
        Task<User> GetUserByName(string userName);
        Task<User> GetById(Guid userId);
    }
}
