using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FileSystemViewer.BLL.Interface.Interfaces;
using FileSystemViewer.PLMVC.Infrastructure.Mappers;
using FileSystemViewer.PLMVC.Models;

namespace FileSystemViewer.PLMVC.Controllers
{
    public class HomeController : Controller
    {

		private readonly IUserService userService;
	    private readonly IRoleService roleService;


		public HomeController(IUserService userService, IRoleService roleService)
		{
			this.userService = userService;
			this.roleService = roleService; 
		}

        // GET: Home
        public ActionResult Index()
        {
	        List<ViewRole> list = roleService.GetAll().Select(r => r.ToMvcRole() ).ToList();

            return View(list);
        }
    }
}