 using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Security.JWT;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
	public interface IAuthService
	{
		IDataResult<UserCompanyDto> Register(UserForRegister userForRegister, string password,Company company);
		IDataResult<List<UserReletionshipDto>> RegisterSecondAccount(UserForRegister userForRegister, string password, int companyId, int adminUserId);
		IDataResult<User> Login(UserForLogin userForLogin);
		IDataResult<User> GetById(int id);
		IDataResult<User> GetByEmail(string email);
		IDataResult<User> GetByMailConfirmValue(string value);
		IDataResult<UserCompany> GetCompany(int userId);
		IResult ChangePassword(User user);
		IResult UserExists(string email);
		IResult Update(User user);
		IResult CompanyExists(Company company);
		IResult SendConfirmEmailAgain(User user);
		IDataResult<AccessToken> CreateAccessToken(User user, int companyId);
		IResult SendForgotPasswordEmail(User user, string value);
	}
}
