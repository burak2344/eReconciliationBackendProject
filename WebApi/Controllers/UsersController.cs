using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly IUserService _userService;
		private readonly IAuthService _authService;

		public UsersController(IUserService userService, IAuthService authService)
		{
			_userService = userService;
			_authService = authService;
		}

		[HttpGet("getUserList")]
		public IActionResult GetUserList(int companyId)
		{
			var result = _userService.GetUserList(companyId);
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest(result.Message);
		}

		[HttpPost("updateOperationClaim")]
		public IActionResult UpdateOperationClaim(OperationClaimForUserListDto operationClaim)
		{
			var result = _userService.UpdateOperationClaim(operationClaim);
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest(result.Message);
		}
	}
}
