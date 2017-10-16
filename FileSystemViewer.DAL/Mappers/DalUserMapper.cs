using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileSystemViewer.DAL.Interface.DTO;
using FileSystemViewer.ORM.Entities;

namespace FileSystemViewer.DAL.Mappers
{
	public static class DalUserMapper
	{
		public static DalUser ToDalUser(this User ormUser)
		{
			if (ormUser == null)
				return null;
			var dalUser = new DalUser()
			{
				Id = ormUser.UserId,
				UserName = ormUser.UserName,
				Password = ormUser.Password,
				Email = ormUser.Email
			};
			return dalUser;
		}

		public static User ToOrmUser(this DalUser dalUser)
		{
			if (dalUser == null)
				return null;
			var ormUser = new User()
			{
				UserId = dalUser.Id,
				UserName = dalUser.UserName,
				Password = dalUser.Password,
				Email = dalUser.Email
			};
			return ormUser;
		}
	}
}
