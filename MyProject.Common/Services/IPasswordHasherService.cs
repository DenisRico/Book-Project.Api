using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Common.Services
{
    public interface IPasswordHasherService
    {
        string HashPassword(string password);
        bool VirifyPassword(string password, string passwordHash);
    }
}
