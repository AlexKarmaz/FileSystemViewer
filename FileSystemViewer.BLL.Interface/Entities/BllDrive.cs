using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemViewer.BLL.Interface.Entities
{
	public class BllDrive
	{
		public string Name { get; set; }
		public string DriveType { get; set; }
		public string TotalSize { get; set; }
		public string TotalFreeSpace { get; set; }
	}
}
