using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileSystemViewer.DAL.Interface.Interfaces;

namespace FileSystemViewer.DAL.Concrete
{
	public class UnitOfWork : IUnitOfWork
	{
		private bool isDisposed = false;
		public DbContext Context { get; private set; }
		public UnitOfWork(DbContext context)
		{
			Context = context;
		}

		public void Commit()
		{
			try
			{
				Context.SaveChanges();
			}
			catch (DbEntityValidationException e)
			{
				throw e;
				//logger
			}
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		private void Dispose(bool isDisposing)
		{
			if (!isDisposed)
			{
				if (isDisposing)
				{
					if (Context != null)
					{
						Context.Dispose();
					}
				}
			}
			isDisposed = true;
		}

		~UnitOfWork()
		{
			Dispose(false);
		}
	}
}
