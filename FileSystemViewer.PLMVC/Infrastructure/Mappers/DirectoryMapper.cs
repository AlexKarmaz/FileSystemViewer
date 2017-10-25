using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FileSystemViewer.BLL.Interface.Entities;
using FileSystemViewer.PLMVC.Models;
using FileSystemViewer.PLMVC.Models.Directory;

namespace FileSystemViewer.PLMVC.Infrastructure.Mappers
{
	public static class DirectoryMapper
	{
		public static ExplorerViewModel ToExplorerObject(this BllDirectory directory)
		{
			return new ExplorerViewModel
			{
				Name = directory.Name,
				Type = "Folder",
				LastAccessTime = directory.LastAccessTime,
				Size = ""
			};
		}

		public static FolderTreeViewModel ToFolderTreeViewModel(this BllDirectory directory)
		{
			return new FolderTreeViewModel
			{
				Name = directory.Name
			};
		}
	}
}