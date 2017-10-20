using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using FileSystemViewer.BLL.Interface.Interfaces;
using FileSystemViewer.PLMVC.Infrastructure.Mappers;
using FileSystemViewer.PLMVC.Models;
using FileSystemViewer.PLMVC.Models.Directory;

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
		public ActionResult CreateFolder(string path)
		{

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
		[ValidateAntiForgeryToken]
		public ActionResult CreateFolder(DirectoryViewModel directoryModel)
		{
			string path = directoryModel.ParentDirectoryPath + directoryModel.Name;
			if (directoryService.IsExist(path))
			{
				ModelState.AddModelError(String.Empty, "Such folder is already exists");
		
				return PartialView("CreateFolder", directoryModel);
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
				//return Redirect(Url.Action("GetAllDirectory", directoryModel.ParentDirectoryPath));
				return PartialView("GetAllDirectory", explorerObjects);
			}

			if (Request.IsAjaxRequest())
				return PartialView( directoryModel);
			return View( directoryModel);
		}
    }
}