using Business.Abstract;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
	public class UserCompanyManager : IUserCompanyService
	{
		private readonly IUserCompanyDal _userCompanyDal;

		public UserCompanyManager(IUserCompanyDal userCompanyDal)
		{
			_userCompanyDal = userCompanyDal;
		}
	}
}
