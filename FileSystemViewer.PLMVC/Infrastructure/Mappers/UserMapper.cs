using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FileSystemViewer.BLL.Interface.Entities;
using FileSystemViewer.BLL.Interface.Interfaces;
using FileSystemViewer.PLMVC.Models.User;

namespace FileSystemViewer.PLMVC.Infrastructure.Mappers
{
	public static class UserMapper
	{
		public static UserViewModel ToMvcUser(this BllUser user, IRoleService roleService)
		{
			var mvcUser = new UserViewModel
			{
				Id = user.Id,
				UserName = user.UserName,
				Email = user.Email,
				Roles = roleService.GetUserRoles(user.Id).Select(r => r.Name)
			};

			return mvcUser;
		}

		public static BllUser ToBllUser(this UserViewModel user, IUserService userService)
		{
			var bllUser = new BllUser
			{
				Id = user.Id,
				UserName = user.UserName,
				Email = user.Email,
				Password = userService.GetOneByPredicate(u=> u.Id == user.Id).Password
			};

			return bllUser;
		}
	}
}