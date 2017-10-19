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
    }
}