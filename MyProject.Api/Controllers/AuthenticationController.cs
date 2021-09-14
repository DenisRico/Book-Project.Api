using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyProject.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MyProject.Common.Models.ClientRequest;
using MyProject.Common.Models.Responses;
using MyProject.Common.Services;
using MyProject.Common.Models.Domain;
using MyProject.BL.TokenValidators;

namespace MyProject.Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AuthenticationController : ControllerBase
    {
		private readonly IConfiguration _configuration;
		private readonly IUserService _userService;
		private readonly IPasswordHasherService _passwordHasherService;
		private readonly IAuthentificator _authentificator;
		private readonly RefreshTokenValidator _refreshTokenValidator;
		private readonly IRefreshTokenService _refreshTokenService;

		public AuthenticationController(IConfiguration configuration,
			IUserService userService,
			IPasswordHasherService passwordHasherService, IAuthentificator authentificator, RefreshTokenValidator refreshTokenValidator, IRefreshTokenService refreshTokenService)
        {
			_configuration = configuration;
			_userService = userService;
			_passwordHasherService = passwordHasherService;
			_authentificator = authentificator;
			_refreshTokenValidator = refreshTokenValidator;
			_refreshTokenService = refreshTokenService;
		}


		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
		{
			if (!ModelState.IsValid)
			{
				return BadRequestModelState();
			}

			if (registerRequest.Password != registerRequest.ConfirmPassword)
			{
				return BadRequest(new ErrorResponse("Password does not match confirm password"));
			}

			bool ecxistingUser =await _userService.UserExisting(registerRequest.UserName, registerRequest.Email);

            if (ecxistingUser==true)
            {
				return Conflict(new ErrorResponse("Email or UserName already exists"));
			}


			string passwordHash = _passwordHasherService.HashPassword(registerRequest.Password);

			var id = await _userService.Create(registerRequest.UserName, registerRequest.Email, passwordHash);
			
			return Ok(id);
		}

		[HttpPost("login")]
		public async Task<ActionResult> Login([FromBody] LoginRequest loginRequest)
		{
			if (!ModelState.IsValid)
			{
				IEnumerable<string> errorMessages = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
				return BadRequest(new ErrorResponse(errorMessages));
			}

			User user = await _userService.GetUserByName(loginRequest.UserName);
			if (user == null)
			{
				return Unauthorized();
			}
			bool isCorectPassword = _passwordHasherService.VirifyPassword(loginRequest.Password, user.PasswordHash);
			if (!isCorectPassword)
			{
				return Unauthorized();
			}

			AuthenticatedUserResponse response = await _authentificator.Auntentificate(user);

			return Ok(response);
		}

		[HttpPost("refresh")]
		public async Task<ActionResult> Refresh([FromBody] RefreshRequest refreshRequest)
		{
			if (!ModelState.IsValid)
			{
				IEnumerable<string> errorMessages = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
				return BadRequest(new ErrorResponse(errorMessages));
			}
			bool isValidRefreshToken = _refreshTokenValidator.Validate(refreshRequest.RefreshToken);
			if (!isValidRefreshToken)
			{
				return BadRequest(new ErrorResponse("Invalid refresh token."));
			}
			RefreshToken refreshTokenDTO = await _refreshTokenService.GetByToken(refreshRequest.RefreshToken);
			if (refreshTokenDTO == null)
			{
				return NotFound(new ErrorResponse("Invalid refresh token"));
			}
			User user = await _userService.GetById(refreshTokenDTO.UserId);
			if (user == null)
			{
				return NotFound(new ErrorResponse("User not found"));
			}
			AuthenticatedUserResponse response = await _authentificator.Auntentificate(user);

			return Ok(response);
		}

		[Authorize]
		[HttpDelete("logout")]
		public async Task<IActionResult> Logout()
		{
			string rawUserId = HttpContext.User.FindFirstValue("id");

			if (!Guid.TryParse(rawUserId, out Guid userId))
			{
				return Unauthorized();
			}

			await _refreshTokenService.DeleteAll(userId);

			return NoContent();
		}

		private IActionResult BadRequestModelState()
		{
			IEnumerable<string> errorMessages = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
			return BadRequest(new ErrorResponse(errorMessages));
		}

		///// <summary>
		///// Gets JWT token for user
		///// </summary>
		///// <returns>authorized user's info with token </returns>
		//[ProducesResponseType(StatusCodes.Status200OK)] 
		//[ProducesResponseType(StatusCodes.Status403Forbidden)]
		//[ProducesResponseType(StatusCodes.Status404NotFound)]
		//[ProducesResponseType(StatusCodes.Status400BadRequest)]
		//[HttpPost]
		//[AllowAnonymous]
		//[Route("generatetoken")]
		//public IActionResult GenerateToken([FromBody] AuthIn model)
		//{
		//	try
		//	{
		//		List<Person> people = new List<Person>()
		//		{
		//			 new Person {Login="admin@gmail.com", Password="12345", Role = "admin" },
		//	         new Person { Login="qwerty@gmail.com", Password="55555", Role = "user" }
		//		};

		//		var raw = people.FirstOrDefault(item => item.Login == model.Login);

		//		if (raw == null)
		//			return NotFound();



		//		var tokens =  GenerateTokens(raw);

		//		return Ok(tokens);
		//	}
		//	catch (Exception ex)
		//	{
		//		return StatusCode(StatusCodes.Status500InternalServerError, ex);
		//	}
		//}

		//private string GenerateTokens(Person userProfile)
		//{
		//	var tokenHandler = new JwtSecurityTokenHandler();

		//	var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
		//	var claims = GetClaims(userProfile);

		//	var tokenLifetime = TimeSpan.Parse(_configuration["Jwt:TokenLifetime"]);
		//	var tokenExpires = DateTime.UtcNow.Add(tokenLifetime);
		//	var refreshTokenExpires = tokenExpires.Add(tokenLifetime);
		//	var tokenDescriptor = new SecurityTokenDescriptor
		//	{
		//		Subject = new ClaimsIdentity(claims),
		//		Expires = tokenExpires,
		//		Audience = _configuration["Jwt:Audience"],
		//		Issuer = _configuration["Jwt:Issuer"],
		//		SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
		//	};
		//	var securitytoken = tokenHandler.CreateToken(tokenDescriptor);
		//	var token = tokenHandler.WriteToken(securitytoken);

		//	return token;
		//}

		//private static IEnumerable<Claim> GetClaims(Person userProfile)
		//{
		//	var claims = new List<Claim>
		//			{
		//				new Claim("login", userProfile.Login.ToString()),
		//				new Claim("password", userProfile.Password.ToString()),
		//					new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
		//			};

		//	return claims;
		//}

	}
}
