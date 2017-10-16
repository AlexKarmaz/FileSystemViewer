using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileSystemViewer.DAL.Interface.DTO;
using FileSystemViewer.ORM.Entities;

namespace FileSystemViewer.DAL.Mappers
{
	public static class DalRoleMapper
	{
		public static DalRole ToDalRole(this Role ormRole)
		{
			if (ormRole == null)
				return null;
			var dalRole = new DalRole()
			{
				Id = ormRole.RoleId,
				Name = ormRole.Name,
			};
			return dalRole;
		}

		public static Role ToOrmRole(this DalRole dalRole)
		{
			if (dalRole == null)
				return null;
			var ormRole = new Role()
			{
				RoleId = dalRole.Id,
				Name = dalRole.Name,
			};
			return ormRole;
		}
	}
}
