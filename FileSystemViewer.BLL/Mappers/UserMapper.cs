using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileSystemViewer.BLL.Interface.Entities;
using FileSystemViewer.DAL.Interface.DTO;

namespace FileSystemViewer.BLL.Mappers
{
	public static class UserMapper
	{
		public static DalUser ToDalUser(this BllUser bllUser)
		{
			if (bllUser == null)
				return null;
			var dalUser = new DalUser()
			{
				Id = bllUser.Id,
				UserName = bllUser.UserName,
				Password = bllUser.Password,
				Email = bllUser.Email
			};
			return dalUser;
		}

		public static BllUser ToBllUser(this DalUser dalUser)
		{
			if (dalUser == null)
				return null;
			var bllUser = new BllUser()
			{
				Id = dalUser.Id,
				UserName = dalUser.UserName,
				Password = dalUser.Password,
				Email = dalUser.Email
			};
			return bllUser;
		}
	}
}
