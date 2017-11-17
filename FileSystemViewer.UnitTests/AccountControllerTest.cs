using System;
using System.Text;
using System.Collections.Generic;
using System.Web;
using FileSystemViewer.BLL.Interface.Interfaces;
using FileSystemViewer.PLMVC.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web.Mvc;
using System.Web.Routing;
using FileSystemViewer.BLL.Interface.Entities;
using FileSystemViewer.BLL.Services;
using FileSystemViewer.PLMVC.Infrastructure.Mappers;
using FileSystemViewer.PLMVC.Models.User;

namespace FileSystemViewer.UnitTests
{
	
	[TestClass]
	public class AccountControllerTest
	{
		private Mock<IUserService> userServiceMock;
		private Mock<IRoleService> roleServiceMock;
		private Mock<HttpRequestBase> HttpRequestMock;
		private Mock<HttpContextBase> HttpContextBaseMock;

		[TestInitialize]
		public void TestInitialize()
		{
			userServiceMock = new Mock<IUserService>();
			roleServiceMock = new Mock<IRoleService>();
			HttpRequestMock = new Mock<HttpRequestBase>();
			HttpContextBaseMock = new Mock<HttpContextBase>();

			HttpContextBaseMock.SetupGet(x => x.Request).Returns(HttpRequestMock.Object);
		}

		[TestMethod]
		public void Login_ViewResult_NotNull()
		{
			AccountController controller = new AccountController(userServiceMock.Object, roleServiceMock.Object);
			ViewResult result = controller.Login() as ViewResult;

			Assert.IsNotNull(result);
		}

		[TestMethod]
		public void Login_ViewResult_EqualLoginCshtml()
		{
			AccountController controller = new AccountController(userServiceMock.Object, roleServiceMock.Object);
			ViewResult result = controller.Login() as ViewResult;

			Assert.AreEqual("Login", result.ViewName);
		}

		[TestMethod]
		public void Login_PostActionWithValidateModelError_ReturnCorrectModel()
		{
			AccountController controller = new AccountController(userServiceMock.Object, roleServiceMock.Object);
			LogViewModel model = new LogViewModel();

			controller.ModelState.AddModelError("Name", "Invalid username or password");
			ViewResult result = controller.Login(model) as ViewResult;

			Assert.IsNotNull(result.Model);
			Assert.AreEqual(model, result.Model);
		}

		[TestMethod]
		public void Register_ViewResult_NotNull()
		{
			AccountController controller = new AccountController(userServiceMock.Object, roleServiceMock.Object);
			ViewResult result = controller.Register() as ViewResult;

			Assert.IsNotNull(result);
		}

		[TestMethod]
		public void Register_ViewResult_EqualRegisterCshtml()
		{
			AccountController controller = new AccountController(userServiceMock.Object, roleServiceMock.Object);
			ViewResult result = controller.Register() as ViewResult;

			Assert.AreEqual("Register", result.ViewName);
		}

		[TestMethod]
		public void Register_PostActionWithValidateModelError_ReturnCorrectModel()
		{
			AccountController controller = new AccountController(userServiceMock.Object, roleServiceMock.Object);
			RegisterViewModel model = new RegisterViewModel();

			controller.ModelState.AddModelError("Email", "User with this email already registered.");
			ViewResult result = controller.Register(model) as ViewResult;

			Assert.IsNotNull(result.Model);
			Assert.AreEqual(model, result.Model);
		}

		[TestMethod]
		public void Register_PostActionWithAlreadyRegisteredEmail_AddModelError()
		{
			string expectedErrorMesage = "User with this email already registered.";
			RegisterViewModel model = new RegisterViewModel
			{
				UserName = "Test",
			    UserId = 1,
				UserEmail = "test@mail.ru",
				UserPassword = "123"
			};

			userServiceMock.Setup(a => a.GetOneByPredicate(It.IsAny<System.Linq.Expressions.Expression<System.Func<BllUser,bool>>>())).Returns(new BllUser());

			AccountController controller = new AccountController(userServiceMock.Object, roleServiceMock.Object);

			ViewResult result = controller.Register(model) as ViewResult;

			Assert.IsNotNull(result.Model);
			Assert.IsTrue(!result.ViewData.ModelState.IsValid);
			Assert.AreEqual(expectedErrorMesage, controller.ModelState[""].Errors[0].ErrorMessage);
		}

		[TestMethod]
		public void GetAllUsers_PositiveTest_ReturnCorrectModel()
		{
			var listOfUsers = new List<BllUser>();

			listOfUsers.Add(new BllUser
			{
				Id = 1,
				UserName = "Test",
				Password = "123",
				Email = "test@mail.ru"
			});
			userServiceMock.Setup(a => a.GetAll()).Returns(listOfUsers);

			AccountController controller = new AccountController(userServiceMock.Object, roleServiceMock.Object);
			controller.ControllerContext = new ControllerContext(HttpContextBaseMock.Object, new RouteData(), controller);

			ViewResult result = controller.GetAllUsers() as ViewResult;

			Assert.IsNotNull(result.Model);
		}

		[TestMethod]
		public void UpdateUserRole_UserHaveAdminRights_DeleteAdminRightsFromTheUser()
		{
			List<BllRole> roles = new List<BllRole>();
			roles.Add(new BllRole(){Name = "Admin"});
			roles.Add(new BllRole(){Name = "User"});

			var bllUser = new BllUser()
			{
				Id = 1,
				UserName = "Test",
				Email = "Test",
			};

			roleServiceMock.Setup(a => a.GetUserRoles(1)).Returns(roles);
			roleServiceMock.Setup(a => a.GetAll()).Returns(roles);
			userServiceMock.Setup(a => a.GetOneByPredicate(It.IsAny<System.Linq.Expressions.Expression<System.Func<BllUser, bool>>>())).Returns(bllUser);

			AccountController controller = new AccountController(userServiceMock.Object, roleServiceMock.Object);
			controller.ControllerContext = new ControllerContext(HttpContextBaseMock.Object, new RouteData(), controller);
			ViewResult result = controller.UpdateUserRole("Test","Admin") as ViewResult;

			roleServiceMock.Verify(a=> a.RemoveRoleFromUser(1,It.IsAny<int>()));
			Assert.IsNotNull(result.Model);
		}

		[TestMethod]
		public void UpdateUserRole_UserHaveNoAdminRights_GiveAdminRightsToTheUser()
		{
			List<BllRole> roles = new List<BllRole>();
			roles.Add(new BllRole() { Name = "Admin" });
			roles.Add(new BllRole() { Name = "User" });

			List<BllRole> userRoles = new List<BllRole>();
			userRoles.Add(new BllRole() { Name = "User" });

			var bllUser = new BllUser()
			{
				Id = 1,
				UserName = "Test",
				Email = "Test",
			};

			roleServiceMock.Setup(a => a.GetUserRoles(1)).Returns(userRoles);
			roleServiceMock.Setup(a => a.GetAll()).Returns(roles);
			userServiceMock.Setup(a => a.GetOneByPredicate(It.IsAny<System.Linq.Expressions.Expression<System.Func<BllUser, bool>>>())).Returns(bllUser);

			AccountController controller = new AccountController(userServiceMock.Object, roleServiceMock.Object);
			controller.ControllerContext = new ControllerContext(HttpContextBaseMock.Object, new RouteData(), controller);
			ViewResult result = controller.UpdateUserRole("Test", "Admin") as ViewResult;

			roleServiceMock.Verify(a => a.AddRoleToUser(1, It.IsAny<int>()));
			Assert.IsNotNull(result.Model);
		}
	}
}
