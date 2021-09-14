using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Common.Services
{
    public interface IRefreshTokenGenerator
    {
        string GenerateToken();
    }
}
