using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FileSystemViewer.BLL.Interface.Entities;
using FileSystemViewer.PLMVC.Models.Directory;

namespace FileSystemViewer.PLMVC.Infrastructure.Mappers
{
	public static class DirectoryMapper
	{
		public static DirectoryViewModel ToMvcDirectory(this BllDirectory directory)
		{
			return new DirectoryViewModel
			{
				Name = directory.Name,
				LastAccessTime = directory.LastAccessTime
			};
		}

		public static BllDirectory ToBllDirectory(this DirectoryViewModel directory)
		{
			return new BllDirectory
			{
				Name = directory.Name,
				LastAccessTime = directory.LastAccessTime
			};
		}
	}
}