using System;
using Core.Interfaces;
using Ninject;
using System.Threading.Tasks;
using Core.Http;
using Core.Util;

namespace Core.ViewModels
{
	public class FavoriteViewModel : EventClass
	{	
		private IPlatform _platform;
		[Inject]
		public FavoriteViewModel(iFileSystem fileSystem, iToast toast, IPlatform platform)
		{

			_platform = platform;
			Core.Resor.StoppingManager.SetFileSystem(fileSystem,toast);
		}




	}
}

