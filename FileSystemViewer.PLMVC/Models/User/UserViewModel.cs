using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FileSystemViewer.PLMVC.Models.User
{
	public class UserViewModel
	{
		public int Id { get; set; }
		public string UserName { get; set; }
		public string Email { get; set; }
		public IEnumerable<string> Roles { get; set; }
	}
}