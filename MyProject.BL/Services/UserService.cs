using MyProject.Common.Models.Domain;
using MyProject.Common.Repositories;
using MyProject.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.BL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Guid> Create(string userName, string email, string passwordHash)
        {
            User user = new User() { 
            Email = email,
            UserName = userName,
            PasswordHash = passwordHash};

            var obj = await _userRepository.Create(user);
            return obj.Id;
        }

        public async Task<bool> UserExisting(string userName, string email)
        {
            User ecxistingUserByEmail = await _userRepository.GetByEmail(email);
            if (ecxistingUserByEmail != null)
            {
                return true;
            }

            User ecxistingUserByUserName = await _userRepository.GetByUserName(userName);
            if (ecxistingUserByUserName != null)
            {
                return true;
            }

            return false;
        }

        public async Task<User> GetUserByName(string userName)
        {
            User ecxistingUserByUserName = await _userRepository.GetByUserName(userName);

            return ecxistingUserByUserName;
        }

        public async Task<User> GetById(Guid userId)
        {
            User user = await _userRepository.GetById(userId);

            return user;
        }
    }
}
