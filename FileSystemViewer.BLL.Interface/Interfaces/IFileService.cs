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
		void CreateFile(BllFile entity);
		void DeleteFile(string path);
	}
}
