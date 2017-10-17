using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FileSystemViewer.BLL.Interface.Entities;
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
				FileSizeText = file.FileSizeText,
				Extension = file.Extension,
				LastAccessTime = file.LastAccessTime
			};
		}

		public static BllFile ToBllFile(this FileViewModel file)
		{
			return new BllFile
			{
				Name = file.Name,
				FileSizeText = file.FileSizeText,
				Extension = file.Extension,
				LastAccessTime = file.LastAccessTime
			};
		}
	}
}