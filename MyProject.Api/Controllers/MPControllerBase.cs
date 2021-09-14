using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MyProject.Common.Models.ClientModel;

namespace MyProject.Api.Controllers
{
	/// <summary>
	/// A generic class for getting information about a request
	/// </summary>
	public class MPControllerBase : ControllerBase
    {
		protected Guid? GetCurrentUserId()
		{
			var user = HttpContext.User;
			if (user == null)
				return null;
			var raw = user.FindFirstValue("id");
			if (Guid.TryParse(raw, out var id))
			{
				return id;
			}
			return null;
		}

		protected string GetCurrentUserEmail()
		{
			var user = HttpContext.User;
			if (user == null)
				return null;
			var email = user.FindFirstValue(ClaimTypes.Email);
			if (email!=null)
			{
				return email;
			}
			return null;
		}

		protected AuthInfo GetAuthInfo()
		{
			var userId = GetCurrentUserId().Value;
			var email = GetCurrentUserEmail();
			var auth = Request.Headers["Authorization"].ToString();
			return new AuthInfo
			{
				UserId = userId,
				AuthToken = auth,
				Email = email
			};
		}
	}
}
