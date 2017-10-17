﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FileSystemViewer.PLMVC.Models.File
{
	public class FileViewModel
	{
		public string Name { get; set; }
		public string FileSizeText { get; set; }
		public string Extension { get; set; }
		public DateTime LastAccessTime { get; set; }
	}
}