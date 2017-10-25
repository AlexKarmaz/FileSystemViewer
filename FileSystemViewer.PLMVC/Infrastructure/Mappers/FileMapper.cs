using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FileSystemViewer.BLL.Interface.Entities;
using FileSystemViewer.PLMVC.Models;
using FileSystemViewer.PLMVC.Models.File;

namespace FileSystemViewer.PLMVC.Infrastructure.Mappers
{
	public static class FileMapper
	{
		public static FileViewModel ToMvcFile(this BllFile file)
		{
			return new FileViewModel
			{
				Name = file.Name,
				FileSize = file.FileSize,
				Extension = file.Extension,
				LastAccessTime = file.LastAccessTime
			};
		}

		public static BllFile ToBllFile(this FileViewModel file)
		{
			return new BllFile
			{
				Name = file.Name,
				FileSize = file.FileSize,
				Extension = file.Extension,
				LastAccessTime = file.LastAccessTime
			};
		}

		public static ExplorerViewModel ToExplorerObject(this BllFile file)
		{
			return new ExplorerViewModel
			{
				Name = file.Name,
				Type = "File",
				Size = file.FileSize,
				LastAccessTime = file.LastAccessTime
			};
		}

		public static FolderTreeViewModel ToFolderTreeViewModel(this BllFile file)
		{
			return new FolderTreeViewModel
			{
				Name = file.Name
			};
		}
	}
}