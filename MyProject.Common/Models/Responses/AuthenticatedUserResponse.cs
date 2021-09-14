using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Common.Models.Responses
{
    public class AuthenticatedUserResponse
    {
        public string AccesToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
