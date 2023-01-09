using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
	public interface IUserService
	{
		List<OperationClaim> GetClaims(User user, int companyId);
		void Add(User user);
		void Update(User user);
		IResult UpdateOperationClaim(OperationClaimForUserListDto operationClaim);
		User GetByMail(string email);
		User GetById(int id);
		User GetByMailConfirmValue(string value);
		IDataResult<List<UserCompanyDtoForList>> GetUserList(int companyId);
		IDataResult<List<OperationClaimForUserListDto>> GetOperationClaimListForUser(string value, int companyId);
	}
}
