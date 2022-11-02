using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
	public interface ICompanyService
	{
		IResult Add(Company company);
		IResult Update(Company company);
		IDataResult<Company> GetById(int id);
		IResult AddCompanyAndUserCompany(CompanyDto companyDto);
		IDataResult<List<Company>> GetList();

		IResult CompanyExists(Company company);
		IDataResult<UserCompany> GetCompany(int userId);
		IResult UserCompanyAdd(int userId, int companyId);
	}
}
