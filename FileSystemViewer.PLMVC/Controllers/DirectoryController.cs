using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using FileSystemViewer.BLL.Interface.Entities;
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
	    private readonly ISearchService searchService;

		public DirectoryController(IDirectoryService directoryService, IFileService fileService, ISearchService searchService)
		{
			this.directoryService = directoryService;
			this.fileService = fileService;
			this.searchService = searchService;
		}

		[HttpGet]
		public ActionResult GetAllDirectory(string path = "")
		{
			List<ExplorerViewModel> explorerObjects;

			if (path == "")
			{
				return Redirect("/Drive/GetDrives/");
			}

			path = PathValidation(path);

			try
			{
				var dirListModel = directoryService.GetAllDirectories(path).Select(d => d.ToExplorerObject());
				var fileListModel = fileService.GetAllFiles(path).Select(f => f.ToExplorerObject());


				explorerObjects = new List<ExplorerViewModel>();
				foreach (var obj in dirListModel)
				{
					explorerObjects.Add(obj);
				}

				foreach (var obj in fileListModel)
				{
					explorerObjects.Add(obj);
				}
			}
			catch (UnauthorizedAccessException e)
			{
				return Json(new { Status = "NotAcceptable" }, JsonRequestBehavior.AllowGet);
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
		    List<ExplorerViewModel> explorerObjects;
		    string newPath;

			if (path == "")
			{
				return Redirect("/Drive/GetDrives/");
			}

			path = PathValidation(path);

		    try
		    {
			    directoryService.DeleteDirectory(path);
			    newPath = directoryService.GetParrent(path);

			    var dirListModel = directoryService.GetAllDirectories(newPath).Select(d => d.ToExplorerObject());
			    var fileListModel = fileService.GetAllFiles(newPath).Select(f => f.ToExplorerObject());

			    explorerObjects = new List<ExplorerViewModel>();

			    foreach (var obj in dirListModel)
			    {
				    explorerObjects.Add(obj);
			    }

			    foreach (var obj in fileListModel)
			    {
				    explorerObjects.Add(obj);
			    }
		    }
			catch (UnauthorizedAccessException e)
			{
				return Json(new { Status = "NotAcceptable" }, JsonRequestBehavior.AllowGet);
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
			List<ExplorerViewModel> explorerObjects;
			string newPath;

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


			try
			{
				fileService.DeleteFile(path);
			    newPath = fileService.GetParrent(path);

				var dirListModel = directoryService.GetAllDirectories(newPath).Select(d => d.ToExplorerObject());
				var fileListModel = fileService.GetAllFiles(newPath).Select(f => f.ToExplorerObject());

				explorerObjects = new List<ExplorerViewModel>();

				foreach (var obj in dirListModel)
				{
					explorerObjects.Add(obj);
				}

				foreach (var obj in fileListModel)
				{
					explorerObjects.Add(obj);
				}
			}
			catch (UnauthorizedAccessException e)
			{
				return Json(new { Status = "NotAcceptable" }, JsonRequestBehavior.AllowGet);
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
			if (String.IsNullOrEmpty(path))
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			path = PathValidation(path);

			if (Request.IsAjaxRequest())
				return PartialView("CreateFolder", new DirectoryViewModel(){ParentDirectoryPath = path});
			return View("CreateFolder", new DirectoryViewModel(){ParentDirectoryPath = path});
		}

		[HttpPost]
		[Authorize(Roles = "admin")]
		[ValidateAntiForgeryToken]
		public ActionResult CreateFolder(DirectoryViewModel directoryModel)
		{
			List<ExplorerViewModel> explorerObjects;
			if (directoryModel == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			string path = directoryModel.ParentDirectoryPath + directoryModel.Name;
			if (directoryService.IsExist(path))
			{
				return Json(new { Status = "Exist" }, JsonRequestBehavior.AllowGet);
			}

			if (ModelState.IsValid)
			{
				try
				{
					directoryService.CreateDirectory(path);

					var dirListModel =
						directoryService.GetAllDirectories(directoryModel.ParentDirectoryPath).Select(d => d.ToExplorerObject());
					var fileListModel = fileService.GetAllFiles(directoryModel.ParentDirectoryPath).Select(f => f.ToExplorerObject());

					explorerObjects = new List<ExplorerViewModel>();

					foreach (var obj in dirListModel)
					{
						explorerObjects.Add(obj);
					}

					foreach (var obj in fileListModel)
					{
						explorerObjects.Add(obj);
					}
				}
				catch (UnauthorizedAccessException e)
				{
					return Json(new { Status = "NotAcceptable" }, JsonRequestBehavior.AllowGet);
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
			if (String.IsNullOrEmpty(path))
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			path = PathValidation(path);
			
			if (Request.IsAjaxRequest())
				return PartialView("CreateFile", new CreateFileViewModel() { ParentDirectoryPath = path });
			return View("CreateFile", new CreateFileViewModel() { ParentDirectoryPath = path });
		}

	    [HttpPost]
	    [ValidateAntiForgeryToken]
		[Authorize(Roles = "admin")]
	    public ActionResult CreateFile(CreateFileViewModel fileModel)
	    {
		    List<ExplorerViewModel> explorerObjects;

			if (fileModel == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			string path = fileModel.ParentDirectoryPath + fileModel.Name+"."+fileModel.Extension;
			if (fileService.IsExist(path))
			{
				return Json(new { Status = "Exist" }, JsonRequestBehavior.AllowGet);
			}

			if (ModelState.IsValid)
			{
				try
				{
					fileService.CreateFile(path);

					var dirListModel =
						directoryService.GetAllDirectories(fileModel.ParentDirectoryPath).Select(d => d.ToExplorerObject());
					var fileListModel = fileService.GetAllFiles(fileModel.ParentDirectoryPath).Select(f => f.ToExplorerObject());

					explorerObjects = new List<ExplorerViewModel>();

					foreach (var obj in dirListModel)
					{
						explorerObjects.Add(obj);
					}

					foreach (var obj in fileListModel)
					{
						explorerObjects.Add(obj);
					}
				}
				catch (UnauthorizedAccessException e)
				{
					return Json(new { Status = "NotAcceptable" }, JsonRequestBehavior.AllowGet);
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

		[HttpGet]
		public ActionResult GetAllDirectoryByName(string path = "")
		{
			List<ExplorerViewModel> explorerObjects;

			if (path == "")
			{
				return Redirect("/Drive/GetDrives/");
			}

			path = PathValidation(path);

			try
			{
				var dirListModel = directoryService.GetAllDirectories(path).Select(d => d.ToExplorerObject());
				var fileListModel = fileService.GetAllFiles(path).Select(f => f.ToExplorerObject());


				explorerObjects = new List<ExplorerViewModel>();
				foreach (var obj in dirListModel)
				{
					explorerObjects.Add(obj);
				}

				foreach (var obj in fileListModel)
				{
					explorerObjects.Add(obj);
				}

				explorerObjects.Sort((x,y) => x.Name.CompareTo(y.Name));
			}
			catch (UnauthorizedAccessException e)
			{
				return Json(new { Status = "NotAcceptable" }, JsonRequestBehavior.AllowGet);
			}


			path = path.Remove(1, 1);
			path = path.Replace("/", "\\\\");

			ViewBag.LastPath = path;
			if (Request.IsAjaxRequest())
				return PartialView("GetAllDirectory", explorerObjects);
			return View("GetAllDirectory", explorerObjects);
		}

		[HttpGet]
		public ActionResult GetAllDirectoryByType(string path = "")
		{
			List<ExplorerViewModel> explorerObjects;

			if (path == "")
			{
				return Redirect("/Drive/GetDrives/");
			}

			path = PathValidation(path);

			try
			{
				var dirListModel = directoryService.GetAllDirectories(path).Select(d => d.ToExplorerObject());
				var fileListModel = fileService.GetAllFiles(path).Select(f => f.ToExplorerObject());


				explorerObjects = new List<ExplorerViewModel>();
				foreach (var obj in dirListModel)
				{
					explorerObjects.Add(obj);
				}

				foreach (var obj in fileListModel)
				{
					explorerObjects.Add(obj);
				}

				explorerObjects.Sort((x, y) => y.Type.CompareTo(x.Type));
			}
			catch (UnauthorizedAccessException e)
			{
				return Json(new { Status = "NotAcceptable" }, JsonRequestBehavior.AllowGet);
			}


			path = path.Remove(1, 1);
			path = path.Replace("/", "\\\\");

			ViewBag.LastPath = path;
			if (Request.IsAjaxRequest())
				return PartialView("GetAllDirectory", explorerObjects);
			return View("GetAllDirectory", explorerObjects);
		}

		[HttpGet]
		public ActionResult Search( string searchString, string path = "")
		{

			if (String.IsNullOrEmpty(searchString))
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			List<ExplorerSearchViewModel> explorerObjects;

			if (path == "")
			{
				return Redirect("/Drive/GetDrives/");
			}

			path = PathValidation(path);

			try
			{
				IList<BllSearchResult> results = new List<BllSearchResult>();

				searchService.SearchDirectories(path, searchString, results);

				var resultListModel = results.Select(d => d.ToExplorerSearchObject()).OrderByDescending(d => d.Type);

				 explorerObjects = new List<ExplorerSearchViewModel>();

				foreach (var obj in resultListModel)
				{
					explorerObjects.Add(obj);
				}
			}
			catch (UnauthorizedAccessException e)
			{
				return Json(new { Status = "NotAcceptable" }, JsonRequestBehavior.AllowGet);
			}


			path = path.Remove(1, 1);
			path = path.Replace("/", "\\\\");

			ViewBag.LastPath = path;
			if (Request.IsAjaxRequest())
				return PartialView("Search", explorerObjects);
			return View("Search", explorerObjects);
		}

	    private string PathValidation(string path)
	    {
			if (path.Last() != '/')
			{
				path = path + '/';
			}

			if (path.Length > 1)
			{
				path = path.Insert(1, ":");
			}

			return path;
	    }

    }
}