using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FileSystemViewer.BLL.Interface.Interfaces;
using FileSystemViewer.PLMVC.Infrastructure.Mappers;
using FileSystemViewer.PLMVC.Models;
using FileSystemViewer.PLMVC.Models.Drive;

namespace FileSystemViewer.PLMVC.Controllers
{
    public class FolderTreeController : Controller
    {
        private readonly IDriveService driveService;
		private readonly IDirectoryService directoryService;
		private readonly IFileService fileService;


		public FolderTreeController(IDriveService driveService, IDirectoryService directoryService,IFileService fileService)
		{
			this.directoryService = directoryService;
		    this.driveService = driveService;
			this.fileService = fileService;
		}

		[HttpGet]
		public ActionResult GetDrives(string path = "")
		{
			List<FolderTreeViewModel> drives;
			try
			{
				drives = driveService.GetDrives().Select(d => d.ToFolderTreeViewModel()).ToList();
			}
			catch (UnauthorizedAccessException e)
			{
				return Json(new { Status = "NotAcceptable" }, JsonRequestBehavior.AllowGet);
			}

			if (Request.IsAjaxRequest())
				return PartialView("GetDrivesTree",drives);
			return View(drives);
		}

		[HttpGet]
		public ActionResult GetAllDirectory(string path = "")
		{
			List<FolderTreeViewModel> folderTreeObjects;

			if (path == "")
			{
				return Redirect("/FolderTree/GetDrives/");
			}

			path = PathValidation(path);

			try
			{
				var dirListModel = directoryService.GetAllDirectories(path).Select(d => d.ToFolderTreeViewModel());
				var fileListModel = fileService.GetAllFiles(path).Select(f => f.ToFolderTreeViewModel());


				folderTreeObjects = new List<FolderTreeViewModel>();
				foreach (var obj in dirListModel)
				{
					folderTreeObjects.Add(obj);
				}

				foreach (var obj in fileListModel)
				{
					folderTreeObjects.Add(obj);
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
				return PartialView("GetAllDirectoryTree", folderTreeObjects);
			return View("GetAllDirectoryTree", folderTreeObjects);
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