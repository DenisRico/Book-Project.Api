using MyProject.Common.Models.Domain;
using MyProject.Common.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Common.Services
{
    public interface IAuthentificator
    {
        Task<AuthenticatedUserResponse> Auntentificate(User user);
    }
}
