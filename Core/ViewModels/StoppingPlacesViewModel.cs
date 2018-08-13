using System;
using Core.Interfaces;
using Ninject;
using System.Threading.Tasks;
using Core.Http;
using Core.Travel;
using Core.Util;
using System.Collections.Generic;

namespace Core.ViewModels
{
	public class StoppingPlacesViewModel : EventClass
	{	
		private IPlatform _uniqId;
		private iFileSystem _filesystem;

		[Inject]
		public StoppingPlacesViewModel (IPlatform uniqId, iFileSystem filesystem)
		{
			_uniqId = uniqId;
			_filesystem = filesystem;
		}

		




	}
}

