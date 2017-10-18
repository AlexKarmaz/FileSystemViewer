using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FileSystemViewer.PLMVC.Models.Directory;
using FileSystemViewer.PLMVC.Models.File;

namespace FileSystemViewer.PLMVC.Models
{
	public class ExplorerViewModel
	{
		public string Name { get; set; }
		public string Type { get; set; }
		public string Size { get; set; }
		public DateTime LastAccessTime { get; set; }
	}
}