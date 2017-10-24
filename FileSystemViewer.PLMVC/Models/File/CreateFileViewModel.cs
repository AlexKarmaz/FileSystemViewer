using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FileSystemViewer.PLMVC.Models.File
{
	public class CreateFileViewModel
	{
		[Display(Name = "File name")]
		[Required(ErrorMessage = "The field can not be empty!")]
		[RegularExpression("(^(?! ))(^[^\\\\/?*:|\"<>]+$)", ErrorMessage = "Invalid file name")]
		public string Name { get; set; }
		[Display(Name = "File extension")]
		[Required(ErrorMessage = "The field can not be empty!")]
		[RegularExpression("^[a-z]+$", ErrorMessage = "Invalid extension name")]
		public string Extension { get; set; }
		public string ParentDirectoryPath { get; set; }
	}
}