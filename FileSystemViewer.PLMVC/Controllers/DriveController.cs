using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FileSystemViewer.BLL.Interface.Interfaces;
using FileSystemViewer.PLMVC.Infrastructure.Mappers;
using FileSystemViewer.PLMVC.Models.Drive;

namespace FileSystemViewer.PLMVC.Controllers
{
	[Authorize]
    public class DriveController : Controller
    {
		private readonly IDriveService driveService;


		public DriveController(IDriveService driveService)
		{
		   this.driveService = driveService;
		}


		[HttpGet]
		public ActionResult GetDrives(string path = "")
		{
			List<DriveViewModel> drives;
	        try
	        {
		        drives = driveService.GetDrives().Select(d => d.ToMvcDrive()).ToList();
	        }
			catch (UnauthorizedAccessException e)
			{
				return Json(new { Status = "NotAcceptable" }, JsonRequestBehavior.AllowGet);
			}

	        if (Request.IsAjaxRequest())
		        return PartialView(drives);
			return View(drives);
        }
    }
}