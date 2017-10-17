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
		public IEnumerable<DirectoryViewModel> Directories;
		public IEnumerable<FileViewModel> Files;
	}
}