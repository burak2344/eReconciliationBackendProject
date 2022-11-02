using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Results.Concrete;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IAuthService _authService;

		public AuthController(IAuthService authService)
		{
			_authService = authService;
		}

		[HttpPost("register")]
		public IActionResult Register(UserAndCompanyRegisterDto userAndCompanyRegister)
		{
			var userExists = _authService.UserExists(userAndCompanyRegister.UserForRegister.Email);
			if (!userExists.Success)
			{
				return BadRequest(userExists.Message);
			}
			var companyExists = _authService.CompanyExists(userAndCompanyRegister.Company);
			if (!companyExists.Success)
			{
				return BadRequest(userExists.Message);
			}

			var registerResult = _authService.Register(userAndCompanyRegister.UserForRegister, userAndCompanyRegister.UserForRegister.Password, userAndCompanyRegister.Company);
			var result = _authService.CreateAccessToken(registerResult.Data, registerResult.Data.CompanyId);
			if (registerResult.Success)
			{
				return Ok(result.Data);
			}
			return BadRequest(registerResult.Message);
		}

		[HttpPost("registerSecondAccount")]
		public IActionResult RegisterSecondAccount(UserForRegisterToSecondAccountDto userForRegister)
		{
			var userExists = _authService.UserExists(userForRegister.Email);
			if (!userExists.Success)
			{
				return BadRequest(userExists.Message);
			}

			var registerResult = _authService.RegisterSecondAccount(userForRegister, userForRegister.Password, userForRegister.CompanyId);
			if (registerResult.Success)
			{
				return Ok(registerResult);
			}

			return BadRequest(registerResult.Message);
		}
	

			[HttpPost("login")]
		public IActionResult Login(UserForLogin userForLogin)
		{
			var userToLogin = _authService.Login(userForLogin);
			if (!userToLogin.Success)
			{
				return BadRequest(userToLogin.Message);
			}

			if (userToLogin.Data.IsActive)
			{
				if (userToLogin.Data.MailConfirm)
				{
					var userCompany = _authService.GetCompany(userToLogin.Data.Id).Data;
					var result = _authService.CreateAccessToken(userToLogin.Data, userCompany.CompanyId);
					if (result.Success)
					{
						return Ok(result);
					}
					return BadRequest(result);
				}
				return BadRequest("Gelen onay mailini cevaplamalısınız. Mail adresinizi onaylamadan sisteme giriş yapamazsınız!");

			}
			return BadRequest("Kullanıcı pasif durumda. Aktif etmek için yöneticinize danışın");

		}

		[HttpGet("confirmuser")]
		public IActionResult ConfirmUser(string value)
		{
			var user = _authService.GetByMailConfirmValue(value).Data;
			if (user.MailConfirm)
			{
				return BadRequest("Kullanıcı maili zaten onaylı. Aynı maili tekrar onaylayamazsınız!");
			}

			user.MailConfirm = true;
			user.MailConfirmDate = DateTime.Now;
			var result = _authService.Update(user);
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest(result.Message);
		}

		[HttpGet("sendConfirmEmail")]
		public IActionResult SendConfirmEmail(string email)
		{
			var user = _authService.GetByEmail(email).Data;

			if (user == null)
			{
				return BadRequest("Kullanıcı bulunamadı!");
			}

			if (user.MailConfirm)
			{
				return BadRequest("Kullanıcının maili onaylı!");
			}

			var result = _authService.SendConfirmEmailAgain(user);
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest(result.Message);
		} 
	}
}
