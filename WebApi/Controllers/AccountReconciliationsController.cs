using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountReconciliationsController : ControllerBase
	{
		private readonly IAccountReconciliationService _accountReconciliationService;

		public AccountReconciliationsController(IAccountReconciliationService accountReconciliationService)
		{
			_accountReconciliationService = accountReconciliationService;
		}

		[HttpPost("add")]
		public IActionResult Add(AccountReconciliation accountReconciliation)
		{
			var result = _accountReconciliationService.Add(accountReconciliation);
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest(result.Message);
		}

		[HttpPost("update")]
		public IActionResult Update(AccountReconciliation accountReconciliation)
		{
			var result = _accountReconciliationService.Update(accountReconciliation);
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest(result.Message);
		}

		[HttpGet("delete")]
		public IActionResult Delete(AccountReconciliation accountReconciliation)
		{
			var result = _accountReconciliationService.Delete(accountReconciliation);
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest(result.Message);
		}

		[HttpGet("getById")]
		public IActionResult GetById(int id)
		{
			var result = _accountReconciliationService.GetById(id);
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest(result.Message);
		}

		[HttpGet("getList")]
		public IActionResult GetList(int companyId)
		{
			var result = _accountReconciliationService.GetList(companyId);
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest(result.Message);
		}
	}
}
