using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using FileSystemViewer.BLL.Interface.Interfaces;
using FileSystemViewer.PLMVC.Controllers;
using FileSystemViewer.PLMVC.Models.Directory;
using FileSystemViewer.PLMVC.Models.File;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FileSystemViewer.UnitTests
{
	[TestClass]
	public class DirectoryControllerTest
	{
		private Mock<IUserService> userServiceMock;
		private Mock<IRoleService> roleServiceMock;
		private Mock<HttpRequestBase> HttpRequestMock;
		private Mock<HttpContextBase> HttpContextBaseMock;
		private Mock<IDirectoryService> directoryServiceMock;
		private Mock<IFileService> fileServiceMock;
		private Mock<ISearchService> searchServiceMock;

		[TestInitialize]
		public void TestInitialize()
		{
			userServiceMock = new Mock<IUserService>();
			roleServiceMock = new Mock<IRoleService>();
			directoryServiceMock = new Mock<IDirectoryService>();
			fileServiceMock = new Mock<IFileService>();
			searchServiceMock = new Mock<ISearchService>();
			HttpRequestMock = new Mock<HttpRequestBase>();
			HttpContextBaseMock = new Mock<HttpContextBase>();

			HttpContextBaseMock.SetupGet(x => x.Request).Returns(HttpRequestMock.Object);
		}

		[TestMethod]
		public void GetAllDirectory_EmptyPath_RedirectToAnotherUrl()
		{
			string expected = "/Drive/GetDrives/";
			DirectoryController controller = new DirectoryController(directoryServiceMock.Object, fileServiceMock.Object, searchServiceMock.Object);

			RedirectResult result = controller.GetAllDirectory("") as RedirectResult;

			Assert.IsNotNull(result);
			Assert.AreEqual(expected, result.Url);
		}

		[TestMethod]
		public void GetAllDirectory_ThrowException_ReturnJsonResult()
		{
			directoryServiceMock.Setup(a => a.GetAllDirectories(It.IsAny<String>())).Throws<UnauthorizedAccessException>();
			DirectoryController controller = new DirectoryController(directoryServiceMock.Object, fileServiceMock.Object, searchServiceMock.Object);

			JsonResult result = controller.GetAllDirectory("D/") as JsonResult;

			Assert.IsNotNull(result);
			Assert.AreEqual("{ Status = NotAcceptable }", result.Data.ToString());
		}

		[TestMethod]
		public void GetAllDirectory_PositiveTest_ReturnCorrectModel()
		{
			DirectoryController controller = new DirectoryController(directoryServiceMock.Object, fileServiceMock.Object, searchServiceMock.Object);
			controller.ControllerContext = new ControllerContext(HttpContextBaseMock.Object, new RouteData(), controller);

			ViewResult result = controller.GetAllDirectory("D/data/") as ViewResult;

			Assert.IsNotNull(result);
			Assert.AreEqual("D\\\\data\\\\", result.ViewData["LastPath"]);
		}

		[TestMethod]
		public void DeleteDirectory_EmptyPath_RedirectToAnotherUrl()
		{
			string expected = "/Drive/GetDrives/";
			DirectoryController controller = new DirectoryController(directoryServiceMock.Object, fileServiceMock.Object, searchServiceMock.Object);

			RedirectResult result = controller.DeleteDirectory("") as RedirectResult;

			Assert.IsNotNull(result);
			Assert.AreEqual(expected, result.Url);
		}

		[TestMethod]
		public void DeleteDirectory_ThrowException_ReturnJsonResult()
		{
			directoryServiceMock.Setup(a => a.GetAllDirectories(It.IsAny<String>())).Throws<UnauthorizedAccessException>();
			DirectoryController controller = new DirectoryController(directoryServiceMock.Object, fileServiceMock.Object, searchServiceMock.Object);

			JsonResult result = controller.DeleteDirectory("D/") as JsonResult;

			Assert.IsNotNull(result);
			Assert.AreEqual("{ Status = NotAcceptable }", result.Data.ToString());
		}

		[TestMethod]
		public void DeleteDirectory_PositiveTest_ReturnCorrectModel()
		{
			directoryServiceMock.Setup(a => a.GetParrent(It.IsAny<string>())).Returns("D/");
			DirectoryController controller = new DirectoryController(directoryServiceMock.Object, fileServiceMock.Object, searchServiceMock.Object);
			controller.ControllerContext = new ControllerContext(HttpContextBaseMock.Object, new RouteData(), controller);

			ViewResult result = controller.DeleteDirectory("D/data/") as ViewResult;

			Assert.IsNotNull(result);
			Assert.AreEqual("D\\\\", result.ViewData["LastPath"]);
		}

		[TestMethod]
		public void DeleteDirectory_PositiveTest_DeleteDirectory()
		{
			directoryServiceMock.Setup(a => a.GetParrent(It.IsAny<string>())).Returns("D/");
			DirectoryController controller = new DirectoryController(directoryServiceMock.Object, fileServiceMock.Object, searchServiceMock.Object);
			controller.ControllerContext = new ControllerContext(HttpContextBaseMock.Object, new RouteData(), controller);

			ViewResult result = controller.DeleteDirectory("D/data/") as ViewResult;

			Assert.IsNotNull(result);
			directoryServiceMock.Verify(a => a.DeleteDirectory("D:/data/"));
		}

		[TestMethod]
		public void DeleteFile_EmptyPath_RedirectToAnotherUrl()
		{
			string expected = "/Drive/GetDrives/";
			DirectoryController controller = new DirectoryController(directoryServiceMock.Object, fileServiceMock.Object, searchServiceMock.Object);

			RedirectResult result = controller.DeleteFile("") as RedirectResult;

			Assert.IsNotNull(result);
			Assert.AreEqual(expected, result.Url);
		}

		[TestMethod]
		public void DeleteFile_ThrowException_ReturnJsonResult()
		{
			directoryServiceMock.Setup(a => a.GetAllDirectories(It.IsAny<String>())).Throws<UnauthorizedAccessException>();
			DirectoryController controller = new DirectoryController(directoryServiceMock.Object, fileServiceMock.Object, searchServiceMock.Object);

			JsonResult result = controller.DeleteFile("D/") as JsonResult;

			Assert.IsNotNull(result);
			Assert.AreEqual("{ Status = NotAcceptable }", result.Data.ToString());
		}

		[TestMethod]
		public void DeleteFile_PositiveTest_ReturnCorrectModel()
		{
			fileServiceMock.Setup(a => a.GetParrent(It.IsAny<string>())).Returns("D/");
			DirectoryController controller = new DirectoryController(directoryServiceMock.Object, fileServiceMock.Object, searchServiceMock.Object);
			controller.ControllerContext = new ControllerContext(HttpContextBaseMock.Object, new RouteData(), controller);

			ViewResult result = controller.DeleteFile("D/data/") as ViewResult;

			Assert.IsNotNull(result);
			Assert.AreEqual("D\\\\", result.ViewData["LastPath"]);
		}

		[TestMethod]
		public void DeleteFile_PositiveTest_DeleteDirectory()
		{
			fileServiceMock.Setup(a => a.GetParrent(It.IsAny<string>())).Returns("D/");
			DirectoryController controller = new DirectoryController(directoryServiceMock.Object, fileServiceMock.Object, searchServiceMock.Object);
			controller.ControllerContext = new ControllerContext(HttpContextBaseMock.Object, new RouteData(), controller);

			ViewResult result = controller.DeleteFile("D/data/") as ViewResult;

			Assert.IsNotNull(result);
			fileServiceMock.Verify(a => a.DeleteFile("D:/data"));
		}
		//

		[TestMethod]
		public void CreateFolder_EmptyPath_ReturnStatusCode400()
		{
			DirectoryController controller = new DirectoryController(directoryServiceMock.Object, fileServiceMock.Object, searchServiceMock.Object);

			HttpStatusCodeResult result = controller.CreateFolder("") as HttpStatusCodeResult;

			Assert.IsNotNull(result);
			Assert.AreEqual(400, result.StatusCode);
		}

		[TestMethod]
		public void CreateFolder_PositiveTest_ReturnCorrectView()
		{
			DirectoryController controller = new DirectoryController(directoryServiceMock.Object, fileServiceMock.Object, searchServiceMock.Object);
			controller.ControllerContext = new ControllerContext(HttpContextBaseMock.Object, new RouteData(), controller);

			ViewResult result = controller.CreateFolder("D/data/NewFolder") as ViewResult;

			Assert.IsNotNull(result);
			Assert.AreEqual("CreateFolder", result.ViewName);
		}

		[TestMethod]
		public void CreateFolder_PostActionWithExistFolder_ReturnCorrectJson()
		{
			directoryServiceMock.Setup(a => a.IsExist(It.IsAny<string>())).Returns(true);
			DirectoryController controller = new DirectoryController(directoryServiceMock.Object, fileServiceMock.Object, searchServiceMock.Object);

			JsonResult result = controller.CreateFolder(new DirectoryViewModel()) as JsonResult;

			Assert.IsNotNull(result);
			Assert.AreEqual("{ Status = Exist }", result.Data.ToString());
		}

		[TestMethod]
		public void CreateFolder_ThrowException_ReturnJsonResult()
		{
			directoryServiceMock.Setup(a => a.GetAllDirectories(It.IsAny<String>())).Throws<UnauthorizedAccessException>();
			DirectoryController controller = new DirectoryController(directoryServiceMock.Object, fileServiceMock.Object, searchServiceMock.Object);

			JsonResult result = controller.CreateFolder(new DirectoryViewModel()) as JsonResult;

			Assert.IsNotNull(result);
			Assert.AreEqual("{ Status = NotAcceptable }", result.Data.ToString());
		}

		[TestMethod]
		public void CreateFolder_PositiveTest_CreateDirectoryAndReturnPartialView()
		{
			fileServiceMock.Setup(a => a.GetParrent(It.IsAny<string>())).Returns("D/");
			DirectoryController controller = new DirectoryController(directoryServiceMock.Object, fileServiceMock.Object, searchServiceMock.Object);
			controller.ControllerContext = new ControllerContext(HttpContextBaseMock.Object, new RouteData(), controller);

			PartialViewResult result = controller.CreateFolder(new DirectoryViewModel() { ParentDirectoryPath = "D/" }) as PartialViewResult;

			Assert.IsNotNull(result);
			directoryServiceMock.Verify(a => a.CreateDirectory(It.IsAny<string>()));
		}

		//

		[TestMethod]
		public void CreateFile_EmptyPath_ReturnStatusCode400()
		{
			DirectoryController controller = new DirectoryController(directoryServiceMock.Object, fileServiceMock.Object, searchServiceMock.Object);

			HttpStatusCodeResult result = controller.CreateFile("") as HttpStatusCodeResult;

			Assert.IsNotNull(result);
			Assert.AreEqual(400, result.StatusCode);
		}

		[TestMethod]
		public void CreateFile_PositiveTest_ReturnCorrectView()
		{
			DirectoryController controller = new DirectoryController(directoryServiceMock.Object, fileServiceMock.Object, searchServiceMock.Object);
			controller.ControllerContext = new ControllerContext(HttpContextBaseMock.Object, new RouteData(), controller);

			ViewResult result = controller.CreateFile("D/data/NewFolder") as ViewResult;

			Assert.IsNotNull(result);
			Assert.AreEqual("CreateFile", result.ViewName);
		}

		[TestMethod]
		public void CreateFile_PostActionWithExistFolder_ReturnCorrectJson()
		{
			fileServiceMock.Setup(a => a.IsExist(It.IsAny<string>())).Returns(true);
			DirectoryController controller = new DirectoryController(directoryServiceMock.Object, fileServiceMock.Object, searchServiceMock.Object);

			JsonResult result = controller.CreateFile(new CreateFileViewModel()) as JsonResult;

			Assert.IsNotNull(result);
			Assert.AreEqual("{ Status = Exist }", result.Data.ToString());
		}

		[TestMethod]
		public void CreateFile_ThrowException_ReturnJsonResult()
		{
			directoryServiceMock.Setup(a => a.GetAllDirectories(It.IsAny<String>())).Throws<UnauthorizedAccessException>();
			DirectoryController controller = new DirectoryController(directoryServiceMock.Object, fileServiceMock.Object, searchServiceMock.Object);

			JsonResult result = controller.CreateFile(new CreateFileViewModel()) as JsonResult;

			Assert.IsNotNull(result);
			Assert.AreEqual("{ Status = NotAcceptable }", result.Data.ToString());
		}

		[TestMethod]
		public void CreateFile_PositiveTest_CreateDirectoryAndReturnPartialView()
		{
			fileServiceMock.Setup(a => a.GetParrent(It.IsAny<string>())).Returns("D/");
			DirectoryController controller = new DirectoryController(directoryServiceMock.Object, fileServiceMock.Object, searchServiceMock.Object);
			controller.ControllerContext = new ControllerContext(HttpContextBaseMock.Object, new RouteData(), controller);

			PartialViewResult result = controller.CreateFile(new CreateFileViewModel() { ParentDirectoryPath = "D/" }) as PartialViewResult;

			Assert.IsNotNull(result);
			fileServiceMock.Verify(a => a.CreateFile(It.IsAny<string>()));
		}
	}
}
