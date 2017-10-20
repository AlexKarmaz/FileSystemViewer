using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FileSystemViewer.PLMVC.Models.Directory
{
	public class DirectoryViewModel
	{
		[Display(Name = "Folder name")]
		[Required(ErrorMessage = "The field can not be empty!")]
		[RegularExpression("^[^\\\\/?%*:|\"<>]+$", ErrorMessage = "Invalid folder name")]
		public string Name { get; set; }
		public string ParentDirectoryPath { get; set; }
	}
}