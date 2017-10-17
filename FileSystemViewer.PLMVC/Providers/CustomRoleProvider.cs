using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using FileSystemViewer.BLL.Interface.Entities;
using FileSystemViewer.BLL.Interface.Interfaces;

namespace FileSystemViewer.PLMVC.Providers
{
	public class CustomRoleProvider : RoleProvider
	{
		public IUserService UserService { get; private set; }
		public IRoleService RoleService { get; private set; }

		public CustomRoleProvider()
		{
			RoleService = (IRoleService)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IRoleService));
			UserService = (IUserService)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IUserService));
		}

		public override string ApplicationName { get; set; }

		public override bool IsUserInRole(string username, string roleName)
		{
			var user = UserService.GetOneByPredicate(u => u.UserName == username);

			if (user == null)
				return false;

			return RoleService.GetUserRoles(user.Id).Any(role => role.Name == roleName);
		}

		public override string[] GetRolesForUser(string username)
		{
			var user = UserService.GetOneByPredicate(u => u.UserName == username);

			if (user == null)
				return null;

			return RoleService.GetUserRoles(user.Id) != null
				? RoleService.GetUserRoles(user.Id).Select(r => r.Name).ToArray()
				: null;
		}

		public override void CreateRole(string roleName)
		{
			var role = new BllRole() { Name = roleName };
			RoleService.Create(role);
		}

		#region NotImplementedMethods

		public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
		{
			throw new NotImplementedException();
		}

		public override bool RoleExists(string roleName)
		{
			throw new NotImplementedException();
		}

		public override void AddUsersToRoles(string[] usernames, string[] roleNames)
		{
			throw new NotImplementedException();
		}

		public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
		{
			throw new NotImplementedException();
		}

		public override string[] GetUsersInRole(string roleName)
		{
			throw new NotImplementedException();
		}

		public override string[] GetAllRoles()
		{
			throw new NotImplementedException();
		}

		public override string[] FindUsersInRole(string roleName, string usernameToMatch)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}