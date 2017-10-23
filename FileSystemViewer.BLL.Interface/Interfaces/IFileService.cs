using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileSystemViewer.BLL.Interface.Entities;

namespace FileSystemViewer.BLL.Interface.Interfaces
{
	public interface IFileService
	{
		IEnumerable<BllFile> GetAllFiles(string path);
		BllFile GetFile(int id);
		void CreateFile(string path);
		void DeleteFile(string path);
		string GetParrent(string path);
		bool IsExist(string path);
	}
}
