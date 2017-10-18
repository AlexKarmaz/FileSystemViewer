using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileSystemViewer.BLL.Interface.Entities;

namespace FileSystemViewer.BLL.Interface.Interfaces
{
	public interface IDriveService
	{
		IEnumerable<BllDrive> GetDrives();
	}
}
