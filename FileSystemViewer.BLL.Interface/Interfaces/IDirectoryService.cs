using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileSystemViewer.BLL.Interface.Entities;

namespace FileSystemViewer.BLL.Interface.Interfaces
{
	public interface IDirectoryService
	{
		IEnumerable<BllDirectory> GetAllDirectories(string path);
		BllDirectory GetDirectory(int id);
		void CreateDirectory(string path);
		void DeleteDirectory(string path);
        string GetParrent(string path);
	}
}
