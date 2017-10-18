using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FileSystemViewer.PLMVC.Models.Drive
{
	public class DriveViewModel
	{
		[Display(Name = "Drive name")]
		[Required(ErrorMessage = "The field can not be empty!")]
		public string Name { get; set; }
		public string DriveType { get; set; }
		public string TotalSize { get; set; }
		public string TotalFreeSpace { get; set; }
	}
}