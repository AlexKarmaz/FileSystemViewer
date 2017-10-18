using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileSystemViewer.BLL.Interface.Entities;
using FileSystemViewer.BLL.Interface.Interfaces;


namespace FileSystemViewer.BLL.Services
{
	public class DriveService : IDriveService
	{
		public IEnumerable<BllDrive> GetDrives()
		{
			IList<BllDrive> bllDrives = new List<BllDrive>();
			var drives = DriveInfo.GetDrives();

			foreach (DriveInfo drive in drives)
			{
				BllDrive newDrive = new BllDrive();
				newDrive.Name = drive.Name;
				newDrive.DriveType = drive.DriveType.ToString();
				newDrive.TotalSize = SizeConverter(drive.TotalSize) ;
				newDrive.TotalFreeSpace = SizeConverter(drive.TotalFreeSpace);
				bllDrives.Add(newDrive);
			}

			return bllDrives;
		}

		private string SizeConverter(long size)
		{
			string newSize = "";
			if (size > 1024)
			{
				size = size / 1024;
				if (size > 1024)
				{
					size = size / 1024;
					if (size > 1024)
					{
						size = size / 1024;
						newSize = size + "GB";
					}
					else
					{
						newSize = size + "MB";
					}
				}
				else
				{
					newSize = size + "KB";
				}
			}
			else
			{
				newSize = size + "B";
			}

			return newSize;
		}
	}
}
