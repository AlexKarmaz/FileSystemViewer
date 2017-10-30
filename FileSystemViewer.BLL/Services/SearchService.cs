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
	public class SearchService: ISearchService
	{
		public void SearchDirectories(string path, string searchString, IList<BllSearchResult> results)
		{
			if (results == null)
			{
				results = new List<BllSearchResult>();
			}

			DirectoryInfo di = new DirectoryInfo(path);

			FileInfo[] searchFiles = di.GetFiles("*" + searchString + "*");

			foreach (var f in searchFiles)
			{
				results.Add(new BllSearchResult()
						{
							Name = f.Name,
							Path = f.FullName,
							Type = "File"
						});
			}

			foreach (DirectoryInfo d in di.GetDirectories())
			{
				try
				{
					SearchDirectories(d.FullName, searchString, results);

					if (d.Name.ToLower().Contains(searchString.ToLower()))
					{
						results.Add(new BllSearchResult()
						{
							Name = d.Name,
							Path = d.FullName,
							Type = "Folder"
						});
					}
				}
				catch (UnauthorizedAccessException e)
				{
					//Here are skipped the folders to which you do not have access
				}

			}
		}
	}
}
