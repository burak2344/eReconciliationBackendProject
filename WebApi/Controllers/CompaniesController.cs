using Business.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CompaniesController : Controller
	{
		private readonly ICompanyService _companyService;

		public CompaniesController(ICompanyService companyService)
		{
			_companyService = companyService;
		}

		[HttpGet("getCompany")]
		public IActionResult GetById(int companyId)
		{
			var result = _companyService.GetById(companyId);
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest(result.Message);
		}

		[HttpPost("addCompanyAndUserCompany")]
		public IActionResult AddCompanyAndUserCompany(CompanyDto companyDto)
		{
			var result = _companyService.AddCompanyAndUserCompany(companyDto);
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest(result.Message);
		}

		[HttpPost("addCompany")]
		public IActionResult AddCompany(Company company, int userId)
		{
			var result = _companyService.Add(company);
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest(result.Message);
		}

		[HttpGet("getcompanylist")]
		public IActionResult GetCompanyList()
		{
			var result = _companyService.GetList();
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest(result.Message);
		}
		[HttpPost("updateCompany")]
		public IActionResult UpdateCompany(Company company)
		{
			var result = _companyService.Update(company);
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest(result.Message);
		}
	}
}
