using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using FileSystemViewer.BLL.Interface.Interfaces;
using FileSystemViewer.PLMVC.Infrastructure.Mappers;
using FileSystemViewer.PLMVC.Models;
using FileSystemViewer.PLMVC.Models.Directory;
using FileSystemViewer.PLMVC.Models.File;

namespace FileSystemViewer.PLMVC.Controllers
{
	[Authorize]
    public class DirectoryController : Controller
    {
        
		private readonly IDirectoryService directoryService;
		private readonly IFileService fileService;

		public DirectoryController(IDirectoryService directoryService, IFileService fileService)
		{
			this.directoryService = directoryService;
			this.fileService = fileService;
		}

		[HttpGet]
		public ActionResult GetAllDirectory(string path = "")
		{
			if (path == "")
			{
				return Redirect("/Drive/GetDrives/");
			}

			if (path.Last() != '/')
			{
				path = path + '/';
			}

			if (path.Length > 1)
			{
				path = path.Insert(1, ":");
			}
			var dirListModel = directoryService.GetAllDirectories(path).Select(d => d.ToExplorerObject());
			var fileListModel = fileService.GetAllFiles(path).Select(f => f.ToExplorerObject());

			List<ExplorerViewModel> explorerObjects = new List<ExplorerViewModel>();
			foreach (var obj in dirListModel)
			{
				explorerObjects.Add(obj);
			}

			foreach (var obj in fileListModel)
			{
				explorerObjects.Add(obj);
			}
			path = path.Remove(1, 1);
			path = path.Replace("/","\\\\");
			ViewBag.LastPath = path;
			if (Request.IsAjaxRequest())
				return PartialView(explorerObjects);
			return View(explorerObjects);
		}


	    [HttpGet]
		[Authorize(Roles = "admin")]
	    public ActionResult DeleteDirectory(string path = "")
	    {
			if (path == "")
			{
				return Redirect("/Drive/GetDrives/");
			}

			if (path.Last() != '/')
			{
				path = path + '/';
			}

			if (path.Length > 1)
			{
				path = path.Insert(1, ":");
			}

		    directoryService.DeleteDirectory(path);
		    string newPath = directoryService.GetParrent(path);

			var dirListModel = directoryService.GetAllDirectories(newPath).Select(d => d.ToExplorerObject());
			var fileListModel = fileService.GetAllFiles(newPath).Select(f => f.ToExplorerObject());

			List<ExplorerViewModel> explorerObjects = new List<ExplorerViewModel>();

			foreach (var obj in dirListModel)
			{
				explorerObjects.Add(obj);
			}

			foreach (var obj in fileListModel)
			{
				explorerObjects.Add(obj);
			}

			newPath = newPath.Remove(1, 1);

			if (newPath.Last() != '\\')
			{
				newPath = newPath + "\\";
			}

			newPath = newPath.Replace("\\", "\\\\");
			ViewBag.LastPath = newPath;

			if (Request.IsAjaxRequest())
				return PartialView("GetAllDirectory",explorerObjects);
			return View("GetAllDirectory",explorerObjects);
	    }

		[HttpGet]
		[Authorize(Roles = "admin")]
		public ActionResult DeleteFile(string path = "")
		{
			if (path == "")
			{
				return Redirect("/Drive/GetDrives/");
			}

			if (path.Last() == '/')
			{
				path = path.Remove(path.Length-1,1);
			}

			if (path.Length > 1)
			{
				path = path.Insert(1, ":");
			}

			fileService.DeleteFile(path);
			string newPath = fileService.GetParrent(path);

			var dirListModel = directoryService.GetAllDirectories(newPath).Select(d => d.ToExplorerObject());
			var fileListModel = fileService.GetAllFiles(newPath).Select(f => f.ToExplorerObject());

			List<ExplorerViewModel> explorerObjects = new List<ExplorerViewModel>();

			foreach (var obj in dirListModel)
			{
				explorerObjects.Add(obj);
			}

			foreach (var obj in fileListModel)
			{
				explorerObjects.Add(obj);
			}

			newPath = newPath.Remove(1, 1);

			if (newPath.Last() != '\\')
			{
				newPath = newPath + "\\";
			}
			newPath = newPath.Replace("\\", "\\\\");
			ViewBag.LastPath = newPath;

			if (Request.IsAjaxRequest())
				return PartialView("GetAllDirectory", explorerObjects);
			return View("GetAllDirectory", explorerObjects);
		}

		[HttpGet]
		[Authorize(Roles = "admin")]
		public ActionResult CreateFolder(string path)
		{
			if (path == String.Empty)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			if (path.Last() != '/')
			{
				path = path + '/';
			}

			if (path.Length > 1)
			{
				path = path.Insert(1, ":");
			}
			if (Request.IsAjaxRequest())
				return PartialView("CreateFolder", new DirectoryViewModel(){ParentDirectoryPath = path});
			return View("CreateFolder", new DirectoryViewModel(){ParentDirectoryPath = path});
		}

		[HttpPost]
		[Authorize(Roles = "admin")]
		[ValidateAntiForgeryToken]
		public ActionResult CreateFolder(DirectoryViewModel directoryModel)
		{
			if (directoryModel == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			string path = directoryModel.ParentDirectoryPath + directoryModel.Name;
			if (directoryService.IsExist(path))
			{
				ModelState.AddModelError(String.Empty, "Such folder is already exists");
		
				return PartialView(directoryModel);
			}

			if (ModelState.IsValid)
			{
				directoryService.CreateDirectory(path);

				var dirListModel = directoryService.GetAllDirectories(directoryModel.ParentDirectoryPath).Select(d => d.ToExplorerObject());
				var fileListModel = fileService.GetAllFiles(directoryModel.ParentDirectoryPath).Select(f => f.ToExplorerObject());

				List<ExplorerViewModel> explorerObjects = new List<ExplorerViewModel>();

				foreach (var obj in dirListModel)
				{
					explorerObjects.Add(obj);
				}

				foreach (var obj in fileListModel)
				{
					explorerObjects.Add(obj);
				}
				path = directoryModel.ParentDirectoryPath.Remove(1, 1);

				if (path.Last() != '\\')
				{
					path = path + "\\";
				}
				path = path.Replace("\\", "\\\\");
				ViewBag.LastPath = path;
				return PartialView("GetExplorerTable", explorerObjects);
			}

			if (Request.IsAjaxRequest())
				return PartialView( directoryModel);
			return View( directoryModel);
		}

		[HttpGet]
		[Authorize(Roles = "admin")]
		public ActionResult CreateFile(string path)
		{
			if (path == String.Empty)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			if (path.Last() != '/')
			{
				path = path + '/';
			}

			if (path.Length > 1)
			{
				path = path.Insert(1, ":");
			}
			if (Request.IsAjaxRequest())
				return PartialView("CreateFile", new CreateFileViewModel() { ParentDirectoryPath = path });
			return View("CreateFile", new CreateFileViewModel() { ParentDirectoryPath = path });
		}

	    [HttpPost]
	    [ValidateAntiForgeryToken]
		[Authorize(Roles = "admin")]
	    public ActionResult CreateFile(CreateFileViewModel fileModel)
	    {
			if (fileModel == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			string path = fileModel.ParentDirectoryPath + fileModel.Name;
			if (fileService.IsExist(path))
			{
				ModelState.AddModelError(String.Empty, "Such file is already exists");

				return PartialView(fileModel);
			}

			if (!(fileModel.Name.IndexOf(".") == fileModel.Name.Length - 4 || fileModel.Name.IndexOf(".") == fileModel.Name.Length - 3))
			{
				ModelState.AddModelError(String.Empty, "You forgot write file extension");

				return PartialView(fileModel);
			}

			if (ModelState.IsValid)
			{
				fileService.CreateFile(path);

				var dirListModel = directoryService.GetAllDirectories(fileModel.ParentDirectoryPath).Select(d => d.ToExplorerObject());
				var fileListModel = fileService.GetAllFiles(fileModel.ParentDirectoryPath).Select(f => f.ToExplorerObject());

				List<ExplorerViewModel> explorerObjects = new List<ExplorerViewModel>();

				foreach (var obj in dirListModel)
				{
					explorerObjects.Add(obj);
				}

				foreach (var obj in fileListModel)
				{
					explorerObjects.Add(obj);
				}

				path = fileModel.ParentDirectoryPath.Remove(1, 1);

				if (path.Last() != '\\')
				{
					path = path + "\\";
				}
				path = path.Replace("\\", "\\\\");
				ViewBag.LastPath = path;
				return PartialView("GetExplorerTable", explorerObjects);
			}

			if (Request.IsAjaxRequest())
				return PartialView(fileModel);
			return View(fileModel);
	    }
    }
}