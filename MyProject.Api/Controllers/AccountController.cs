using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyProject.Common.Models;
using MyProject.Common.Models.ClientModel;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace MyProject.Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AccountController : ControllerBase
    {
		private readonly IConfiguration configuration;

		public AccountController(IConfiguration configuration)
        {
			this.configuration = configuration;
		}

		/// <summary>
		/// Gets JWT token for user
		/// </summary>
		/// <returns>authorized user's info with token </returns>
		[ProducesResponseType(StatusCodes.Status200OK)] 
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[HttpPost]
		[AllowAnonymous]
		[Route("generatetoken")]
		public IActionResult GenerateToken([FromBody] AuthIn model)
		{
			try
			{
				List<Person> people = new List<Person>()
				{
					 new Person {Login="admin@gmail.com", Password="12345", Role = "admin" },
			         new Person { Login="qwerty@gmail.com", Password="55555", Role = "user" }
				};

				var raw = people.FirstOrDefault(item => item.Login == model.Login);

				if (raw == null)
					return NotFound();

				

				var tokens =  GenerateTokens(raw);
				
				return Ok(tokens);
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, ex);
			}
		}

		private string GenerateTokens(Person userProfile)
		{
			var tokenHandler = new JwtSecurityTokenHandler();

			var key = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]);
			var claims = GetClaims(userProfile);

			var tokenLifetime = TimeSpan.Parse(configuration["Jwt:TokenLifetime"]);
			var tokenExpires = DateTime.UtcNow.Add(tokenLifetime);
			var refreshTokenExpires = tokenExpires.Add(tokenLifetime);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(claims),
				Expires = tokenExpires,
				Audience = configuration["Jwt:Audience"],
				Issuer = configuration["Jwt:Issuer"],
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};
			var securitytoken = tokenHandler.CreateToken(tokenDescriptor);
			var token = tokenHandler.WriteToken(securitytoken);
			
			return token;
		}

		private static IEnumerable<Claim> GetClaims(Person userProfile)
		{
			var claims = new List<Claim>
					{
						new Claim("login", userProfile.Login.ToString()),
						new Claim("password", userProfile.Password.ToString()),
							new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
					};

			return claims;
		}
	}
}
