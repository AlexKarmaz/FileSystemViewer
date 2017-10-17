using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemViewer.BLL.Interface.Entities
{
	public class BllFile
	{
		public string Name { get; set; }
		public string FileSizeText { get; set; }
		public string Extension { get; set; }
		public DateTime LastAccessTime { get; set; }
	}
}
