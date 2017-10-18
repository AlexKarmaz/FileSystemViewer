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
	[Authorize]
    public class HomeController : Controller
	{
		private readonly IFileService fileService;
		private readonly IDirectoryService directoryService;
		private readonly IDriveService driveService;


		public HomeController(IFileService fileService, IDirectoryService directoryService,IDriveService driveService)
		{
			this.fileService = fileService;
			this.directoryService = directoryService;
			this.driveService = driveService;
		}

		[HttpGet]
		public ActionResult Index(string path = "")
		{
			////var realPath = Server.MapPath(RootDirectory + path);

			//if (!Request.RawUrl.Contains(RouteData.Values["action"].ToString()))
			//{
			//	Response.Redirect("/Home/Index/");
			//}

			//if (Directory.Exists(realPath))
			//{
			//	if (Request.RawUrl.Last() != '/')
			//	{
			//		Response.Redirect("/Home/Index/" + path + "/");
			//	}

			//	var dirListModel = directoryService.GetAllDirectories(realPath).Select(d => d.ToMvcDirectory());
			//	var fileListModel = fileService.GetAllFiles(realPath).Select(f => f.ToMvcFile());

			//	var explorerModel = new ExplorerViewModel
			//	{
			//		Directories = dirListModel,
			//		Files = fileListModel
			//	};

			//	return View(explorerModel);
			//}
			//return Content(path + " is not a valid file or directory. " + RouteData.Values["controller"] + " " + RouteData.Values["action"]
			//	+ " " + RouteData.Values["path"]);
			List<DriveViewModel> drives =  driveService.GetDrives().Select(d => d.ToMvcDrive()).ToList();
			return View(drives);
		}
    }
}