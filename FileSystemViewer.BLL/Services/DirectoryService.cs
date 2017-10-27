using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FileSystemViewer.BLL.Interface.Entities;
using FileSystemViewer.BLL.Interface.Interfaces;

namespace FileSystemViewer.BLL.Services
{
	public class DirectoryService : IDirectoryService
	{
		public IEnumerable<BllDirectory> GetAllDirectories(string path)
		{
			return Directory.EnumerateDirectories(path)
				.Select(directoryName => new DirectoryInfo(directoryName))
				.Select(directoryInfo => new BllDirectory
				{
					Name = directoryInfo.Name,
					LastAccessTime = directoryInfo.LastAccessTime
				})
				.ToList();
		}

		public void SearchDirectories(string path, string searchString, IList<BllDirectory> directories)
		{
			if (directories == null)
			{
				directories = new List<BllDirectory>();
			}

			DirectoryInfo di = new DirectoryInfo(path);

			foreach (DirectoryInfo d in di.GetDirectories())
			{
				try
				{
					SearchDirectories(d.FullName, searchString, directories);

					if (d.Name.ToLower().Contains(searchString.ToLower()))
					{
						directories.Add(new BllDirectory()
						{
							Name = d.Name,
							Path = d.FullName
						});
					}
				}
				catch 
				{

				}

			}

			//return directories.Select(directoryInfo => new BllDirectory
			//{
			//	Name = directoryInfo.Name,
			//	LastAccessTime = directoryInfo.LastAccessTime
			//})
			//	.ToList(); ;
		}

		public BllDirectory GetDirectory(int id)
		{
			throw new System.NotImplementedException();
		}

		public void CreateDirectory(string path)
		{
			if (Directory.Exists(path))
			{
				return;
			}
			Directory.CreateDirectory(path);
		}

		public void DeleteDirectory(string path)
		{
			if (Directory.Exists(path))
			{
				Directory.Delete(path, true);
			}
		}

		public string GetParrent(string path)
		{
			string newPath = Directory.GetParent(path).ToString();
			return Directory.GetParent(newPath).ToString();
		}

		public bool IsExist(string path)
		{
			if (Directory.Exists(path))
			{
				return true;
			}
			return false;
		}
	}
}
