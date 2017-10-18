using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FileSystemViewer.BLL.Interface.Entities;
using FileSystemViewer.PLMVC.Models.Directory;
using FileSystemViewer.PLMVC.Models.Drive;

namespace FileSystemViewer.PLMVC.Infrastructure.Mappers
{
	public static class DriveMapper
	{
		public static DriveViewModel ToMvcDrive(this BllDrive drive)
		{
			return new DriveViewModel
			{
				Name = drive.Name.Remove(1,1),
				DriveType = drive.DriveType,
				TotalSize = drive.TotalSize,
				TotalFreeSpace = drive.TotalFreeSpace
			};
		}

		public static BllDrive ToBllDrive(this DriveViewModel drive)
		{
			return new BllDrive
			{
				Name = drive.Name.Insert(1,":"),
				DriveType = drive.DriveType,
				TotalSize = drive.TotalSize,
				TotalFreeSpace = drive.TotalFreeSpace
			};
		}
	}
}