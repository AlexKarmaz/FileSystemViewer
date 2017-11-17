using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using FileSystemViewer.BLL.Interface.Interfaces;
using FileSystemViewer.PLMVC.Infrastructure.Mappers;
using FileSystemViewer.PLMVC.Models.User;
using FileSystemViewer.PLMVC.Providers;

namespace FileSystemViewer.PLMVC.Controllers
{
	public class AccountController : Controller
	{
		private readonly IUserService userService;
		private readonly IRoleService roleService;

		public AccountController(IUserService userService, IRoleService roleService)
		{
			this.userService = userService;
			this.roleService = roleService;
		}

		[HttpGet]
		public ActionResult Login()
		{
			return View("Login");
		}

		[HttpGet]
		public ActionResult Register()
		{
			return View("Register");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Login(LogViewModel model)
		{
			if (ModelState.IsValid)
			{
				if (new CustomMembershipProvider().ValidateUser(model.UserName, model.Password))
				{
					FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
					return RedirectToAction("GetDrives", "Drive");
				}
				else
					ModelState.AddModelError("", "Invalid username or password");
			}
			return View(model);
		}

		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public ActionResult Register(RegisterViewModel viewModel)
		{
			if (userService.GetOneByPredicate(u => u.Email == viewModel.UserEmail) != null)
			{
				ModelState.AddModelError("", "User with this email already registered.");
				return View(viewModel);
			}

			if (userService.GetOneByPredicate(u => u.UserName == viewModel.UserName) != null)
			{
				ModelState.AddModelError("", "User with this name already registered.");
				return View(viewModel);
			}

			if (ModelState.IsValid)
			{
				var membershipUser = ((CustomMembershipProvider)Membership.Provider)
				.CreateUser(viewModel.UserEmail, viewModel.UserName, viewModel.UserPassword);
				if (membershipUser != null)
				{
					FormsAuthentication.SetAuthCookie(viewModel.UserName, false);
					return RedirectToAction("GetDrives", "Drive");
				}
			}

			return View(viewModel);
		}

		public ActionResult Logoff()
		{
			FormsAuthentication.SignOut();
			return RedirectToAction("Login", "Account");
		}

		[Authorize(Roles = "admin")]
		[HttpGet]
		public ActionResult GetAllUsers()
		{
			var users = userService.GetAll()
				.OrderBy(u => u.UserName)
				.Select(u => u.ToMvcUser(roleService));

			if (Request.IsAjaxRequest())
				return PartialView(users);
			return View(users);
		}

		[Authorize(Roles = "admin")]
		[HttpPost]
		public ActionResult UpdateUserRole(string userName, string roleName)
		{
			var user = userService.GetOneByPredicate(u => u.UserName == userName).ToMvcUser(roleService);
			int userId = user.Id;

			if (user.Roles.Contains(roleName))
			{
				roleService.RemoveRoleFromUser(userId, roleService.GetAll().FirstOrDefault(r => r.Name == roleName).Id);
			}
			else
			{
				roleService.AddRoleToUser(userId, roleService.GetAll().FirstOrDefault(r => r.Name == roleName).Id);
			}

			if (Request.IsAjaxRequest())
				return PartialView(user);
			return View(user);
		}
	}
}