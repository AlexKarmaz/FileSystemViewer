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
	public class FileService : IFileService
	{
		public IEnumerable<BllFile> GetAllFiles(string path)
		{
			return Directory.EnumerateFiles(path)
				.Select(fileName => new FileInfo(fileName))
				.Select(fileInfo => new BllFile
				{
					Name = fileInfo.Name,
					FileSizeText = (fileInfo.Length < 1024) ? fileInfo.Length.ToString() + " B" : fileInfo.Length / 1024 + " KB",
					Extension = fileInfo.Extension,
					LastAccessTime = fileInfo.LastAccessTime
				})
				.ToList();
		}

		public BllFile GetFile(int id)
		{
			throw new System.NotImplementedException();
		}

		public void CreateFile(BllFile entity)
		{
			throw new System.NotImplementedException();
		}

		public void DeleteFile(string path)
		{
			if (File.Exists(path))
			{
				File.Delete(path);
			}
		}
	}
}
