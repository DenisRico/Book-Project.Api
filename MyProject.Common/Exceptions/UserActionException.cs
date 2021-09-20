using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Common.Exceptions
{
	public class UserActionException : Exception
	{
		public UserActionException() { }

		public UserActionException(string message) : base(message)
		{ }

		public UserActionException(string message, Exception ex) : base(message, ex)
		{ }
	}
}
