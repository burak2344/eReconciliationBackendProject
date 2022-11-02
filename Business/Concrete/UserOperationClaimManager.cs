using Business.Abstract;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
	public class UserOperationClaimManager : IUserOperationClaimService
	{
		private readonly IUserOperationClaimDal _userOperationClaimDal;

		public UserOperationClaimManager(IUserOperationClaimDal userOperationClaimDal)
		{
			_userOperationClaimDal = userOperationClaimDal;
		}
	}
}
