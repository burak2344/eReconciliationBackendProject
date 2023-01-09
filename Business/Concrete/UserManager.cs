using Business.Abstract;
using Business.BusinessAcpects;
using Business.Constans;
using Business.ValidaitonRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Aspects.Caching;
using Core.Aspects.Performance;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
	public class UserManager : IUserService
	{
		private IUserDal _userDal;
		private IUserOperationClaimService _userOperationClaimService;

		public UserManager(IUserDal userDal, IUserOperationClaimService userOperationClaimService)
		{
			_userDal = userDal;
			_userOperationClaimService = userOperationClaimService;
		}

		[CacheRemoveAspect("IUserService.Get")]
		[ValidationAspect(typeof(UserValidator))]
		public void Add(User user)
		{

			_userDal.Add(user);
		}
		[CacheAspect(60)]
		public User GetById(int id)
		{
			return _userDal.Get(u => u.Id == id);
		}
		[CacheAspect(60)]
		public User GetByMail(string email)
		{
			return _userDal.Get(p => p.Email == email);
		}
		[CacheAspect(60)]
		public User GetByMailConfirmValue(string value)
		{
			return _userDal.Get(p => p.MailConfirmValue == value);
		}

		public List<OperationClaim> GetClaims(User user, int companyId)
		{
			return _userDal.GetClaims(user, companyId);
		}
		[SecuredOperation("User.Update,Admin")]
		public IDataResult<List<OperationClaimForUserListDto>> GetOperationClaimListForUser(string value, int companyId)
		{
			return new SuccesDataResult<List<OperationClaimForUserListDto>>(_userDal.GetOperationClaimListForUser(value, companyId));
		}

		[PerformanceAspect(3)]
		[SecuredOperation("User.GetList,Admin")]
		public IDataResult<List<UserCompanyDtoForList>> GetUserList(int companyId)
		{
			return new SuccesDataResult<List<UserCompanyDtoForList>>(_userDal.GetUserList(companyId));
		}

		[PerformanceAspect(3)]
		//[SecuredOperation("User.Update,Admin")]
		[CacheRemoveAspect("IUserService.Get")]
		public void Update(User user)
		{
			_userDal.Update(user);
		}
		[SecuredOperation("User.Update,Admin")]
		public IResult UpdateOperationClaim(OperationClaimForUserListDto operationClaim)
		{
			if (operationClaim.Status == true)
			{
				var result = _userOperationClaimService.GetList(operationClaim.UserId, operationClaim.CompanyId).Data.Where(p => p.OperationClaimId == operationClaim.Id).FirstOrDefault();
				_userOperationClaimService.Delete(result);
			}
			else
			{
				UserOperationClaim userOperationClaim = new UserOperationClaim()
				{
					CompanyId = operationClaim.CompanyId,
					AddedAt = DateTime.Now,
					IsActive = true,
					OperationClaimId = operationClaim.Id,
					UserId = operationClaim.UserId
				};
				_userOperationClaimService.Add(userOperationClaim);
			}

			return new SuccessResult(Messages.UpdatedUserOperationClaim);
		}
	}
}
