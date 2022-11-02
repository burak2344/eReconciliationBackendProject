using Business.Abstract;
using Business.Constans;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
	public class AccountReconciliationManager : IAccountReconciliationService
	{
		private readonly IAccountReconciliationDal _accountReconciliationDal;

		public AccountReconciliationManager(IAccountReconciliationDal accountReconciliationDal)
		{
			_accountReconciliationDal = accountReconciliationDal;
		}

		public IResult Add(AccountReconciliation accountReconciliation)
		{
			
			_accountReconciliationDal.Add(accountReconciliation);
			return new SuccessResult(Messages.AddedAccountReconciliation);
		}


		public IResult Delete(AccountReconciliation accountReconciliation)
		{
			_accountReconciliationDal.Delete(accountReconciliation);
			return new SuccessResult(Messages.DeletedAccountReconciliation);
		}

		public IDataResult<AccountReconciliation> GetById(int id)
		{
			return new SuccesDataResult<AccountReconciliation>(_accountReconciliationDal.Get(p => p.Id == id));
		}

		public IDataResult<List<AccountReconciliation>> GetList(int companyId)
		{
			return new SuccesDataResult<List<AccountReconciliation>>(_accountReconciliationDal.GetList(p => p.CompanyId == companyId));
		}

		public IResult Update(AccountReconciliation accountReconciliation)
		{
			_accountReconciliationDal.Update(accountReconciliation);
			return new SuccessResult(Messages.UpdatedAccountReconciliation);
		}

	}
}
