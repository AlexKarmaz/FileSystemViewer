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
					FileSize = SizeConverter(fileInfo.Length),
					Extension = fileInfo.Extension,
					LastAccessTime = fileInfo.LastAccessTime
				})
				.ToList();
		}

		public BllFile GetFile(int id)
		{
			throw new System.NotImplementedException();
		}

		public void CreateFile(string path)
		{
			if (File.Exists(path))
			{
				return;
			}
			File.Create(path);
		}

		public void DeleteFile(string path)
		{
			if (File.Exists(path))
			{
				File.Delete(path);
			}
		}

		public string GetParrent(string path)
		{
			FileInfo file = new FileInfo(path);
			string newPath = file.Directory.FullName;
			return newPath;
		}

		public bool IsExist(string path)
		{
			if (File.Exists(path))
			{
				return true;
			}
			return false;
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
