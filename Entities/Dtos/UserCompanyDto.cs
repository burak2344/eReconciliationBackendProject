﻿using Core.Entities.Concrete;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
	public class UserCompanyDto : User, IDto
	{
		public int CompanyId { get; set; }
	}
}
