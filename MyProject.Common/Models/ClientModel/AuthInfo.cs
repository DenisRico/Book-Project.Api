using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Common.Models.ClientModel
{
	public class AuthInfo
	{
		public Guid UserId { get; set; }
		public string Email { get; set; }
		public string AuthToken { get; set; }
	}
}
